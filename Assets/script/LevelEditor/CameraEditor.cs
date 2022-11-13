using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEditor : MonoBehaviour {
	public float speedcam;
	
	void Update () {
		float hAxis = Input.GetAxis ("Horizontal") * speedcam;
		float vAxis = Input.GetAxis ("Vertical") * speedcam;

		transform.Translate (hAxis, 0, vAxis);

		float mv = Input.GetAxis ("Mouse X") * speedcam;
		float mh = Input.GetAxis ("Mouse Y") * -speedcam;

		if(Input.GetMouseButton(1)){
		   transform.Rotate(mh, mv, 0);
		}
	}
}
