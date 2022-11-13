using System.Xml;
using UnityEngine.UI;
using UnityEngine;

public class ChoiseTexture : MonoBehaviour {

	[SerializeField] ScrollRect go;
	[SerializeField] ScrollRect go2;
	[SerializeField] GameObject btPfef;
	[SerializeField] Sprite inActiveButton;
	[SerializeField] Sprite inNoActiveButton;
	GameObject[] btDynDistr;
	GameObject[] btDyn;
	bool isCl = false;
	bool isCl2 = false;
	public Texture2D [] distributionsTextures;
	public int numberTexture = 0;
	public string[] pathObjects;
	public string[][] plantsTextures;
	XmlDocument xm = new XmlDocument ();
	XmlNode[] xe;
	string[] pathDistr;
	bool[][] p;
	[SerializeField] LoadMap lm;

	public void isClicked(){
		isCl = !isCl;

		if(isCl)
			isCl2 = false;

		go.gameObject.SetActive (isCl);
		go2.gameObject.SetActive (isCl2);
	}

	public void isClicked2(){
		isCl2 = !isCl2;

		if(isCl2)
			isCl = false;

		for(int i =0; i < btDyn.Length;i++){
			btDynDistr [i].GetComponent<Button> ().image.sprite = inNoActiveButton;
		
			if (p [numberTexture] [i])
				btDynDistr [i].GetComponent<Button> ().image.sprite = inActiveButton;
		}
		go2.gameObject.SetActive (isCl2);
		go.gameObject.SetActive (isCl);
	}

	void ButtF(int n){
		numberTexture = n;
		for(int i = 0; i < btDyn.Length; i++){
			btDyn [i].GetComponent<Button> ().image.sprite = inNoActiveButton;
		}
	     btDyn [numberTexture].GetComponent<Button>().image.sprite = inActiveButton;
	}

	void ButtF2(int n){
		p [numberTexture][n] = !p [numberTexture][n];

		if (p [numberTexture][n]) {
			btDynDistr [n].GetComponent<Button> ().image.sprite = inActiveButton;
		} else {
			btDynDistr [n].GetComponent<Button> ().image.sprite = inNoActiveButton;
		}
		foreach(XmlNode xn in xm.DocumentElement.ChildNodes){
			if(xn.Name == "Distribution" ){
				if (p [numberTexture][n]) {
					if(System.Convert.ToInt32(xn.Attributes["number"].Value) == numberTexture){
						xn.Attributes ["plants"].Value += (pathDistr [n] + ",");
						plantsTextures[numberTexture] = xn.Attributes ["plants"].Value.Split (new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries );
					}
//					
				} else {
					
					if (System.Convert.ToInt32 (xn.Attributes ["number"].Value) == numberTexture) {

						if (xn.Attributes ["plants"].Value == pathDistr [n] + ",") {
							xn.Attributes ["plants"].Value = xn.Attributes ["plants"].Value.Split (new string[] { pathDistr [n] }, System.StringSplitOptions.RemoveEmptyEntries) [0];
						} else {
							xn.Attributes ["plants"].Value = xn.Attributes ["plants"].Value.Split (new string[] { pathDistr [n] + "," }, System.StringSplitOptions.RemoveEmptyEntries) [0];
						}

						if (xn.Attributes ["plants"].Value == ",")
							xn.Attributes ["plants"].Value = "";
					}


					}
				}

			}

		xm.Save (lm.pathMap);

	}

	Texture2D LoadTexturePNG(string pathTexture){
		Texture2D tex = new Texture2D(256, 256);
		tex.LoadImage (System.IO.File.ReadAllBytes(pathTexture));
		return tex;
	}

	void Start(){
		int col = 0;
		pathObjects = new string[0];
		plantsTextures = new string[1][];
		pathDistr = System.IO.Directory.GetFiles ("Maps/classes/Distributions/", "*_distr.xml", System.IO.SearchOption.AllDirectories);
		distributionsTextures = new Texture2D[0];
		xe = new XmlElement[0];



		xm.Load (lm.pathMap);
		foreach(XmlNode xn in xm.DocumentElement.ChildNodes) {
			if(xn.Name == "Distribution"){
				System.Array.Resize (ref pathObjects, col + 1);
				System.Array.Resize (ref distributionsTextures, col + 1);
				if(col > 1)System.Array.Resize (ref plantsTextures[col], col + 1);
				System.Array.Resize (ref plantsTextures, col + 1);
				System.Array.Resize (ref xe, col + 1);
				pathObjects [col] = xn.Attributes ["texture"].Value;
				plantsTextures[col] = xn.Attributes ["plants"].Value.Split (new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries );
				distributionsTextures [col] = Dummiesman.ImageLoader.LoadTexture (pathObjects[col]);
				col++;
			}
		}

		btDyn = new GameObject[pathObjects.Length];
		btDynDistr = new GameObject[pathDistr.Length];
		p = new bool[pathDistr.Length][];

		for(int i = 0; i < pathObjects.Length;i++){
		    btDyn[i] = Instantiate (btPfef) as GameObject;
			btDyn [i].transform.SetParent (go.content);
			btDyn [i].GetComponentInChildren<UnityEngine.UI.Text> ().text = "";
			btDyn [i].GetComponent<Button> ().image.sprite = inNoActiveButton;
			btDyn [i].GetComponent<UnityEngine.UI.Button> ().GetComponent<RectTransform> ().localScale = new Vector3 (0.4f, 0.8f);
			int n = i;

			btDyn [i].GetComponent<UnityEngine.UI.Button> ().onClick.AddListener ( () => ButtF(n));
		}
		btDyn [0].GetComponent<Button> ().image.sprite = inActiveButton;
		for(int i = 0; i < pathDistr.Length;i++){
			p[i] = new bool[pathDistr.Length];
			btDynDistr [i] = Instantiate (btPfef) as GameObject;
			btDynDistr [i].transform.SetParent (go2.content);
			btDynDistr [i].GetComponentInChildren<Text> ().text = pathDistr [i];
			btDynDistr [i].GetComponentInChildren<Text> ().color = new Vector4(0.2f, 0, 0, 1);
			btDynDistr [i].GetComponent<Button> ().GetComponent<RectTransform> ().localScale = new Vector3 (0.8f, 0.8f);
			btDynDistr [i].GetComponent<Button> ().image.sprite = inNoActiveButton;
			int n = i;
			btDynDistr [i].GetComponent<Button> ().onClick.AddListener (() => ButtF2 (n));
		}
	}
}
