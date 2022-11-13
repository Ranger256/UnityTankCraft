using UnityEngine.UI;
using UnityEngine;
using System.Xml;

public class AddDistribution : MonoBehaviour {
	[SerializeField] ChoiseTexture ct;
	[SerializeField] Scrollbar sizeBrush;
	[SerializeField] Scrollbar Opacity;
	[SerializeField] GameObject terrain;
	[SerializeField] GameObject PaintPlit;
	[SerializeField] GameObject PanelIn;
	[SerializeField] Material mat;
	[SerializeField] Terrain ter;
	[SerializeField] Texture2D tex;
	MaterialPropertyBlock mb;
	bool iscl1;
	float[,] r;
	int px = 0;
	int py = 0;
	int n = 8;
	int ny = 8;
	string [] pathtextures;
	string[] pathtextures2;
	float sizeBrus;

	public void isClicked1(){
		iscl1 = !iscl1;

		terrain.SetActive (!iscl1);
		PaintPlit.SetActive (iscl1);
		PanelIn.SetActive (iscl1);

		if (!iscl1) {
			System.IO.File.WriteAllBytes (ct.pathObjects [ct.numberTexture], ct.distributionsTextures [ct.numberTexture].EncodeToPNG ());
			CreateDistr ();
		} else {
			PaintPlit.GetComponent<MeshRenderer> ().material.mainTexture = ct.distributionsTextures [ct.numberTexture];
		}
	}
		
	void Awake(){
		px = n;
		py = ny;
		mb = new MaterialPropertyBlock ();

		//CreateDistr ();
	}

	GameObject CreateTree(string xmlTreeConfig, float height, Vector3 positionTree, Texture2D tex){
		string pathMeshTree = "";
		string pathDiffusesTree = "";
		Vector3 scaleTree = new Vector3 (1, height * 5, 10);

		XmlDocument xdoc = new XmlDocument ();
		xdoc.Load (xmlTreeConfig);
		foreach (XmlNode xn in xdoc.DocumentElement.ChildNodes) {
			switch(xn.Name){
			case "Mesh":
				pathMeshTree = xn.Attributes ["mesh"].Value;
				break;
			case "Diffuse":
				pathDiffusesTree = xn.Attributes ["texture"].Value;
				break;
			case "ScaleX":
//				scaleTree.x = System.Convert.ToSingle (xn.Attributes["ScaleX"].Value);
				break;
			case "ScaleY":
				//scaleTree.y = System.Convert.ToSingle (xn.Attributes["ScaleY"].Value) * height;
				break;
			case "ScaleZ":
				//scaleTree.z = System.Convert.ToSingle (xn.Attributes["ScaleZ"].Value);
				break;
			}
		}

		GameObject tree = OBJLoader.LoadOBJFile(pathMeshTree);

		tree.transform.SetParent (ter.transform);

			tree.transform.GetChild (0).GetComponent<MeshRenderer> ().material.mainTexture = tex;
		tree.transform.GetChild (0).transform.position = positionTree;
		tree.transform.GetChild (0).transform.localScale = scaleTree;
		tree.isStatic = true;
		tree.transform.GetChild (0).gameObject.isStatic = true;

		return tree;

	}

	void CreateXMLConfigTree(){
		XmlDocument xdoc = new XmlDocument ();

		XmlElement xe = xdoc.CreateElement ("Tree");
		XmlElement xMeshTree = xdoc.CreateElement ("Mesh");
		XmlElement xMeshLOD = xdoc.CreateElement ("MeshLOD");
		XmlElement xTextureDiffuse = xdoc.CreateElement ("Diffuse");
		XmlElement xTextureNormal = xdoc.CreateElement ("Normal");
		XmlElement ScaleX = xdoc.CreateElement ("ScaleX");
		XmlElement ScaleY = xdoc.CreateElement ("ScaleY");
		XmlElement ScaleZ = xdoc.CreateElement ("ScaleZ");

		xdoc.AppendChild (xe);
		xe.AppendChild (xMeshTree);
		xe.AppendChild (xMeshLOD);
		xe.AppendChild (xTextureDiffuse);
		xe.AppendChild (xTextureNormal);
		xe.AppendChild (ScaleX);
		xe.AppendChild (ScaleY);
		xe.AppendChild (ScaleZ);

		xdoc.Save ("Maps/classes/Distributions/pl_distr.xml");
	}

	void Paint(){
		sizeBrus = sizeBrush.value * 50;

		Ray ray = UnityEngine.Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			if (hit.collider.gameObject.CompareTag ("painter")) {
				//hit.transform.
				if (Input.GetMouseButton (0)) {
					
					for (int i = 0; i < sizeBrus; i++) {
						for (int j = 0; j < sizeBrus; j++) {
							int x = Mathf.FloorToInt (ct.distributionsTextures[ct.numberTexture].height - hit.textureCoord.y * ct.distributionsTextures[ct.numberTexture].height) * -1 +i;
							int y = Mathf.FloorToInt (ct.distributionsTextures[ct.numberTexture].width - hit.textureCoord.x * ct.distributionsTextures[ct.numberTexture].width) * -1 + j;

							Color pix = ct.distributionsTextures[ct.numberTexture].GetPixel(y, x);

							if(Opacity.value > 0.5f){
								pix.r += Opacity.value;
							}

							ct.distributionsTextures[ct.numberTexture].SetPixel (y, x, pix);
						}

					}
					ct.distributionsTextures[ct.numberTexture].Apply ();

					//Smothing ();

				//	System.IO.File.WriteAllBytes (ct.pathObjects[ct.numberTexture], ct.distributionsTextures[ct.numberTexture].EncodeToPNG ());
					PaintPlit.GetComponent<MeshRenderer> ().material.mainTexture = ct.distributionsTextures [ct.numberTexture];
				}
			}
		}
	}

	public void CreateDistr(){
		for (int i = 0; i < 512; i++) {
			if(px == i){
				px += n;
				for(int j = 0; j < 512; j++){
					if (py == j) {
						if (ct.distributionsTextures [ct.numberTexture].GetPixel (i, j).r > 0f) {
							foreach(string pathPlant in ct.plantsTextures[ct.numberTexture]){
								CreateTree (pathPlant, ct.distributionsTextures [ct.numberTexture].GetPixel (i, j).b * + 0.7f, new Vector3 (i, ter.terrainData.GetHeight(i,j), j), tex);
							}
						}
						py += ny;
					}
					if(py > 510){
						py = 0;
					}
				}
			}

			if(px > 510){
				px = 0;
			}
		}
	}

	void Start () {
		r = new float[512, 512];


	}
	
	// Update is called once per frame
	void Update () {
		if (iscl1) {
			Paint ();



		} else {
			
		}
	}
}
