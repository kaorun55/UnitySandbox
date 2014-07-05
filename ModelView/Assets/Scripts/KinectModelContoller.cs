using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System.Collections.Generic;
using System.Linq;

public class KinectModelContoller : MonoBehaviour
{
    public BodySourceManager BodySource;

    public int Player = 0;

    public GameObject SpineBase;
    public GameObject SpineMid;
    public GameObject SpineShoulder;
    public GameObject Neck;
    public GameObject Head;
    public GameObject ShoulderLeft;
    public GameObject ElbowLeft;
    public GameObject WristLeft;
    public GameObject HandLeft;
    public GameObject ShoulderRight;
    public GameObject ElbowRight;
    public GameObject WristRight;
    public GameObject HandRight;
    public GameObject HipLeft;
    public GameObject KneeLeft;
    public GameObject AnkleLeft;
    public GameObject FootLeft;
    public GameObject HipRight;
    public GameObject KneeRight;
    public GameObject AnkleRight;
    public GameObject FootRight;
    public GameObject HandTipLeft;
    public GameObject ThumbLeft;
    public GameObject HandTipRight;
    public GameObject ThumbRight;

    GameObject[] bones;

    Quaternion[] baseRotations;


    // Use this for initialization
    void Start()
    {
        bones = new GameObject[]{
            SpineBase, SpineMid, Neck, Head,
            ShoulderLeft, ElbowLeft, WristLeft, HandLeft,
            ShoulderRight, ElbowRight, WristRight, HandRight,
            HipLeft, KneeLeft, AnkleLeft, FootLeft,
            HipRight, KneeRight, AnkleRight, FootRight,
            SpineShoulder, HandTipLeft, ThumbLeft, HandTipRight, ThumbRight,
        };

        baseRotations = new Quaternion[bones.Length];
        for ( int i = 0; i < baseRotations.Length; i++ ) {
            if ( bones[i] != null ) {
                baseRotations[i] = bones[i].transform.localRotation;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var trackedBody = BodySource.GetData().Where( d => d.IsTracked ).ToArray();
        if ( trackedBody.Length > Player ) {
            for ( int i = 0; i < bones.Length; i++ ) {
                RotateJoint( trackedBody[Player], i );
            }
        }
        else {
            //for ( int i = 0; i < baseRotations.Length; i++ ) {
            //    if ( bones[i] != null ) {
            //        bones[i].transform.localRotation = baseRotations[i];
            //    }
            //}
        }
    }

    private void RotateJoint( Body body, int i )
    {
        if ( bones[i] == null ) {
            return;
        }

        var joint = body.JointOrientations[(JointType)i];
        bones[i].transform.localRotation = new Quaternion( joint.Orientation.X, joint.Orientation.Y, joint.Orientation.Z, joint.Orientation.W );
    }
}

public static class Vector4Extentions
{
    public static Vector3 ToVector3( this Windows.Kinect.Vector4 vec4 )
    {
        return new Vector3( vec4.X, vec4.Y, vec4.Z );
    }
}
