// Thank you for @nakamura001
// http://d.hatena.ne.jp/nakamura001/20120107/1325922070
using UnityEngine;
using System.Collections;

public class WebCamBehaviourScript : MonoBehaviour
{
    public int Width = 1920;
    public int Height = 1080;
    public int FPS = 30;

    public bool Mirror = false;

    public bool RightUp = false;
    public bool LeftUp = false;

    // Use this for initialization
    void Start()
    {
        var devices = WebCamTexture.devices;
        if ( devices.Length == 0 ) {
            Debug.LogError( "Webカメラが検出できませんでした。" );
            return;
        }

        // WebCamテクスチャを作成する
        var webcamTexture = new WebCamTexture( Width, Height, FPS );
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();

        // ミラーリング
        if ( Mirror ) {
            transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
        }

        // 縦にする
        if ( RightUp ) {
            var euler = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler( euler.x, euler.y, euler.z + 90 );
        }
        else if ( LeftUp ) {
            var euler = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler( euler.x, euler.y, euler.z - 90 );
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
