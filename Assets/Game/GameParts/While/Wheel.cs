using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {

	[SerializeField] GameObject[] go;
	[SerializeField] KPP kpp;
	public float BrakesForce;
	public float BrakesFrontPart;
	public float BrakesState;
	public float Ca = 15;
	float speedRot;
	float z;
	float speed = 0;
	float r;


	// Use this for initialization
	void Start () {
		z = go [0].transform.root.rotation.z;
	}
	

	void Update(){
		if(go [0].GetComponent<Rigidbody> ().velocity.magnitude > 1){
			z += (-Input.GetAxis("Horizontal") / go [0].GetComponent<Rigidbody> ().velocity.magnitude * Ca);
		}


		go [0].transform.root.rotation = Quaternion.Euler(new Vector3 (90, go[0].transform.root.rotation.y,z));
	}

	void FixedUpdate () {
		for(int i =0; i < go.Length; i++){

			float ka = ((kpp.toWheelTorq / 40) / kpp.engine.Inertia);

			if(Input.GetKey(KeyCode.S))
				ka = 0;

			speed += ka;
		
			Vector3 direction = new Vector3 (speed, 0, 0);

			go[i].transform.position += go[i].transform.TransformDirection(direction);
		}





	}
}
