using System.Xml;
using UnityEngine;

public class SaveMap : MonoBehaviour {
	
	[SerializeField]LoadMap lm;
	string pathObjectXml;

	void SaveMapXML(){
		pathObjectXml = lm.pathObjectXml;

		GameObject[] go = GameObject.FindGameObjectsWithTag ("ObjectMap");
		XmlElement[] xe = new XmlElement[go.Length];

		XmlDocument xdoc = new XmlDocument ();
		XmlElement mapCore = xdoc.CreateElement ("Map");
		xdoc.AppendChild (mapCore);

		for(int i =0 ; i < go.Length; i++){
			xe [i] = xdoc.CreateElement ("Object");
			xe [i].SetAttribute ("object", "Maps/classes/Objects/" + go[i].name + "_object.xml");
			xe [i].SetAttribute ("PosX", go[i].transform.position.x.ToString());
			xe [i].SetAttribute ("PosY", go[i].transform.position.y.ToString());
			xe [i].SetAttribute ("PosZ", go[i].transform.position.z.ToString());
			xe [i].SetAttribute ("RotX", go[i].transform.rotation.eulerAngles.x.ToString());
			xe [i].SetAttribute ("RotY", go[i].transform.rotation.eulerAngles.y.ToString());
			xe [i].SetAttribute ("RotZ", go[i].transform.rotation.eulerAngles.z.ToString());
			xe [i].SetAttribute ("ScaleX", go[i].transform.localScale.x.ToString());
			xe [i].SetAttribute ("ScaleY", go[i].transform.localScale.y.ToString());
			xe [i].SetAttribute ("ScaleZ", go[i].transform.localScale.z.ToString());
			mapCore.AppendChild (xe[i]);
		}

		xdoc.Save (pathObjectXml);

	}

	public void isClicked1(){
		SaveMapXML ();
	}
}
