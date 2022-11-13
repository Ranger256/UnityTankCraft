using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sh : MonoBehaviour {

	public GameObject sp;
	public GameObject bul;
	public float uron;
	public int countAmmu;
	private bool reload = false;
	public AudioClip ac1;
	public AudioSource ac2;
	public Text text1;
	public int WaitTime;
	public float speedPul;


	// Use this for initialization
	void Awake () {
		ac2.pitch = 1;
		ac2.clip = ac1;
		ac2.volume = 1;
		ac2.loop = false;
	}
	
	// Update is called once per frame
	void Update () {
		ac2.volume = 1;
		text1.text = string.Format ("{0:0}", countAmmu);
		if(Input.GetMouseButtonDown(0) && countAmmu > 0 && reload == false){
			Vector3 pos = sp.transform.position;
			Quaternion rot = sp.transform.rotation;
			GameObject pul = Instantiate (bul,pos,rot);
			Rigidbody rb = pul.GetComponent<Rigidbody> ();
			rb.AddForce (pul.transform.up * speedPul, ForceMode.Impulse);
			countAmmu--;
			ac2.Play ();
			Reload ();
			Destroy (pul, 2);
		}


	}
	void Reload(){
		reload = true;
		StartCoroutine (Reloads());
	}
	IEnumerator Reloads(){
		//if (ac2.clip.length < WaitTime) {
		//	ac2.Play();
		//} else {
			//ac2.Stop();
		//}
		yield return new WaitForSeconds (WaitTime);
		reload = false;
	}
}
