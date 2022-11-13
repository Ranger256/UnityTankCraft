using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFP : MonoBehaviour {
	[SerializeField] GameObject prefFP;
	[SerializeField] GameObject arrPref;
	[SerializeField] UnityEngine.UI.Scrollbar sb;
	[SerializeField] float Speed;
	GameObject arr;
	bool isClick;

	public void Add(){
		GameObject go = Instantiate(prefFP);

		go.transform.GetChild(0).gameObject.tag = "cp";
	}

	void TranlFP(){
		Ray ray = UnityEngine.Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			if (hit.transform.gameObject.CompareTag ("cp")) {
				if (Input.GetMouseButtonDown (0)) {
					isClick = !isClick;
					if (isClick) {
						Vector3 arrPos = hit.collider.gameObject.transform.position;
						Quaternion rot = hit.transform.rotation;
						arrPos.y += 2.7f;
							arr = Instantiate (arrPref, arrPos, arrPref.transform.rotation);
							arr.transform.SetParent (hit.transform);
							hit.transform.gameObject.isStatic = false;
							hit.transform.GetChild (0).gameObject.isStatic = false;
					} else {
						Destroy (arr);
					}
				}
			} 
			if (isClick) {
				if (Input.GetMouseButton (0)){
					Vector3 vec1 = hit.collider.gameObject.transform.parent.parent.position;

						switch (hit.collider.gameObject.tag) {

						case "X":

							vec1.x = hit.point.x; 
							break;
						case "Y":

							vec1.y = hit.point.y;
							break;
						case "YDown":
							vec1.y = hit.point.y * -1;
							break;
						case "Z":
							vec1.z = hit.point.z;
							break;
						}
					hit.collider.transform.parent.parent.parent.position = Vector3.Lerp(hit.collider.transform.parent.parent.parent.position, vec1, Time.deltaTime * Speed * sb.value);
						//hit.collider.transform.parent.transform.parent.transform.parent.transform.position = Vector3.MoveTowards (hit.collider.transform.parent.transform.parent.transform.parent.transform.position, vec1, Time.deltaTime * Speed * sb.value);
		}
	}
	}
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		TranlFP ();
	}
}
