using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartFixing : MonoBehaviour {
	[SerializeField] GameObject container;
	GameObject[] go;

	void Fixing(){
		for(int i =0; i < go.Length; i++){
			for(int j = 0; j < go.Length;j++){
				if(i != j){
					//for(int k = 0; k < AddPartTank.go2.transform.GetChildCount){

					//}
					if (Vector3.Distance (go [i].transform.position, go [j].transform.position) < 1f) {
						
						go[j].transform.root.SetParent (go [i].transform);
						if (Input.GetMouseButtonDown (2)) {
							print (go[j]);
							go[i].transform.parent.parent.localPosition = new Vector3 (go[j].transform.localPosition.x * -1, 0, 0);;
						}
					} else {
						//AddPartTank.go2.transform.root.SetParent (container.transform);
					}
				}

			}
		}
	}

	public void Up(){
		go = GameObject.FindGameObjectsWithTag ("cp");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Fixing ();
	}
}
