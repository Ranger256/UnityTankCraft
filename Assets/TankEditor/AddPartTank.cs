using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AddPartTank : MonoBehaviour {
	[SerializeField] GameObject btnPref;
	[SerializeField] ScrollRect sr;
	[SerializeField] Material matPart;
	[SerializeField] GameObject arrPref;
	[SerializeField] GameObject whPref;
	[SerializeField] float mult;
	[SerializeField] float Speed;
	[SerializeField] float SpeedRot;
	[SerializeField] Scrollbar sb;
	GameObject arr;
	GameObject wh;
	float yPan;
	bool isRotIsPos;
	GameObject[] btn;
	string[] filesParts;
	XmlDocument xdoc = new XmlDocument();
	GameObject go;
	bool isClick;
	bool isclick2;
	public static GameObject go2;

	public void isClicked(){
		isRotIsPos = !isRotIsPos;
	}

	public void SetRotNapr(){
		isclick2 = !isclick2;

		if (isclick2)
			mult = -1;
		else
			mult = 1;
	}

	void ButtF(int n){
		string texture = "";
		XmlDocument xdoc = new XmlDocument ();
		xdoc.Load (filesParts[n]);
		foreach(XmlNode xn in xdoc.DocumentElement.ChildNodes){
			switch(xn.Name){
			case "Mesh":
				go = OBJLoader.LoadOBJFile (xn.Attributes ["mesh"].Value);
				go.transform.GetChild (0).gameObject.GetComponent<MeshRenderer> ().material = matPart;
				print (texture);
				go.transform.GetChild (0).gameObject.GetComponent<MeshRenderer> ().material.mainTexture = TexturesLoader.texturesDiffuse[texture];
				go.transform.GetChild (0).gameObject.tag = "parts"; 
				go.transform.GetChild (0).gameObject.AddComponent<BoxCollider> ();
				go.transform.GetChild (0).gameObject.name = filesParts [n];
				break;
			case "Scale":
    			go.transform.localScale = new Vector3 (System.Convert.ToSingle(xn.Attributes["scalex"].Value), System.Convert.ToSingle(xn.Attributes["scaley"].Value), System.Convert.ToSingle(xn.Attributes["scalez"].Value));
				break;
			case "fp":
				GameObject fp = new GameObject ();
				fp.transform.SetParent (go.transform.GetChild (0));
				fp.tag = "cp";
				fp.transform.position = new Vector3 (Convert.ToSingle(xn.Attributes["PosX"].Value), Convert.ToSingle(xn.Attributes["PosY"].Value), Convert.ToSingle(xn.Attributes["PosZ"].Value));
				break;
			case "Diffuse":
				texture = xn.Attributes["texture"].Value;
				break;
			}
		}
	}

	void CreateButtons(){
		filesParts = System.IO.Directory.GetFiles ("Parts", "*_part.xml", System.IO.SearchOption.AllDirectories);
		btn = new GameObject[filesParts.Length];

		for(int i =0; i < btn.Length; i++){
			btn [i] = Instantiate (btnPref);
			btn [i].transform.SetParent (sr.content.transform);
			btn [i].transform.localScale = new Vector3 (0.4f, 0.7f, 0);
			int n = i;
			btn [i].GetComponent<Button> ().onClick.AddListener ( () => ButtF(n));	
		}
	}

	void Start () {
		CreateButtons ();
	}

	void TranslPart(){
		Ray ray = UnityEngine.Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			if (hit.transform.gameObject.CompareTag ("parts")) {
				if (Input.GetMouseButtonDown (0)) {
					isClick = !isClick;
					if (isClick) {
						Vector3 arrPos = hit.collider.gameObject.transform.position;
						Quaternion rot = hit.transform.rotation;
						go2 = hit.collider.gameObject;
						arrPos.y += 2.7f;
						if (!isRotIsPos) {
							arr = Instantiate (arrPref, arrPos, arrPref.transform.rotation);
							arr.transform.SetParent (hit.transform);
							hit.transform.gameObject.isStatic = false;
							hit.transform.GetChild (0).gameObject.isStatic = false;
						} else {
							wh = Instantiate (whPref, arrPos, Quaternion.Euler (0, 0, 0));
							wh.transform.SetParent (hit.transform.parent.transform);
							hit.transform.gameObject.isStatic = false;
							hit.transform.GetChild (0).gameObject.isStatic = false;
						}

					} else {
						go2 = null;
						Destroy (arr);
						Destroy (wh);
						hit.transform.gameObject.isStatic = true;
						hit.transform.GetChild (0).gameObject.isStatic = true;
					}
				}
			} 
			if (isClick) {
				if (Input.GetMouseButtonDown (0))
					yPan = hit.point.y;
				if (Input.GetMouseButton (0)) {
					if (!isRotIsPos) {
						Vector3 vec1 = hit.collider.gameObject.transform.parent.transform.parent.transform.parent.position;

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
						hit.collider.transform.parent.transform.parent.transform.parent.transform.position = Vector3.Lerp(hit.collider.transform.parent.transform.parent.transform.parent.transform.position, vec1, Time.deltaTime * Speed * sb.value);
						//hit.collider.transform.parent.transform.parent.transform.parent.transform.position = Vector3.MoveTowards (hit.collider.transform.parent.transform.parent.transform.parent.transform.position, vec1, Time.deltaTime * Speed * sb.value);
					} else {
						Vector3 vec2 = wh.transform.rotation.eulerAngles;
						switch (hit.transform.gameObject.tag) {
						case "Xrot":
							vec2.x += 360f * mult;
							break;
						case "Yrot":
							vec2.y += 360f * mult;
							break;
						case "Zrot":
							vec2.z += 360f * mult;
							break;
						}
						hit.transform.parent.parent.GetChild (0).transform.rotation = Quaternion.Euler (Vector3.MoveTowards(hit.transform.parent.parent.GetChild (0).transform.rotation.eulerAngles, vec2,Time.deltaTime * SpeedRot * (sb.value * 20)));
					}

				}
			}
	}
	}
	
	// Update is called once per frame
	void Update () {
		TranslPart ();
	}
}
