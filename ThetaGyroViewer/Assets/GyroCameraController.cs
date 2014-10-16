using UnityEngine;
using System.Collections;

public class GyroCameraController : MonoBehaviour {

	void Update () {
		Quaternion gyro = Input.gyro.attitude;
		this.transform.localRotation = Quaternion.Euler(90, 0, 0) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
	}
}
