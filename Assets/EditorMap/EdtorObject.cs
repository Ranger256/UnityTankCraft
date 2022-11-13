using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class EdtorObject : MonoBehaviour {

	[SerializeField] GameObject ButtPrefab;
	[SerializeField] ScrollRect sr1;
	[SerializeField] UnityEngine.UI.ScrollRect sr2;
	[SerializeField] ScrollRect sr3;
	[SerializeField] ScrollRect sr4;
	[SerializeField] ScrollRect sr5;
	[SerializeField] ScrollRect sr6;
	[SerializeField] InputField nameOb;
	[SerializeField] InputField scalex;
	[SerializeField] InputField scaley;
	[SerializeField] InputField scalez;
	[SerializeField] GameObject go;
	[SerializeField] Text meshText;
	[SerializeField] Text diffuseTextureText;
	[SerializeField] Text normalTextureText;
	[SerializeField] Text specularTextureText;
	[SerializeField] Text glossinessTextureText;
	[SerializeField] Text iconTextureText;

	string pathMesh;
	string pathDiffuseTexture;
	string pathNormalTexture;
	string pathSpecularTexture;
	string pathGlossinessTexture;
	string pathIconTexture;
	string NameObject;
	string ScaleX;
	string ScaleY;
	string ScaleZ;
	bool[] isClick = new bool[7];

	Texture2D LoadTexture(string pathTextur){
		Texture2D tex = new Texture2D (512, 512);
		tex.LoadImage (System.IO.File.ReadAllBytes(pathTextur));
		return tex;
	}

	public void CreateButtonsOfDirectoty(string path, string sep, System.Func<int,string[], int> ButtFunc,UnityEngine.UI.ScrollRect sr){
		string[] pathObjects = System.IO.Directory.GetFiles (path, sep, System.IO.SearchOption.AllDirectories);
		GameObject[] btDyn = new GameObject[pathObjects.Length];
		for(int i = 0; i < pathObjects.Length;i++){
			btDyn[i] = Instantiate (ButtPrefab) as GameObject;
			btDyn [i].transform.SetParent (sr.content);
			btDyn [i].GetComponentInChildren<UnityEngine.UI.Text> ().text = "";
			btDyn [i].GetComponent<UnityEngine.UI.Button> ().GetComponent<RectTransform> ().localScale = new Vector2(0.5f,0.7f);
			int n = i;
			btDyn [i].GetComponent<UnityEngine.UI.Button> ().onClick.AddListener (() => ButtFunc(n, pathObjects));

		}
	}
	int mesh(int g, string[] pathob){
		pathMesh = pathob [g];
		meshText.text = pathMesh;
		return 0;
	}

	int diftex(int g, string[] pathob){
		pathDiffuseTexture = pathob [g];
		diffuseTextureText.text = pathDiffuseTexture;
		return 0;
	}

	int normaltex(int g, string[] pathob){
		pathNormalTexture = pathob [g];
		normalTextureText.text = pathNormalTexture;
		return 0;
	}

	int spectex(int g, string[] pathob){
		pathSpecularTexture = pathob [g];
		specularTextureText.text = pathSpecularTexture;
		return 0;
	}

	int glosstex(int g, string[] pathob){
		pathGlossinessTexture = pathob[g];
		glossinessTextureText.text = pathGlossinessTexture;
		return 0;
	}

	int icontex(int g, string[] pathob){
		pathIconTexture = pathob [g];
		iconTextureText.text = pathIconTexture;
		return 0;
	}
		
	void Start () {
		CreateButtonsOfDirectoty ("media/meshes/", "*.obj", mesh, sr1);
		CreateButtonsOfDirectoty ("media/textures/Diffuse", "*.png", diftex, sr2);
		CreateButtonsOfDirectoty ("media/textures/Normal", "*.png", normaltex, sr3);
		CreateButtonsOfDirectoty ("media/textures/Specular", "*.png", spectex, sr4);
		CreateButtonsOfDirectoty ("media/textures/Glossiness", "*.png", glosstex, sr5);
		CreateButtonsOfDirectoty ("media/Icons/", "*.png", icontex, sr6);
	}

	void Update(){

		NameObject = nameOb.text;
		ScaleX = scalex.text;
		ScaleY = scaley.text;
		ScaleZ = scalez.text;

	}

	public void SaveObject(){
		XmlDocument xdoc = new XmlDocument ();
		XmlElement obj = xdoc.CreateElement ("Object");
		XmlElement objSet = xdoc.CreateElement ("ObjectSettings");
		XmlElement difTex = xdoc.CreateElement ("DiffuseTexture");
		XmlElement normTex = xdoc.CreateElement ("NormalTexture");
		XmlElement glossTex = xdoc.CreateElement ("GlossinessTexture");
		XmlElement specTex = xdoc.CreateElement ("SpecularTexture");
		XmlElement meshEl = xdoc.CreateElement ("Mesh");
		XmlElement icontexEl = xdoc.CreateElement ("Icon");
		XmlElement NameEl = xdoc.CreateElement ("Name");
		XmlElement ScaleXel = xdoc.CreateElement ("ScaleX");
		XmlElement ScaleYel = xdoc.CreateElement ("ScaleY");
		XmlElement ScaleZel = xdoc.CreateElement ("ScaleZ");


		xdoc.AppendChild (obj);
		obj.AppendChild (objSet);
		objSet.AppendChild (difTex);
		objSet.AppendChild (normTex);
		objSet.AppendChild (glossTex);
		objSet.AppendChild (specTex);
		objSet.AppendChild (meshEl);
		objSet.AppendChild (icontexEl);
		objSet.AppendChild (NameEl);
		objSet.AppendChild (ScaleXel);
		objSet.AppendChild (ScaleYel);
		objSet.AppendChild (ScaleZel);

		difTex.SetAttribute ("texture",pathDiffuseTexture);
		normTex.SetAttribute ("texture", pathNormalTexture);
		specTex.SetAttribute ("texture", pathSpecularTexture);
		meshEl.SetAttribute ("mesh", pathMesh);
		glossTex.SetAttribute ("texture", pathGlossinessTexture);
		icontexEl.SetAttribute ("icon", pathIconTexture);
		NameEl.SetAttribute ("name", NameObject);
		ScaleXel.SetAttribute ("scalex", ScaleX);
		ScaleYel.SetAttribute ("scaley", ScaleY);
		ScaleZel.SetAttribute ("scalez", ScaleZ);



		xdoc.Save ("Maps/classes/" + NameObject + "_object.xml");
	}

	public void isClicked(){
		isClick [6] = !isClick [6];
		go.SetActive (isClick[6]);
	}

	public void isClicked1(){
		isClick [0] = !isClick [0];
		sr2.gameObject.SetActive (isClick[0]);
	}

	public void isClicked2(){
		isClick [1] = !isClick [1];
		sr1.gameObject.SetActive (isClick[1]);
	}
	public void isClicked3(){
		isClick [2] = !isClick [2];
		sr3.gameObject.SetActive (isClick[2]);
	}
	public void isClicked4(){
		isClick [3] = !isClick [3];
		sr4.gameObject.SetActive (isClick[3]);
	}
	public void isClicked5(){
		isClick [4] = !isClick [4];
		sr5.gameObject.SetActive (isClick[4]);
	}
	public void isClicked6(){
		isClick [5] = !isClick [5];
		sr6.gameObject.SetActive (isClick[5]);
	}

}
