using System.Xml;
using System.Collections.Generic;
using UnityEngine;

public class LoadMapGame : MonoBehaviour {
	[SerializeField] LoadMap lm;
	[SerializeField] GenerateMaterialTerrain gmt;
	System.Xml.XmlDocument xdoc;
	string[] x = new string[0];
	int col = 0;

	// Use this for initialization
	void Start () {
		xdoc = new System.Xml.XmlDocument ();
		xdoc.Load ("Maps/levels/Test2/Test2_map.xml");
		foreach(XmlNode xn in xdoc.DocumentElement.ChildNodes){
			if(xn.Name == "MaterialTextureMap"){
				System.Array.Resize (ref x, col + 1);
				x [col] = xn.Attributes ["texture"].Value;
				col++;
			}
		}
		gmt.allMasksTextures = x;
		lm.Loading (Mediator.nameMap);

		//gmt.TexturePaintTerrain ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
