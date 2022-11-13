using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBOT : MonoBehaviour {
	public float uron;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnCollisionEnter(Collision collision){
		GameObject col = collision.gameObject;
		Tank tank1 = col.GetComponent<Tank> ();
		if(collision.collider.tag == "PlaTank"){
			uron = uron * tank1.ArmourBron;
			tank1.currentXP = tank1.currentXP - uron;
			Destroy (this.gameObject);
		}
	}
}
