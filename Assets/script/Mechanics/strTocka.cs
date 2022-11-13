using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class strTocka : MonoBehaviour {
	Coroutine CorutineZaxvat;
	bool teams;
	float prt;
	float prv;
	public float kz;
	public Image imag;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		prt = Mathf.Clamp (prt, 0, 100);
		prv = Mathf.Clamp (prv, 0, 100);
	}
	void OnTriggerEnter(Collider other){
		if (CorutineZaxvat == null)
			CorutineZaxvat = StartCoroutine (Zaxvat ());
		if (other.GetComponent<Tank> ()) {
			teams = true;
		} else if(other.GetComponent<Tank1>()){
			teams = false;
		}
			
	}

	IEnumerator Zaxvat(){
		while(true){
			if (teams == true) {
				imag.fillAmount += kz / 100;
				prt += kz;
				prv -= kz;
			} else {
				prt -= kz;
				prv += kz;
			}
			yield return new WaitForFixedUpdate ();
		}

	}
}
