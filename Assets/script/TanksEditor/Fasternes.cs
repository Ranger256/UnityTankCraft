using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fasternes : MonoBehaviour {
	bool clicked;
	GameObject gos;
	GameObject con;

	void Start () {
		con = GameObject.FindGameObjectWithTag ("container");
	}
	void Update () {

		GameObject[] go = GameObject.FindGameObjectsWithTag ("parts");
		GameObject[] cp = GameObject.FindGameObjectsWithTag ("cp");

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = UnityEngine.Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				if (hit.collider.gameObject.CompareTag ("parts")){
					gos = hit.collider.gameObject;
					clicked = !clicked;
				}

			}
		}
		if(clicked){
			gos.transform.position = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 19));
			for(int i = 0; i < cp.Length - 1; i++){
				print(Vector3.Distance (cp [i].transform.position, cp [i + 1].transform.position));
				if (Vector3.Distance (cp [i].transform.position, cp [i + 1].transform.position) <= 0.50f) {
					if (gos == cp [i].transform.parent.gameObject) {
						gos.transform.parent = cp [i + 1].transform.parent.gameObject.transform;
					}
					if (gos == cp [i + 1].transform.parent.gameObject) {
						gos.transform.parent = cp [i].transform.parent.gameObject.transform;
					}
				} 
				if(Vector3.Distance (cp [i].transform.position, cp [i + 1].transform.position) > 0.50f){
					//gos.transform.parent = con.transform;
				}
			}
		}
	}
	public void Attach(){
		
	}
}
