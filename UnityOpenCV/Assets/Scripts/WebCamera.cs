using UnityEngine;
using System.Collections;
using OpenCvSharp.CPlusPlus;
using OpenCvSharp;

public class WebCamera : MonoBehaviour {

    public int Width = 640;
    public int Height = 480;
    public int FPS = 30;

    VideoCapture video;
    public int VideoIndex = 1;

    Texture2D texture;

	// Use this for initialization
	void Start () {
        // カメラを列挙する
        // 使いたいカメラのインデックスをVideoIndexに入れる
        // 列挙はUnityで使うのはOpenCVだけど、インデックスは同じらしい
        var devices = WebCamTexture.devices;
        for ( int i = 0; i < devices.Length; i++ ) {
            print( string.Format( "index {0}:{1}", i, devices[i].name ) );
        }

        // ビデオの設定
        video = new VideoCapture( VideoIndex );
        video.Set( CaptureProperty.FrameWidth, Width );
        video.Set( CaptureProperty.FrameHeight, Height );

        print( string.Format( "{0},{1}", Width, Height ) );

        // テクスチャの作成
        texture = new Texture2D( Width, Height, TextureFormat.RGB24, false );
        renderer.material.mainTexture = texture;
	}
	
	// Update is called once per frame
	void Update () {
        using ( Mat image = new Mat() ) {
            // Webカメラから画像を取得する
            video.Read( image );
        
            // OpenCVのデータがBGRなのでRGBに変える
            // Bitmap形式に変えてテクスチャに流し込む
            using ( var cvtImage = image.CvtColor( ColorConversion.BgrToRgb ) ) {
                texture.LoadRawTextureData( cvtImage.ImEncode( ".bmp" ) );
                texture.Apply();
            }
        }
    }


    /// <summary>
    /// 終了処理
    /// </summary>
    void OnApplicationQuit()
    {
        if ( video !=null ) {
            video.Dispose();
            video = null;
        }
    }
}
