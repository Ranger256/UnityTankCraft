using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using System;

public class EditorTerrain : MonoBehaviour {
	public string _tdname;
	public int _tdsize;
	public string path;
	TerrainData _td;
	public Text ie;
	private bool isTer = false;
	public float[,] hieghts;
	public string heighmap;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		path = ie.text;
	}

	public	void CreateTerrain(){
		TerrainData _td = new TerrainData();
		GameObject _terrain = (GameObject)Terrain.CreateTerrainGameObject (_td);
		FileStream fs = new FileStream (heighmap, FileMode.Open, FileAccess.Read);

		BinaryReader br = new BinaryReader (fs);

		int  resolution=500;
		float[,] heights = new float[resolution,resolution]; // Создаём массив вершин
		for (int x = 0; x < resolution; x++) 
		{
			for (int y = 0; y < resolution; y++) {
				
				heights [x, y] = (float)br.ReadSingle();
			}
		}


		_td.name = _tdname;
		_td.size = new Vector3(_tdsize, _tdsize, _tdsize);
		_td.heightmapResolution = resolution; // Задаём разрешение (кол-во высот)
		_td.SetHeights(0, 0, heights); // И, наконец, применяем нашу карту высот (heights)


		isTer = true;
	}

	public void SaveLevel(){
		SavingClass sc = new SavingClass (_tdname, _tdsize, isTer);
		XmlSerializer xs = new XmlSerializer (typeof(SavingClass));
		using(FileStream fs = new FileStream(path + ".xml", FileMode.Create)){
			xs.Serialize (fs,sc);
			fs.Close ();
		}
	}

	public void LoadLevel(){
		XmlSerializer xs = new XmlSerializer (typeof(SavingClass));
		FileStream fs = new FileStream (path + ".xml", FileMode.Open);
		SavingClass sc1 = xs.Deserialize (fs) as SavingClass;
		fs.Close ();
		_tdsize = sc1._tdSize;
		_tdname = sc1._tdCom;
		isTer = sc1.IsTer;

		if(isTer){
			CreateTerrain ();
		}
	}
}
