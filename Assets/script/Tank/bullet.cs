using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
	public float uron;
	public string tag1;

	// Use this for initialization
	void Start () {
		
	}
	void OnCollisionEnter(Collision collision){
		print ("GG");
		GameObject col = collision.gameObject;

		Tank1 tank2 = col.GetComponent<Tank1> ();
		if(collision.collider.tag == tag1){
			uron = uron * tank2.armorPenetrability;
			tank2.currenthp -= uron;
			print (tank2.currenthp);
			Destroy (this.gameObject);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
