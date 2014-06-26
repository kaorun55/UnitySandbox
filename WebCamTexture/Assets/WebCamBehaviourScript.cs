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
        if ( devices.Length > 0 ) {
            var webcamTexture = new WebCamTexture( Width, Height, FPS );
            renderer.material.mainTexture = webcamTexture;
            webcamTexture.Play();

            if( Mirror){
                transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
            }

            if ( RightUp ) {
                var euler = transform.localRotation.eulerAngles;
                transform.localRotation = Quaternion.Euler( euler.x, euler.y, euler.z + 90 );
            }
            else if ( LeftUp ) {
                var euler = transform.localRotation.eulerAngles;
                transform.localRotation = Quaternion.Euler( euler.x, euler.y, euler.z - 90 );
            }
        }
        else {
            Debug.LogError( "Webカメラが検出できませんでした。" );
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
