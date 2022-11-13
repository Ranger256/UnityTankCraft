using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatingIconMap : MonoBehaviour {
	[SerializeField] GenerateMaterialTerrain gm;
	[SerializeField] UnityEngine.UI.Image im;
	Texture2D[] tex;
	Texture2D icon;
	[SerializeField]LoadMap lm;

	void Awake(){
		icon = new Texture2D (512,512);
	}

	public void CreateIcon(){
		tex = new Texture2D[gm.ter.terrainData.splatPrototypes.Length];

		for(int i = 0; i < gm.ter.terrainData.splatPrototypes.Length; i++){
			tex [i] = gm.ter.terrainData.splatPrototypes [i].texture;
		}

		for (int w = 0; w < 512; w++) {
			for (int h = 0; h < 512; h++) {
				icon.SetPixel (w, h,tex[0].GetPixel(w, h));
			}
		}

		for(int i = 0; i < gm.masks.Length / 2; i++){
			
			for(int w = 0; w < gm.masks[i].width;w++){
				for(int h =0 ; h < gm.masks[i].height; h++){
					if (gm.masks [i].GetPixel (w, h).r > 0) {
						icon.SetPixel (w, h, tex[i].GetPixel(w, h));
					}

				}
			}
		}
		icon.Apply ();
		System.IO.File.WriteAllBytes (lm.iconPath, icon.EncodeToPNG());

		im.sprite = Sprite.Create (icon, new Rect(0, 0, 512, 512), new Vector2());
	}

	// Use this for initialization
	void Start () {
		im.sprite = Sprite.Create (Dummiesman.ImageLoader.LoadTexture(lm.iconPath), new Rect(0, 0, 512, 512), new Vector2());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
