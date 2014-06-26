// http://docs.unity3d.com/ScriptReference/PlayerPrefs.html
using UnityEngine;
using System.Collections;

public class GUIBehaviourScript1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Toggle = PlayerPrefs.GetInt( "Toggle1" ) == 0 ? false : true;
        Text = PlayerPrefs.GetString( "Text1" );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    #region Toggle
    public bool _Toggle;
    public bool Toggle
    {
        get
        {
            return _Toggle;
        }

        set
        {
            if ( _Toggle == value ) {
                return;
            }

            _Toggle = value;
            PlayerPrefs.SetInt( "Toggle1", value ? 1 : 0 );
            //PlayerPrefs.Save();
        }
    }
    #endregion

    #region Text
    public string _Text = "";
    public string Text
    {
        get
        {
            return _Text;
        }

        set
        {
            if ( _Text == value ) {
                return;
            }

            _Text = value;
            PlayerPrefs.SetString( "Text1", value );
            //PlayerPrefs.Save();
        }
    }
    #endregion

    void OnGUI()
    {
        Toggle = GUI.Toggle( new Rect( 100, 100, 100, 30 ), Toggle, "bool値" );
        Text = GUI.TextArea( new Rect( 100, 140, 100, 30 ), Text, 10 );
    }
}
