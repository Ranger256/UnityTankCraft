using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class SaveTank : MonoBehaviour {
	[SerializeField] InputField inf;
	GameObject[] go;

	public void isSaved(){
		go = GameObject.FindGameObjectsWithTag ("parts");
		XmlDocument xdoc = new XmlDocument ();
		XmlElement craft = xdoc.CreateElement ("Craft");
		xdoc.AppendChild (craft);
		for(int i =0; i < go.Length;i++){
			XmlElement part = xdoc.CreateElement ("Part");
			part.SetAttribute ("part", go[i].name);
			part.SetAttribute ("PosX", go[i].transform.position.x.ToString());
			part.SetAttribute ("PosY", go[i].transform.position.y.ToString());
			part.SetAttribute ("PosZ", go[i].transform.position.z.ToString());
			part.SetAttribute ("RotX", go[i].transform.rotation.eulerAngles.x.ToString());
			part.SetAttribute ("RotY", go[i].transform.rotation.eulerAngles.y.ToString());
			part.SetAttribute ("RotZ", go[i].transform.rotation.eulerAngles.z.ToString());
			part.SetAttribute ("ScaleX", go[i].transform.localScale.x.ToString());
			part.SetAttribute ("ScaleY", go[i].transform.localScale.y.ToString());
			part.SetAttribute ("ScaleZ", go[i].transform.localScale.z.ToString());
			craft.AppendChild (part);
		}
		xdoc.Save ("Tanks/" + inf.text + ".xml");
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
