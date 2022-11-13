using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bashn : MonoBehaviour {
	public float K;
	private float Angl = 0;
	public float minspeed;
	public float maxspeed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("TurningTowerLeft")) {
			Angl = Angl + K;
		}
		if(Input.GetButton("TurningTowerRight")){
			Angl = Angl - K;
		}
		Angl = Mathf.Clamp (Angl, minspeed, maxspeed);
		transform.Rotate (0, 0, Angl);	
	}
}
