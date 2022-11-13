using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateHeight : MonoBehaviour {

	[SerializeField] Terrain terrain;
	int resolution = 0;
	[SerializeField] GameObject treePref;
	Texture2D tex;
	string heightMapPath;
	const float size = 0.175f;
	float sizeNew = 0.6f;
	float sizeBrush;
	float[,] height1;
	byte[] map;
	bool isClick = false;
	public UnityEngine.UI.Scrollbar sc;
	public UnityEngine.UI.Scrollbar sc2;
	public GameObject go2;
	[SerializeField] LoadMap lm;


	void Awake(){
		resolution = 512;
		height1 = new float[resolution, resolution];
		terrain.terrainData.heightmapResolution = resolution;
	}

	void Start () {

		heightMapPath = lm.heightPath;

		sizeNew = size;
		sc.value = size;		
		//tex = new Texture2D (resolution, resolution);

		//tex.Apply ();
		//System.IO.File.WriteAllBytes (heightMapPath, tex.EncodeToPNG());

		//System.IO.File.WriteAllBytes (heightMapPath, tex.EncodeToPNG());


	//	Generate ();

	}

	public void Generate(){		
		tex = Dummiesman.ImageLoader.LoadTexture (heightMapPath);

		for(int i = 0; i < resolution; i++){
			for(int j = 0; j < resolution; j++){
				
				height1 [i, j] = tex.GetPixel(i, j).r * size;
			}
		}
		terrain.terrainData.SetHeights (0, 0, height1);
	}

	public void IsClickedMetod(){
		isClick = !isClick;

		if (isClick) {
			go2.SetActive (true);
		} else {
			go2.SetActive (false);
		}
	}

	void Update ()
	{





		if (isClick) {

			sizeNew = sc.value;

			sizeBrush = sc2.value * 50;
			Ray ray = UnityEngine.Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				if (hit.collider.gameObject.CompareTag ("painter")) {
					//hit.transform.
					if (Input.GetMouseButton (0)) {

						for (int i = 0; i < sizeBrush; i++) {
							for (int j = 0; j < sizeBrush; j++) {
								
								int x = Mathf.FloorToInt (tex.height - hit.textureCoord.y * tex.height) * -1 +i;
								int y = Mathf.FloorToInt (tex.width - hit.textureCoord.x * tex.width) * -1 + j;

								Color pixel = tex.GetPixel (x, y);

								if (sc.value > 0.5f) {
									pixel.r += (sizeNew - 0.5f) *  0.1f;

								} else {
									pixel.r -= sizeNew ;
									

								}
									
								tex.SetPixel (x, y, pixel);
							}

						}
						tex.Apply ();

						//Smothing ();

						System.IO.File.WriteAllBytes (heightMapPath, tex.EncodeToPNG ());

						Generate ();



					}
				}
			}


		}


	}
}

