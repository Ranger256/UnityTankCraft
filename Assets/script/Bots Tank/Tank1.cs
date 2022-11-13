using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Tank1 : MonoBehaviour {
	[Range(0,360)] public float ViewAngle = 90f;
	public float ViewDistance = 15f;
	public float currenthp;
	public float maxhp;
	public float armorPenetrability;
	public Transform target;
	public float detect;
	public NavMeshAgent agent;
	public float bashras;
	public GameObject bashna;
	private float Angle;
	public float minSpeedBas;
	public float maxSpeedBas;
	public float KS;
	private float rotationSpeed;
	private Transform transformAgent;
	private float distancePlayer;
	public GameObject spawnray;
	public WheelCollider[] bokOsCOL;
	public WheelCollider[] bok2osCOL;
	public Transform[] bokosTR;
	public Transform[] bok2OsTR;
	public float sidespeed;
	private float AngleRotate;
	private float Ob;
	private float speeed;
	public float KO;
	public float brake;
	public GameObject spawn;
	public GameObject bul;
	public float countAmmu;
	public float waitTimes;
	private bool reload;
	public float speedPul;
	public float distStr;
	private float glpar = 4f;
	public float minOb;
	public float MaxOb;
	private float rotare;
	public GameObject strTochka;

	void Awake () {
		
		currenthp = maxhp;
		agent = GetComponent<NavMeshAgent> ();
		agent.updateRotation = false;
		rotationSpeed = agent.angularSpeed;
		transformAgent = agent.transform;
		Ob = 0;
		brake = 0;
		speeed = 0f;
		reload = false;
	}

	void oborots(){
		Ob = Mathf.Clamp (Ob,minOb, MaxOb);
		Ob = Ob + KO;
		speeed = ((bokOsCOL [0].radius * 2) / glpar) * Ob;
	}

	void Start () {

	}
	void FixedUpdate(){
		distancePlayer = Vector3.Distance (target.transform.position, agent.transform.position);
		if(currenthp > 1){
			if (distancePlayer <= detect) {
				if (distStr < distancePlayer) {
					if (bashras > distancePlayer) {
						RotateTarget ();
						brake = 500f;
						speeed = -5f;
						Ob = 0f;
					} else {
						Dvijenie ();
						RotateTank ();
						isView ();
						oborots ();
					}
				} 
			} else {
				float distanceTochka = Vector3.Distance (strTochka.transform.position, agent.transform.position);
				if (distanceTochka > 5f) {
					oborots ();
					Dvijenie ();
					float AngleRotate1 = Vector3.Angle (strTochka.transform.position - transform.position, transform.right) - 90;
					float rotare1 = Mathf.Clamp (AngleRotate1, -sidespeed, sidespeed);
					bok2osCOL [0].steerAngle = -rotare1;
					bokOsCOL [0].steerAngle = -rotare1;
					bok2osCOL [1].steerAngle = -rotare1;
					bokOsCOL [1].steerAngle = -rotare1;
					bok2OsTR [0].localEulerAngles = new Vector3 (-bok2OsTR [0].localEulerAngles.x - rotare1, bok2OsTR [0].localEulerAngles.z);
					bok2OsTR [1].localEulerAngles = new Vector3 (-bok2OsTR [1].localEulerAngles.x - rotare1, bok2OsTR [1].localEulerAngles.z);
					bokosTR [0].localEulerAngles = new Vector3 (-bokosTR [0].localEulerAngles.x, -rotare1, bokosTR [0].localEulerAngles.z);
					bokosTR [1].localEulerAngles = new Vector3 (-bokosTR [1].localEulerAngles.x, -rotare1, bokosTR [1].localEulerAngles.z);
				} else {
					speeed = 0;
					Ob = 0;
					brake = 100;
				}
			}
		}
	}

	void RotateTarget(){
		Vector3 lookVector = target.position - transformAgent.position;
		lookVector.y = 0;
		if(lookVector == Vector3.zero)return;
		if(bashras >= distancePlayer){
			if (target.transform.rotation.y >= transformAgent.transform.rotation.y || target.transform.rotation.y <= transformAgent.transform.rotation.y) {
				Angle = 2;
			} else {
				Angle = Angle - KS;
			}


			Angle = Mathf.Clamp (Angle, minSpeedBas, maxSpeedBas);
			if(isView()){
				bashna.transform.Rotate (0, -Angle, 0);
			}

		}else{
			Angle = Mathf.Clamp (Angle, minSpeedBas, maxSpeedBas);
			Angle = Angle - KS;


		}
	}
	void RotateTank(){
		AngleRotate = Vector3.Angle (target.position - transform.position, transform.right)-90;
		rotare = Mathf.Clamp (AngleRotate, -sidespeed, sidespeed);
		bok2osCOL [0].steerAngle = -rotare;
		bokOsCOL [0].steerAngle = -rotare ;
		bok2osCOL [1].steerAngle = -rotare ;
		bokOsCOL [1].steerAngle = -rotare ;
		bok2OsTR [0].localEulerAngles = new Vector3 (-bok2OsTR[0].localEulerAngles.x -rotare, bok2OsTR[0].localEulerAngles.z);
		bok2OsTR [1].localEulerAngles = new Vector3 (-bok2OsTR[1].localEulerAngles.x -rotare, bok2OsTR[1].localEulerAngles.z);
		bokosTR [0].localEulerAngles = new Vector3 (-bokosTR[0].localEulerAngles.x, -rotare, bokosTR[0].localEulerAngles.z);
		bokosTR [1].localEulerAngles = new Vector3 (-bokosTR[1].localEulerAngles.x, -rotare, bokosTR[1].localEulerAngles.z);
	}

	void Dvijenie(){
		bok2osCOL [0].motorTorque = speeed;
		bok2osCOL [1].motorTorque = speeed;
		bok2osCOL [2].motorTorque = speeed;
		bok2osCOL [3].motorTorque = speeed;
		bokOsCOL [0].motorTorque = speeed;
		bokOsCOL [1].motorTorque = speeed;
		bokOsCOL [2].motorTorque = speeed;
		bokOsCOL [3].motorTorque = speeed;
		bokosTR [0].Rotate (bokOsCOL[0].rpm,0,0);
		bokosTR [1].Rotate (bokOsCOL[1].rpm,0,0);
		bokosTR [2].Rotate (bokOsCOL[2].rpm,0,0);
		bokosTR [3].Rotate (bokOsCOL[3].rpm,0,0);
		bok2OsTR [0].Rotate (bok2osCOL[0].rpm, 0, 0);
		bok2OsTR [1].Rotate (bok2osCOL[1].rpm, 0, 0);
		bok2OsTR [2].Rotate (bok2osCOL[2].rpm, 0, 0);
		bok2OsTR [3].Rotate (bok2osCOL[3].rpm, 0, 0);
		bokOsCOL [0].brakeTorque = brake;
		bok2osCOL [0].brakeTorque = brake;
		bokOsCOL [1].brakeTorque = brake;
		bok2osCOL [1].brakeTorque = brake;
		bokOsCOL [2].brakeTorque = brake;
		bok2osCOL [2].brakeTorque = brake;
		bokOsCOL [3].brakeTorque = brake;
		bok2osCOL [3].brakeTorque = brake;
	}

	bool isView(){
		
		float realAngle = Vector3.Angle (spawnray.transform.forward, target.position - spawnray.transform.position);
				Vector3 targetdir = target.position - bashna.transform.position;
				float angle = Vector3.Angle (targetdir, bashna.transform.forward);
				
		if (angle > 2f) {
			return true;
		} else {
			if(distancePlayer < detect){
				Strelba ();
			}
		}
		return false;
	}
	void Strelba(){
		if (reload == false) {
			if (countAmmu > 0) {
				Vector3 spawnpoint = spawn.transform.position;
				Quaternion rots = spawn.transform.rotation;
				GameObject pul = Instantiate (bul, spawnpoint, rots) as GameObject;
				Rigidbody rb = pul.GetComponent<Rigidbody> ();
				rb.AddForce (pul.transform.forward * speedPul, ForceMode.Impulse);
				countAmmu--;
				reload = true;
				if (reload) {
					StartCoroutine (reloads());
				}
				Destroy (pul, waitTimes);
			}
		}
	}
	IEnumerator reloads(){
		yield return new WaitForSeconds (waitTimes);
		reload = false;
	}
}
