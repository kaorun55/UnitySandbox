// Thank you for @nakamura001
// http://d.hatena.ne.jp/nakamura001/20120107/1325922070
using UnityEngine;
using System.Collections;

public class WebCamBehaviourScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var devices = WebCamTexture.devices;
        if ( devices.Length > 0 ) {
            var webcamTexture = new WebCamTexture( 320, 240, 12 );
            renderer.material.mainTexture = webcamTexture;
            webcamTexture.Play();
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
