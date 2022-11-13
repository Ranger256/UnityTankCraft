using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class RamEditor : MonoBehaviour {
	public Texture2D tex;
	string mass;
	public string namePart;
	string pathXML;
	string Stifness;
	string pathTexture;
	GameObject go;
	GameObject con;
	public Shader sh;
	MeshRenderer mr;
	public bool me;

	void Awake (){
		pathXML = "media/classes/" + namePart + ".xml";
		con = GameObject.FindGameObjectWithTag ("container");
	}

	public void SavePartXML(){
		XmlDocument xmldoc = new XmlDocument ();
		XmlElement xe = xmldoc.CreateElement ("PartSettings");
		XmlElement Frame = xmldoc.CreateElement ("FRAME");
		XmlElement Mass = xmldoc.CreateElement ("Mass");
		XmlElement stifness = xmldoc.CreateElement ("Stifness");
		XmlElement pathTex = xmldoc.CreateElement ("pathTexture");
		pathTex.SetAttribute ("path", pathTexture);
		Mass.SetAttribute ("mass", mass);
		stifness.SetAttribute ("stifness",Stifness);
		Frame.AppendChild (stifness);
		xe.AppendChild (Mass);
		xe.AppendChild (Frame);
		xe.AppendChild (pathTex);
		xmldoc.AppendChild (xe);
		xmldoc.Save (pathXML);
	}

	void Update(){
	}

	public void EditorTank(){
		ReadXML ();
		go = Instantiate (Resources.Load<GameObject>(namePart));
		go.transform.parent = con.transform;
		go.transform.position = new Vector3 (0, 0, 0);
		go.transform.localScale = new Vector3(100, 100, 100);
		mr = go.GetComponent<MeshRenderer> ();
		byte[] bytes = File.ReadAllBytes (pathTexture);
		tex.LoadImage (bytes);
		mr.material.mainTexture = tex;
	}

	void ReadXML(){
		pathXML = "media/classes/" + namePart + ".xml";
		XmlReader xr = XmlReader.Create (pathXML);
		while (xr.Read ()) {

			if ((xr.NodeType == XmlNodeType.Element) && (xr.Name == "pathTexture")) {
				pathTexture = xr.GetAttribute ("path");
			}
		}
	}
}
