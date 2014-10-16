using UnityEngine;
using System.Collections;
using OpenCvSharp.CPlusPlus;
using OpenCvSharp;
using System;

public class FaceDetect : MonoBehaviour {

    public int Width = 640;
    public int Height = 480;
    public int FPS = 30;

    public bool Mirror = false;

    public Camera Camera;
    Camera _Camera;

    VideoCapture video;
    public int VideoIndex = 1;

    Texture2D texture;

    CascadeClassifier cascade;

    public GameObject Object;

	// Use this for initialization
	void Start () {
        // カメラを列挙する
        // 使いたいカメラのインデックスをVideoIndexに入れる
        // 列挙はUnityで使うのはOpenCVだけど、インデックスは同じらしい
        var devices = WebCamTexture.devices;
        for ( int i = 0; i < devices.Length; i++ ) {
            print( string.Format( "index {0}:{1}", i, devices[i].name) );
        }

        // ビデオの設定
        video = new VideoCapture( VideoIndex );
        video.Set( CaptureProperty.FrameWidth, Width );
        video.Set( CaptureProperty.FrameHeight, Height );

        print( string.Format("{0},{1}", Width, Height) );

        // 顔検出器の作成
        cascade = new CascadeClassifier( Application.dataPath + @"/haarcascade_frontalface_alt.xml" );

        // テクスチャの作成
        texture = new Texture2D( Width, Height, TextureFormat.RGB24, false );
        renderer.material.mainTexture = texture;

        // 変換用のカメラの作成
        _Camera = GameObject.Find( Camera.name ).camera;
        print( string.Format( "({0},{1})({2},{3})", Screen.width, Screen.height, _Camera.pixelWidth, _Camera.pixelHeight ) );
    }
	
	// Update is called once per frame
	void Update () {
        using ( Mat image = new Mat() ) {
            // Webカメラから画像を取得する
            video.Read( image );

            // 顔を検出する
            var faces = cascade.DetectMultiScale( image );
            if ( faces.Length > 0 ) {
                var face = faces[0];

                // 顔の矩形を描画する
                image.Rectangle( face, new Scalar( 255, 0, 0 ), 2 );

                // 中心の座標を計算する
                var x = face.TopLeft.X + (face.Size.Width / 2);
                var y = face.TopLeft.Y + (face.Size.Height / 2);

                // オブジェクトを移動する
                if ( Object !=null ) {
                    Object.transform.localPosition = Vector2ToVector3( new Vector2( x, y ) );
                }
            }

            // OpenCVのデータがBGRなのでRGBに変える
            // Bitmap形式に変えてテクスチャに流し込む
            using(var cvtImage = image.CvtColor( ColorConversion.BgrToRgb )){
                texture.LoadRawTextureData( cvtImage.ImEncode( ".bmp" ) );
                texture.Apply();
            }
        }
	}

    void OnApplicationQuit()
    {
        if ( video !=null ) {
            video.Dispose();
            video = null;
        }
    }

    /// <summary>
    /// OpenCVの2次元座標をUnityの3次元座標に変換する
    /// </summary>
    /// <param name="vector2"></param>
    /// <returns></returns>
    private Vector3 Vector2ToVector3( Vector2 vector2 )
    {
        if ( Camera == null ) {
            throw new Exception("");
        }

        // スクリーンサイズで調整(WebCamera->Unity)
        vector2.x = vector2.x * Screen.width / Width;
        vector2.y = vector2.y * Screen.height / Height;

        // Unityのワールド座標系(3次元)に変換
        var vector3 = _Camera.ScreenToWorldPoint( vector2 );

        // 座標の調整
        // Y座標は逆、Z座標は0にする(Xもミラー状態によって逆にする必要あり)
        vector3.y *= -1;
        vector3.z = 0;

        return vector3;
    }

}
