using System;
using System.Xml;
using UnityEngine;

public class LoadTank : MonoBehaviour {
	public string pathTank;
	[SerializeField] Material matPart;
	GameObject go;

	public void LoadingTank(){
		string texture = "";
		XmlDocument xdoc = new XmlDocument ();
		xdoc.Load (pathTank);
		foreach(XmlNode xn in xdoc.DocumentElement.ChildNodes){
			switch (xn.Name) {
			case "Part":
				XmlDocument xd = new XmlDocument ();
				xd.Load (xn.Attributes["part"].Value);
				foreach(XmlNode xn2 in xd.DocumentElement.ChildNodes){
					switch(xn2.Name){
					case "Mesh":
						go = OBJLoader.LoadOBJFile (xn2.Attributes ["mesh"].Value);
						go.transform.position = new Vector3 (Convert.ToSingle (xn.Attributes ["PosX"].Value), Convert.ToSingle (xn.Attributes ["PosY"].Value), Convert.ToSingle (xn.Attributes ["PosZ"].Value));
						go.transform.rotation = Quaternion.Euler (Convert.ToSingle (xn.Attributes ["RotX"].Value), Convert.ToSingle (xn.Attributes ["RotY"].Value), Convert.ToSingle (xn.Attributes ["RotZ"].Value));
						go.transform.localScale = new Vector3 (Convert.ToSingle(xn.Attributes["ScaleX"].Value), Convert.ToSingle(xn.Attributes["ScaleY"].Value), Convert.ToSingle(xn.Attributes["ScaleZ"].Value));
						go.transform.GetChild (0).gameObject.GetComponent<MeshRenderer> ().material = matPart;
						go.transform.GetChild (0).gameObject.GetComponent<MeshRenderer> ().material.mainTexture = TexturesLoader.texturesDiffuse[texture];
						go.transform.GetChild (0).gameObject.tag = "parts"; 
						go.transform.GetChild (0).gameObject.AddComponent<BoxCollider> ();
						go.transform.GetChild (0).gameObject.name = xn.Attributes["part"].Value;
						break;
					case "Scale":
						go.transform.localScale = new Vector3 (System.Convert.ToSingle(xn2.Attributes["scalex"].Value), System.Convert.ToSingle(xn2.Attributes["scaley"].Value), System.Convert.ToSingle(xn2.Attributes["scalez"].Value));
						break;
					case "fp":
						GameObject fp = new GameObject ();
						fp.transform.SetParent (go.transform.GetChild (0));
						fp.tag = "cp";
						fp.transform.position = new Vector3 (Convert.ToSingle(xn2.Attributes["PosX"].Value), Convert.ToSingle(xn2.Attributes["PosY"].Value), Convert.ToSingle(xn2.Attributes["PosZ"].Value));
						break;
					case "Diffuse":
						texture = xn2.Attributes["texture"].Value;
						break;
					}
				}
				break;
			}
		}
	}

}
