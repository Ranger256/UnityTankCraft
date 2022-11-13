using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tank : MonoBehaviour {
	public Rigidbody rb;
	public float MaxOb;
	public float MinOb;
	private float Ob;
	public float KG;
	public float KT;
	public Text ob2;
	private bool Started = false;
	public Text started;
	private float KP;

	public float gear1;
	public float gear2;
	public float gear3;
	public float gear4;
	public float R;
	private float currentGear;
	private float cur = 0;
	public Text gear;
	public float GlPar;

	public WheelCollider[] peredOs;
	public WheelCollider[] zadOs;
	public Transform[] peOs;
	public Transform[] zaOs;
	private float speed;
	public Text speed1;
	private float okr = 3.1f;
	private float hAxis;
	public float steerAnlge2;

	public Bashn bas;
	private float ge;

	public AudioSource as1;
	public AudioClip as2;
	public float curhp;
	public float maxhp;

	public float currentXP;
	public float ArmourBron;
	public Sh sht;


	private bool cams;

	public Camera cam2;
	public Camera cam3;

	public float pr = 0;




	// Use this for initialization
	void Start () {
		cams = false;
		curhp = maxhp;
		rb = GetComponent<Rigidbody> ();
		KP = 1;

		as1.GetComponent<AudioSource> ();
		as1.clip = as2;
		as1.volume = 0f;
		as1.pitch = 1.45f;
		as1.loop = true;
		as1.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		pr = Mathf.Clamp (pr, 0, 100);
		
		if (currentXP > 0) {
			
			sht.enabled = true;

			as1.volume = Ob / 3500;

			if (Input.GetKeyUp (KeyCode.B)) {
				Started = !Started;
			}
			if (Started == true) {
				if (Input.GetButton ("Gas")) {
					Ob = (Ob + KG) - KP;
				} else {
					Ob = (Ob - 2) - KP;
				}
				if (Input.GetButton ("Brake")) {
					Ob = (Ob - KT) - KP;
				}
				Ob = Mathf.Clamp (Ob, MinOb, MaxOb);

				started.text = string.Format ("Started");
			} else {
				Ob = Ob - 5;
				if (Ob < 0) {
					started.text = string.Format ("Shutdown");
					Ob = Mathf.Clamp (Ob, 0, 0);
				} else {
					started.text = string.Format ("Shutdown");
					if (Ob < 810) {
						Ob = (Ob - 10) - KP;
						Ob = Mathf.Clamp (Ob, 0, MinOb);
					} else {
						Ob = (Ob - 10) - KP;
					}
				}
			}
			ge = currentGear + 1;
			ob2.text = string.Format ("{0:0}", Ob);
			peredach ();
			if (currentGear != 0 && Ob > 980) {
				speed = ((okr / (GlPar * ge)) * 2 * Ob);
				speed = speed * -1;
			} else {
				speed = 0;
			}
			if (speed < 10) {
				rb.drag = 3f;
			} 

			speed1.text = string.Format ("{0:0}", speed / -110);
		} else {
			sht.enabled = false;
			Started = false;
			Ob = 0;
			speed = 0;
		}

	}

	void peredach(){
		if (Input.GetButton ("GearN")) {
			cur = 0;
		}
		if(Input.GetButton("Gear1")){
			cur = 1;
		}
		if(Input.GetButton("Gear2")){
			cur = 2;
		}
		if(Input.GetButton("Gear3")){
			cur = 3;
		}
		if(Input.GetButton("Gear4")){
			cur = 4;
		}
		if(Input.GetButton("GearR")){
			cur = 5;
		}

		if(cur == 0){
			currentGear = 0;
			gear.text = string.Format ("N");
			KP = 0;
		}
		if(cur == 1){
			currentGear = gear1;
			gear.text = string.Format ("1");
			KP = 1;
		}
		if(cur == 2){
			currentGear = gear2;
			gear.text = string.Format ("2");
			KP = 1.34f;
		}
		if(cur == 3){
			currentGear = gear3;
			gear.text = string.Format ("3");
			KP = 1.53f;
		}
		if(cur == 4){
			currentGear = gear4;
			gear.text = string.Format ("4");
			KP = 1.8f;
		}
		if(cur == 5){
			currentGear = R;
			gear.text = string.Format ("R");
			KP = 1.1f;
		}
	}
	void FixedUpdate(){
		hAxis = Input.GetAxis ("Horizontal");
		peredOs [0].motorTorque = speed;
		peredOs [1].motorTorque = speed;
		peredOs [2].motorTorque = speed;
		peredOs [3].motorTorque = speed;
		zadOs [0].motorTorque = speed;
		zadOs [1].motorTorque = speed;
		zadOs [2].motorTorque = speed;
		zadOs [3].motorTorque = speed;
		peredOs [0].steerAngle = hAxis * peredOs[0].radius * steerAnlge2;
		zadOs [0].steerAngle = hAxis * zadOs[0].radius * steerAnlge2;
		peredOs [1].steerAngle = hAxis * peredOs[1].radius * steerAnlge2;
		zadOs [1].steerAngle = hAxis * zadOs[1].radius * steerAnlge2;

		peOs [0].Rotate (peredOs[0].rpm * Time.fixedDeltaTime,0, 0);
		peOs [1].Rotate (peredOs[1].rpm * Time.fixedDeltaTime,0, 0);
		peOs [2].Rotate (peredOs[2].rpm * Time.fixedDeltaTime,0, 0);
		peOs [3].Rotate (peredOs[3].rpm * Time.fixedDeltaTime,0, 0);
		zaOs [0].Rotate (-zadOs[0].rpm * Time.fixedDeltaTime, 0, 0);
		zaOs [1].Rotate (-zadOs[1].rpm * Time.fixedDeltaTime, 0, 0);
		zaOs [2].Rotate (zadOs[2].rpm * Time.fixedDeltaTime, 0, 0);
		zaOs [3].Rotate (zadOs[3].rpm * Time.fixedDeltaTime, 0, 0);

		peOs [0].localEulerAngles = new Vector3 (peredOs[0].rpm,hAxis * steerAnlge2 , 0 );
		peOs [3].localEulerAngles = new Vector3 (peredOs[3].rpm,hAxis * steerAnlge2 , 0 );
		zaOs [0].localEulerAngles = new Vector3 (zadOs[0].rpm,hAxis * steerAnlge2 , 180 );
		zaOs [1].localEulerAngles = new Vector3 (zadOs[1].rpm,hAxis * steerAnlge2 , 180 );


	}
}
