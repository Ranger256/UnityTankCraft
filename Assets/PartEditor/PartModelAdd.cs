using UnityEngine.UI;
using UnityEngine;

public class PartModelAdd : MonoBehaviour {
	[SerializeField] ScrollRect sr;
	[SerializeField] ScrollRect sr2;
	[SerializeField] GameObject btnPref;
	[SerializeField] Material mat;
	[SerializeField] GameObject arrPref;
	[SerializeField] GameObject whPref;
	[SerializeField] float Speed;
	[SerializeField] float SpeedRot;
	[SerializeField] Scrollbar sb;
	GameObject go;
	int mult = 1;
	GameObject[] btn;
	GameObject[] btn2;
	string[] files;
	bool isClick;
	bool isRotIsPos;
	GameObject arr;
	GameObject wh;
	public static string activeTexture;
	string[] namesTexture;
	bool isclicked;

	public void isClicked(){
		isclicked = !isclicked;

		sr2.gameObject.SetActive (isclicked);
	}


	void ButtF(int n){
		GameObject go = OBJLoader.LoadOBJFile (files[n]);
		go.transform.GetChild (0).gameObject.AddComponent<BoxCollider> ();
		go.transform.GetChild (0).gameObject.AddComponent<Outline> ().OutlineColor = Color.yellow;
		go.transform.GetChild (0).gameObject.GetComponent<Outline> ().enabled = false;
		go.transform.GetChild(0).gameObject.GetComponent<MeshRenderer> ().material = mat;
		go.transform.GetChild (0).gameObject.name = files [n];
		go.transform.GetChild(0).gameObject.tag = "parts";
	}

	void ButtFTexture(int n){
		activeTexture = namesTexture [n];

		print (activeTexture);

		go.GetComponent<MeshRenderer> ().material.mainTexture = TexturesLoader.texturesDiffuse[namesTexture[n]];
	}
		

	void CreateButtons(){
		files = System.IO.Directory.GetFiles ("media/meshes/", "*.obj", System.IO.SearchOption.AllDirectories);
		btn = new GameObject[files.Length];

		for(int i = 0; i < btn.Length; i++){
			btn [i] = Instantiate (btnPref);
			btn [i].transform.localScale = new Vector3 (0.5f, 0.85f, 0);
			btn [i].transform.parent = sr.content.transform;
			int n = i;
			btn [i].GetComponent<Button> ().onClick.AddListener (() => ButtF(n));
		}
	}

	void CreateButtonsTextures(){
		namesTexture = System.IO.Directory.GetFiles ("media/textures/Diffuse/", "*_parttexture.png", System.IO.SearchOption.AllDirectories);
		btn2 = new GameObject[namesTexture.Length];

		for(int i = 0; i < btn2.Length; i++){
			btn2 [i] = Instantiate (btnPref);
			btn2 [i].transform.localScale = new Vector3 (0.5f, 0.85f, 0);
			btn2 [i].transform.parent = sr2.content.transform;
			int n = i;
			btn2 [i].GetComponent<Button> ().onClick.AddListener (() => ButtFTexture(n));
		}
	}

	void TranlsPart(){
		Ray ray = UnityEngine.Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			if (hit.transform.gameObject.CompareTag ("parts")) {
				if (Input.GetMouseButtonDown (0)) {
					isClick = !isClick;
					if (isClick) {
						go = hit.collider.gameObject;
						Vector3 arrPos = hit.collider.gameObject.transform.position;
						Quaternion rot = hit.transform.rotation;
						arrPos.y += 2.7f;
						hit.collider.gameObject.GetComponent<Outline> ().enabled = true;
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
						go = null;
						hit.collider.gameObject.GetComponent<Outline> ().enabled = false;
						Destroy (arr);
						Destroy (wh);
						hit.transform.gameObject.isStatic = true;
						hit.transform.GetChild (0).gameObject.isStatic = true;
					}
				}
			} 
			if (isClick) {
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

	void Start () {
		namesTexture = System.IO.Directory.GetFiles ("media/textures/Diffuse/", "*_parttexture.png", System.IO.SearchOption.AllDirectories);

		CreateButtonsTextures ();
		CreateButtons ();
	}
	
	// Update is called once per frame
	void Update () {
		TranlsPart ();
	}
}
