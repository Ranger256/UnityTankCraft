using System.Xml;
using UnityEngine;
using System;

public class LoadMap : MonoBehaviour {
	[SerializeField] Material mat;
	[SerializeField] GenerateMaterialTerrain gm;
	[SerializeField] UnityEngine.UI.ScrollRect sr;
	[SerializeField] GameObject btpref;
	[SerializeField] GenerateHeight gh;
	[SerializeField] GenerateMaterialTerrain gmt;
	[SerializeField] AddDistribution adDistr;
	MaterialPropertyBlock mp;
	int col;
	GameObject[] btdyn;
	bool isclicked2;
	public string pathMap = "Maps/levels/Test2/Test2_map.xml";
	public string iconPath;
	public string heightPath;
	public string[] pathMasksText;
	public string pathObjectXml;
	string[] pathMaps;



	void Awake(){

		pathMasksText = new string[0];
		mp = new MaterialPropertyBlock ();
		LoadMapString (pathMap);
	}

	public void Loading(string path){
		LoadMapString (path);
		LoadObjectMapXML (pathObjectXml);
		gh.Generate ();

	}
	void ButtF(int n){
		Loading (pathMaps[n]);
		gmt.StartPaintTerrain ();
		gmt.TexturePaintTerrain ();
		adDistr.CreateDistr ();
	}

	void Start(){
		
		pathMaps = System.IO.Directory.GetFiles ("Maps/levels/", "*_map.xml",  System.IO.SearchOption.AllDirectories);
		btdyn = new GameObject[pathMaps.Length];

		for(int i = 0; i < pathMaps.Length; i++){
			if(btpref != null)
			btdyn[i] = Instantiate (btpref) as GameObject;

			if(btdyn != null){
			btdyn [i].transform.parent = sr.content;
			btdyn [i].transform.localScale = new Vector3 (0.8f, 0.8f, 0);
			int n = i;
			btdyn [i].GetComponent<UnityEngine.UI.Button> ().onClick.AddListener ( () => ButtF(n));
			}
		}
	}

	public void isClicked2(){
		isclicked2 = !isclicked2;

		sr.gameObject.SetActive (isclicked2);
	}

	void LoadMapString(string PatmMap){
		XmlDocument xdoc = new XmlDocument ();
		xdoc.Load (PatmMap);

		foreach(XmlNode xn in xdoc.DocumentElement.ChildNodes){
			switch(xn.Name){
			case "Icon":
				iconPath = xn.Attributes ["path"].Value;
				break;
			case"HeightMap":
				heightPath = xn.Attributes ["path"].Value;
				break;
			case "MaterialTextureMap":
				Array.Resize (ref pathMasksText, col + 1);
				pathMasksText [col] = xn.Attributes ["texture"].Value;
				col++;
				break;
			case "ObjectXml":
				pathObjectXml = xn.Attributes ["path"].Value;
				break;
			}
		}
	}

	GameObject[] LoadObjectMapXML(string pathXml){
		string pathModel = "";
		string pathDiffuseTexture = "";
		string pathNormalTexture = "";
		string pathSmothnessTexture = "";
		string pathSpecularTexture = "";
		string name = "";
		int col = 0;
		GameObject[] go = new GameObject[0];
		XmlDocument xdoc = new XmlDocument ();
		xdoc.Load (pathXml);

		foreach(XmlNode node in xdoc.DocumentElement.ChildNodes){
			if(node.Name == "Object"){
				Array.Resize (ref go, col + 1);
				XmlReader xr = XmlReader.Create (node.Attributes["object"].Value);
				while(xr.Read()){
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
				}
				go [col] = OBJLoader.LoadOBJFile (pathModel);
				go [col].transform.GetChild(0).tag = "ObjectMap";
				go [col].transform.GetChild (0).name = name;
				go [col].transform.GetChild(0).position = new Vector3 (Convert.ToSingle(node.Attributes["PosX"].Value), Convert.ToSingle(node.Attributes["PosY"].Value), Convert.ToSingle(node.Attributes["PosZ"].Value));
				go [col].transform.GetChild(0).rotation = Quaternion.Euler (new Vector3 (Convert.ToSingle(node.Attributes["RotX"].Value), Convert.ToSingle(node.Attributes["RotY"].Value), Convert.ToSingle(node.Attributes["RotZ"].Value)));
				go[col].transform.GetChild(0).localScale = new Vector3 (Convert.ToSingle(node.Attributes["ScaleX"].Value), Convert.ToSingle(node.Attributes["ScaleY"].Value), Convert.ToSingle(node.Attributes["ScaleZ"].Value));
				go [col].transform.GetChild (0).gameObject.AddComponent<BoxCollider> ();
				go [col].transform.GetChild (0).GetComponent<MeshRenderer> ().material = mat;
				mat.mainTexture = Dummiesman.ImageLoader.LoadTexture (pathDiffuseTexture);
				go[col].transform.GetChild (0).GetComponent<MeshRenderer> ().GetPropertyBlock (mp);
				if (pathNormalTexture != "" && pathNormalTexture != " ") {
					mp.SetTexture ("_BumpTex", TGALoa.LoadTGA(pathNormalTexture));
				}
				if(pathSpecularTexture != "" && pathSpecularTexture != " "){
					mp.SetTexture ("_SpecularTex", TGALoa.LoadTGA(pathSpecularTexture));
				}
				if (pathSmothnessTexture != "" && pathSmothnessTexture != " ") {
					mp.SetTexture ("_Glossiness", TGALoa.LoadTGA(pathSmothnessTexture));
				}
				go[col].transform.GetChild (0).GetComponent<MeshRenderer> ().SetPropertyBlock (mp);
				col++;
			}
		}
			
		return go;
	}

	public void Awake1(){
	//	LoadObjectMapXML (pathObjectXml);

	}
		
}
