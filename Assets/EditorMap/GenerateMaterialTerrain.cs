using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMaterialTerrain : MonoBehaviour {

	public Terrain ter;
	bool isClick = false;
	int radius = 1;
	public Texture2D[] masks;
	int size;
	int layers;
	float[,,] alphaMaps;
	public string[] allMasksTextures;
	public GameObject go;
	MeshRenderer mr;
	public UnityEngine.UI.Scrollbar sc1;
	public GameObject go2;
	public UnityEngine.UI.Scrollbar sc2;
	float scaleBrush;
	float Opacity;
	int colText = 0;
	int numTex = 1;
	[SerializeField] UnityEngine.UI.ScrollRect sr1;
	[SerializeField] UnityEngine.UI.ScrollRect sr2;
	[SerializeField] GameObject pb;
	bool isClick3;
	bool isClick4;
	[SerializeField] GameObject go4;
	GameObject[] btM;
	int[] lartex;
	Texture2D texmat;
	[SerializeField] LoadMap lm;

	void OnApplicationQuit(){
		SplatPrototype[] sp = new SplatPrototype[0];			
		ter.terrainData.splatPrototypes = sp;
	}

	void ButtFTexture(int n){
		lartex [numTex] = n;
		TexturePaintTerrain ();
	}

	public void isClicked3(){
		isClick3 = !isClick3;

		if (isClick3) {
			sr1.gameObject.SetActive (true);

		} else {
			sr1.gameObject.SetActive (false);
	
		}
	}

	public void isClicked4(){
		isClick4 = !isClick4;

		if (isClick4) {
			sr2.gameObject.SetActive (true);
		} else {
			sr2.gameObject.SetActive (false);
		}
	}

	public void isClicked (){
		isClick = !isClick;

		if (isClick == false) {
			TexturePaintTerrain ();
			go.SetActive (false);
			ter.gameObject.SetActive (true);
			//go2.SetActive (false);
			go4.SetActive (false);
		} else {
			go.SetActive (true);
			ter.gameObject.SetActive (false);
			mr.material.mainTexture = masks [numTex];
		    //go2.SetActive (true);
			go4.SetActive (true);
		}

	}

	public void IsClicked2(){
		Texture2D tex = new Texture2D (512, 512);
		colText++;
		for(int i = 0; i < 512; i++){
			for(int j = 0; j < 512; j++){
				
				tex.SetPixel (i, j, Color.blue);
			}
		}

		string pathNewText = "Maps/levels/" + "" + @"\" + ("Mask" + System.Convert.ToString(colText) + "_texture" + ".png");
		for(int i = 0; i < allMasksTextures.Length; i++){

			if (pathNewText == allMasksTextures [i]) {
				colText++;
				pathNewText = "Maps/levels/" + "" + @"\" + ("Mask" + System.Convert.ToString(colText) + "_texture" + ".png");
			} 
		}			

		System.IO.File.WriteAllBytes (pathNewText, tex.EncodeToPNG());
		for(int i = 0; i <btM.Length; i++){
			Destroy (btM[i]);
		}
		StartPaintTerrain ();
	}

	void Paint(){
		//scaleBrush = System.Convert.ToInt32(sc1.value);
		//Opacity = System.Convert.ToInt32(sc2.value);

		if (isClick) {

			scaleBrush = sc2.value * 50;
			Opacity = sc1.value;

			Ray ray = UnityEngine.Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				
				if (hit.collider.gameObject.CompareTag ("painter")) {

					//hit.transform.
					if (Input.GetMouseButton (0)) {
						for (int i = 0; i < scaleBrush; i++) {
							for (int j = 0; j < scaleBrush; j++) {

								int x = Mathf.FloorToInt (masks[numTex].height - hit.textureCoord.y * masks[numTex].height) * -1 +i;
								int y = Mathf.FloorToInt (masks[numTex].width - hit.textureCoord.x * masks[numTex].width) * -1 + j;

								Color pixel = Color.black;

								if (Opacity > 0.5f) {
									pixel.r = 1;
									pixel.b = 0;
								} else {
									pixel.r = 0;
									pixel.b = 1;
								}

								masks[numTex].SetPixel (y, x, pixel);
							}

						}
						masks[numTex].Apply ();


						System.IO.File.WriteAllBytes (allMasksTextures [numTex], masks [numTex].EncodeToPNG ());


						go.GetComponent<MeshRenderer> ().material.mainTexture = masks [numTex];


					}
				}
			}
		}

	}
		
	void ButtFunc (int n){
		numTex = n;
		mr.material.mainTexture = masks[numTex];
	}

	public void StartPaintTerrain(){


		
		allMasksTextures = lm.pathMasksText;


		masks = new Texture2D[allMasksTextures.Length];

		for(int i = 0; i < allMasksTextures.Length; i++){
			masks [i] = new Texture2D (128, 128);
			masks [i].LoadImage (System.IO.File.ReadAllBytes(allMasksTextures[i]));
		}

		btM = new GameObject[masks.Length];
		int[] numb = new int[masks.Length];

		for(int i = 0; i < btM.Length; i++){
			btM [i] = Instantiate (pb) as GameObject;

			btM [i].transform.SetParent (sr2.content);

			btM [i].GetComponent<Button> ().GetComponent<RectTransform> ().localScale = new Vector3 (0.6f, 0.6f, 0);

	
			btM [i].GetComponentInChildren<Text> ().text = "";

			Image im = btM[i].GetComponent<Button>().GetComponent<Image>();
			im.sprite = Sprite.Create (masks[i], new Rect(0, 0, 512, 512), new Vector2());

			btM [i].GetComponent<Button> ().image = im;

			int nu = i;
			btM [i].GetComponent<Button> ().onClick.AddListener (() => ButtFunc(nu));

		}

		sr2.content.localScale = new Vector3 (1, masks.Length * 0.5f, 0);
	}
		

	public void TexturePaintTerrain(){
		for(int i = 0; i < masks.Length; i++){
			masks[i].LoadImage (System.IO.File.ReadAllBytes(allMasksTextures[i]));
		}


					for (int z = 0; z < size; z++)
						{
			for (int x = 0; x < size; x++) {
				float sum = 0;

				for (int layer = 0; layer < layers; layer++) {
					sum += alphaMaps [z, x, layer];
				}

				for (int layer = 0; sum > 1.01 && layer < layers; layer++) {
					sum -= alphaMaps [z, x, layer];
					alphaMaps [z, x, layer] = 0;
				}

				for (int layer = 0; layer < layers; layer++) {
					alphaMaps [z, x, layer] = 0;
					//alphaMaps [z, x, 1] = masks [0].GetPixel (-x, -z).b;
					//alphaMaps [z, x, 2] = masks [2].GetPixel (-x, -z).r;

					for(int i =0; i < masks.Length / 2; i++){
						alphaMaps [z, x, 0] = 1;

						if(masks[i].GetPixel(-x, -z).r > 0){
							alphaMaps [z, x, i] = masks [i].GetPixel (-x, -z).r;
						}
					}
					alphaMaps [z, x, layer] *= sum;
				}
			}
		}





		ter.terrainData.SetAlphamaps (0, 0, alphaMaps);

		//ter.Flush ();

	
	}

	SplatPrototype[] SplatP(){
		string[] allFilesTextures = System.IO.Directory.GetFiles("media/textures/Diffuse/","*_maptexture.png" , System.IO.SearchOption.AllDirectories);



		GameObject[] btn = new GameObject[allFilesTextures.Length];


		Texture2D[] texts = new Texture2D[allFilesTextures.Length];


		SplatPrototype[] splat = new SplatPrototype[allFilesTextures.Length + 1];

		for(int i = 0; i < splat.Length; i++){
			splat [i] = new SplatPrototype ();
		}
			
		for(int i = 0; i < allFilesTextures.Length; i++){

			texts [i] = new Texture2D (128, 128);

			texts [i].LoadImage (System.IO.File.ReadAllBytes(allFilesTextures[i]));

			splat [i].texture = texts [i];

			if(pb != null)
				btn [i] = Instantiate (pb) as GameObject;
			
			if (btn [i] != null) {
				

				if (sr1.gameObject != null)
					btn [i].transform.SetParent (sr1.content);

				btn [i].transform.localScale = new Vector3 (1, 1, 0);

				int n = i;
				btn [i].GetComponent<Button> ().onClick.AddListener (() => ButtFTexture (n));
			}

		}
		splat [allFilesTextures.Length].texture = texts [0];

		return splat;
	}

	void Awake(){
		enabled = true;
		
		ter.terrainData.splatPrototypes = SplatP() ;

		size = ter.terrainData.alphamapResolution;
		layers = ter.terrainData.alphamapLayers;
		alphaMaps = new float[size,size,layers];
		if(go != null) 
     	mr = go.GetComponent<MeshRenderer> ();
	}
	void Start () {
		lartex = new int[4];
		for(int i =0; i < lartex.Length; i++){
			lartex [i] = i;
		}

		//StartPaintTerrain ();
		//TexturePaintTerrain ();
	}
	
	// Update is called once per frame
	void Update () {
		//Paint ();
	}
}
