using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System;



public class EditorTank : MonoBehaviour {
	public Texture2D tex;
	string part;
	public string namePart;
    string pathTextur;
	public string partTank;
	string pathXML;
	string AccelerationSpeeed;
	Bashn b;
	string ac;
	string ac1;
	string ac2;
	string speedMax;
	string speedMin;
	string mass = "50";
	public GameObject go;
	public bool me;

	public void SaveTank(){
		pathXML = "media/classes/" + namePart + ".xml";
		XmlDocument xmldoc = new XmlDocument ();
		XmlElement xe = xmldoc.CreateElement ("PartSettings");
		XmlElement tank = xmldoc.CreateElement ("PartTank");
		XmlElement namepart = xmldoc.CreateElement ("NamePart");
		XmlElement TowerSetings = xmldoc.CreateElement ("TOWER");
		XmlElement Acceleration = xmldoc.CreateElement ("Acceleration");
		XmlElement SpeedMax = xmldoc.CreateElement ("SpeedMax");
		XmlElement SpeedMin = xmldoc.CreateElement ("SpeedMin");
		XmlElement Mass = xmldoc.CreateElement ("Mass");
		Mass.SetAttribute ("mass", mass);
		SpeedMax.SetAttribute ("SpeedMax",ac1);
		SpeedMin.SetAttribute ("SpeedMin",ac2);
		Acceleration.SetAttribute ("Acceleration",AccelerationSpeeed);
		tank.SetAttribute ("PartTank", partTank);
		namepart.SetAttribute ("name", namePart);
		XmlElement pathTexture = xmldoc.CreateElement ("pathTexture");
		pathTexture.SetAttribute ("path", pathTextur);
		xe.AppendChild (TowerSetings);
		xe.AppendChild (tank);
		xe.AppendChild (namepart);
		xe.AppendChild (pathTexture);
		xe.AppendChild (Mass);
		TowerSetings.AppendChild (Acceleration);
		TowerSetings.AppendChild (SpeedMax);
		TowerSetings.AppendChild (SpeedMin);
		xmldoc.AppendChild (xe);
		xmldoc.Save (pathXML);
	}

	public void EditorsTank(){
		ReadXML ();

		go = Instantiate (Resources.Load<GameObject>(partTank));
		b = go.GetComponent<Bashn> ();
		go.transform.parent = GameObject.FindGameObjectWithTag ("container").transform;
		go.transform.localScale = new Vector3 (40, 40, 40);
		go.transform.localPosition = new Vector3 (0, 0, 0);
		MeshRenderer mr = go.GetComponent<MeshRenderer> ();
		byte[] ib = File.ReadAllBytes (pathTextur);
		tex.LoadImage (ib);
		mr.material.mainTexture = tex;
		b.K = Convert.ToSingle (ac);
		b.maxspeed = Convert.ToSingle (ac1);
		b.minspeed = Convert.ToSingle (ac2);
	}
	void ReadXML(){
		pathXML = "media/classes/" + namePart + ".xml";
		XmlReader xr = XmlReader.Create (pathXML);
		while(xr.Read()){
			//if((xr.NodeType == XmlNodeType.Element) && (xr.Name == "NamePart")){
			//	namePart = xr.GetAttribute ("name");
			//}
			if((xr.NodeType == XmlNodeType.Element) && (xr.Name == "pathTexture")){
				pathTextur = xr.GetAttribute ("path");
			}
			if((xr.NodeType == XmlNodeType.Element) && (xr.Name == "PartTank")){
				partTank = xr.GetAttribute ("PartTank");
			}

			if((xr.NodeType == XmlNodeType.Element) && (xr.Name == "Acceleration")){
				ac = xr.GetAttribute ("Acceleration");
			}

			if((xr.NodeType == XmlNodeType.Element) && (xr.Name == "SpeedMax")){
				ac1 = xr.GetAttribute ("SpeedMax");
			}

			if((xr.NodeType == XmlNodeType.Element) && (xr.Name == "SpeedMin")){
				ac2 = xr.GetAttribute ("SpeedMin");
			}
		}
	}
}
