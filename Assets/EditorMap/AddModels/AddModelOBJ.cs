using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class AddModelOBJ : MonoBehaviour {
	string[] pathObjects;
	GameObject[] btDyn;
	[SerializeField] GameObject arrPref;
	[SerializeField] GameObject whPref;
	GameObject arr;
	GameObject wh;
	bool isClick = false;
	GameObject[] go;
	int col = 0;
	[SerializeField] float Speed;
	[SerializeField] float SpeedRotation;
	[SerializeField]UnityEngine.UI.ScrollRect sr;
	[SerializeField]UnityEngine.UI.Scrollbar sb;
	[SerializeField] Material mat;
	[SerializeField] GameObject btPfef;
	Vector3 lastFrame;
	Vector3 frame;
	bool isRotIsPos = false;
	float yPan;
	float mult = 1;
	MaterialPropertyBlock mb;
	bool isClick2 = false;


	Texture2D LoadTexture(string pathTextur){
		Texture2D tex = new Texture2D (512, 512);
		tex.LoadImage (System.IO.File.ReadAllBytes(pathTextur));
		return tex;
	}

	void Start(){
		

		pathObjects = System.IO.Directory.GetFiles ("Maps/classes/Objects/", "*_object.xml", System.IO.SearchOption.AllDirectories);

		btDyn = new GameObject[pathObjects.Length];
		for(int i = 0; i < pathObjects.Length;i++){
			btDyn[i] = Instantiate (btPfef) as GameObject;
			btDyn [i].transform.SetParent (sr.content);
			btDyn [i].GetComponentInChildren<UnityEngine.UI.Text> ().text = "";
			btDyn [i].GetComponent<UnityEngine.UI.Button> ().GetComponent<RectTransform> ().localScale = new Vector3 (0.4f, 0.8f);
			int n = i;

			btDyn [i].GetComponent<UnityEngine.UI.Button> ().onClick.AddListener ( () => ButtF(n));
		}
	}

	void ButtF(int n){
		AddModel (pathObjects[n]);
	}

	public void isClickedRot(){
		isRotIsPos = !isRotIsPos;
		if(isRotIsPos){
			Destroy (arr);
		}
	}
	public void isClickedNapr(){
		if (mult == 1) {
			mult = 0;
		} else{
			mult = 1;
		}
	}
		

	public	GameObject AddModel(string path){
		mb = new MaterialPropertyBlock ();
		string pathModel = "";
		string pathDiffuseTexture = "";
		string pathNormalTexture = "";
		string pathSmothnessTexture = "";
		string pathSpecularTexture = "";
		string name = "";
		float scalex = 0;
		float scaley = 0;
		float scalez = 0;
		System.Xml.XmlReader xr = System.Xml.XmlReader.Create (path);
		while (xr.Read ()) {
			//if((xr.NodeType == XmlNodeType.Element) && (xr.Name == "NamePart")){
			//	namePart = xr.GetAttribute ("name");
			//}
			if ((xr.NodeType == System.Xml.XmlNodeType.Element) && (xr.Name == "DiffuseTexture")) {
				pathDiffuseTexture = xr.GetAttribute ("texture");
			}
			if ((xr.NodeType == System.Xml.XmlNodeType.Element) && (xr.Name == "NormalTexture")) {
				pathNormalTexture = xr.GetAttribute ("texture");
			}
			if ((xr.NodeType == System.Xml.XmlNodeType.Element) && (xr.Name == "GlossinessTexture")) {
				pathSmothnessTexture = xr.GetAttribute ("texture");
			}
			if ((xr.NodeType == System.Xml.XmlNodeType.Element) && (xr.Name == "SpecularTexture")) {
				pathSpecularTexture = xr.GetAttribute ("texture");
			}
			if ((xr.NodeType == System.Xml.XmlNodeType.Element) && (xr.Name == "Mesh")) {
				pathModel = xr.GetAttribute ("mesh");
			}
			if ((xr.NodeType == System.Xml.XmlNodeType.Element) && (xr.Name == "Name")) {
				name = xr.GetAttribute ("name");
			}
			if ((xr.NodeType == System.Xml.XmlNodeType.Element) && (xr.Name == "ScaleX")) {
				scalex = System.Convert.ToSingle (xr.GetAttribute ("scalex"));
			}
			if ((xr.NodeType == System.Xml.XmlNodeType.Element) && (xr.Name == "ScaleY")) {
				scaley = System.Convert.ToSingle (xr.GetAttribute ("scaley"));
			}
			if ((xr.NodeType == System.Xml.XmlNodeType.Element) && (xr.Name == "ScaleZ")) {
				scalez = System.Convert.ToSingle (xr.GetAttribute ("scalez"));
			}


		}


		System.Array.Resize (ref go, col + 1);
		GameObject goModel = OBJLoader.LoadOBJFile (pathModel);
		goModel.transform.GetChild(0).localScale = new Vector3 (scalex, scaley, scalez);
		goModel.transform.GetChild(0).tag = "ObjectMap";
		goModel.transform.GetChild (0).name = name;
		goModel.isStatic = true;
		goModel.transform.GetChild (0).gameObject.isStatic = true;
		goModel.transform.GetChild (0).gameObject.AddComponent<BoxCollider> ();
		goModel.transform.GetChild (0).GetComponent<MeshRenderer> ().material = mat;
		mat.mainTexture = LoadTexture (pathDiffuseTexture);
		goModel.transform.GetChild (0).GetComponent<MeshRenderer> ().GetPropertyBlock (mb);
		mb.SetTexture ("_BumpTex", LoadTexture(pathNormalTexture));
		mb.SetTexture ("_SpecularTex", LoadTexture(pathSpecularTexture));
		mb.SetTexture ("_Glossiness", LoadTexture(pathSmothnessTexture));
		goModel.transform.GetChild (0).GetComponent<MeshRenderer> ().SetPropertyBlock (mb);
	    return goModel;
	}

	public void isClicked(){
		isClick2 = !isClick2;

		sr.gameObject.SetActive (isClick2);
	}

	void Update ()
	{
		
		Ray ray = UnityEngine.Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			if (hit.transform.gameObject.CompareTag ("ObjectMap")) {
				if (Input.GetMouseButtonDown (0)) {
					isClick = !isClick;
					if (isClick) {
						Vector3 arrPos = hit.collider.gameObject.transform.position;
						Quaternion rot = hit.transform.rotation;
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
							if (vec1.y < -2) {
								vec1.y = hit.point.y * -1;
							}
							break;
						case "YDown":
							vec1.y = hit.point.y * -1;
							break;
						case "Z":
							vec1.z = hit.point.z;

							break;
						}

						hit.collider.transform.parent.transform.parent.transform.parent.transform.position = Vector3.MoveTowards (hit.collider.transform.parent.transform.parent.transform.parent.transform.position, vec1, Time.deltaTime * Speed * sb.value);
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
						hit.transform.parent.parent.GetChild (0).transform.rotation = Quaternion.Euler (Vector3.MoveTowards(hit.transform.parent.parent.GetChild (0).transform.rotation.eulerAngles, vec2,Time.deltaTime * SpeedRotation * (sb.value * 20)));
					}

				}
			}

		}
	}
}
