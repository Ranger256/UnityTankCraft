using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KPP : MonoBehaviour {

	public Engine engine;
	[SerializeField] float[] Gear;
	[SerializeField] float TopGear;
	[SerializeField] float Efficiency;
	public int CurrentGear = 0;
	public float toWheelTorq;

	void KPPWorking(){
		if (Gear [CurrentGear] != 0)
			toWheelTorq = engine.torq * Efficiency / (TopGear * Gear [CurrentGear]);
		else
			toWheelTorq = 0;
		if (engine.RPM < 830 && engine.Clutch != 1)
			toWheelTorq = 0;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		KPPWorking ();
	}
}
