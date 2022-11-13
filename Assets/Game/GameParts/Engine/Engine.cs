using UnityEngine;

public class Engine : MonoBehaviour {

	float[] Power;
	float Throttle = 1;
	public float RPM = 801;
	public float Inertia = 3.6f;
	float additionalRPM;
	public float Clutch = 0;
	float[] Torque;
	public float torq;
	public float BackTorque = 1;
	public int maxOborots = 5000;

	void EngineWorking(){
		if (Input.GetKey (KeyCode.W))
			Throttle = 1;
		else
			Throttle = 0.1f;

		if (Throttle <= 0 && !Input.GetKey (KeyCode.W))
			Throttle += 0.1f;

		Power[(int) RPM] = Torque[(int) RPM] * RPM / 7000;
		torq = Torque[(int)RPM] * Throttle;
	
		RPM += torq / Inertia;

		float additionRPM = torq / Inertia - Mathf.Pow(1.0f - Throttle, 2) * BackTorque;
		RPM += additionRPM * (1.0f - Clutch);

		RPM = Mathf.Clamp (RPM, 800, maxOborots - 1);

		}


	void Start () {
		Torque = new float[maxOborots];
		Power = new float[maxOborots];

		for(int i = 0; i < maxOborots;i++){
			Torque [i] = i / 71.42f;
		}

		Torque [0] = 0.01f;
	}
	
	// Update is called once per frame
	void Update () {
		EngineWorking ();
	}
}
