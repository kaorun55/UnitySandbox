using UnityEngine;
using System.Collections;

public class GUIBehaviourScript2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Toggle = PlayerPrefs.GetInt( "Toggle2" ) == 0 ? false : true;
        Text = PlayerPrefs.GetString( "Text2" );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool Toggle = false;
    public string Text = "";
    //public string Text = PlayerPrefs.GetString( "Text" ); // これはエラー

    void OnGUI()
    {
        var toggle = GUI.Toggle( new Rect( 250, 100, 100, 30 ), Toggle, "bool値" );
        if ( toggle != Toggle ) {
            Toggle = toggle;
            PlayerPrefs.SetInt( "Toggle2", Toggle ? 1 : 0 );
            //PlayerPrefs.Save();
        }

        var text = GUI.TextArea( new Rect( 250, 140, 100, 30 ), Text, 10 );
        if ( text != Text ) {
            Text = text;
            PlayerPrefs.SetString( "Text2", Text );
            //PlayerPrefs.Save();
        }
    }
}
