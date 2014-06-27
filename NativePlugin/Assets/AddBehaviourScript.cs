using UnityEngine;
using System.Collections;

public class AddBehaviourScript : MonoBehaviour {

    public int A = 1;
    public int B = 2;
    public int Result;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Result = PluginNet.Plugin.PluginAdd( A, B );
	}
}
