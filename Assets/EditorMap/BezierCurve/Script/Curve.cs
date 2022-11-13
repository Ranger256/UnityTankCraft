using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour {
	[SerializeField] GameObject Curve1;
	[SerializeField] GameObject Curve2;
	[SerializeField] GameObject Circle1;
	GameObject[] go;
	int col = 0;
	bool isClicked = false;
	SpriteRenderer sr;
	RectTransform rt;

	// Use this for initialization
	void Start () {
		//sr = line.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			if (col < 1) {
				System.Array.Resize (ref go, col + 1);
				go [col] = Instantiate (Curve1, new Vector3 (0, 0, 0), transform.rotation);
				isClicked = true;
				col++;
			}
		}
		if(isClicked){

		}
		if(Input.GetMouseButtonDown(1)){
			Vector3 posLineOfCircle;
				System.Array.Resize (ref go, col + 1);
			if (col > 1) {
				posLineOfCircle = go[col - 1].transform.GetChild(1).transform.localPosition; 
			} else {
				posLineOfCircle = go[col - 1].transform.GetChild(1).transform.localPosition; 
			}

				posLineOfCircle.x += -15f;
				go[col] = Instantiate (Curve2, posLineOfCircle, transform.rotation);
				go [col].transform.SetParent (go[col - 1].transform);
				col++;
				isClicked = false;
			print (col);


		}
	}
}
