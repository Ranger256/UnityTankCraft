using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateWheels : MonoBehaviour {
	public WheelCollider wh;
	public Transform wheels;
	public float wheelOfset;
	public Vector3 wheelStartPos;


	void Awake(){
	}
		
	void FixedUpdate(){
		WheelHit hit;
		Vector3 lp = wheels.transform.localPosition;
		if (wh.GetGroundHit (out hit)) {
			lp.y -= Vector3.Dot (wheels.transform.position - hit.point, transform.up) - wh.radius;
		} else {
			lp.y = wheelStartPos.y - wheelOfset;
		}
		wheels.transform.localPosition = lp;

	}

}

