using UnityEngine.UI;
using UnityEngine;
using System.Xml;

public class SavePart : MonoBehaviour {

	[SerializeField] InputField inf;

	public void PartSave(){
		XmlDocument xdoc = new XmlDocument ();
		XmlElement xe = xdoc.CreateElement ("Part");
		xdoc.AppendChild (xe);

		GameObject[] models = GameObject.FindGameObjectsWithTag ("parts");
		GameObject[] fp = GameObject.FindGameObjectsWithTag ("cp");

		XmlElement xt = xdoc.CreateElement ("Diffuse");
		xt.SetAttribute ("texture", PartModelAdd.activeTexture);
		xe.AppendChild (xt);

		for(int i =0 ; i < models.Length; i++){
			XmlElement xm = xdoc.CreateElement ("Mesh");
			xm.SetAttribute ("mesh", models[i].name);
			xe.AppendChild (xm);
			XmlElement scale = xdoc.CreateElement ("Scale");
			scale.SetAttribute ("scalex", models[i].transform.localScale.x.ToString());
			scale.SetAttribute ("scaley", models[i].transform.localScale.y.ToString());
			scale.SetAttribute ("scalez", models[i].transform.localScale.z.ToString());
			xe.AppendChild (scale);
		}
		for(int i =0; i < fp.Length;i++){
			XmlElement fpm = xdoc.CreateElement ("fp");
			fpm.SetAttribute ("PosX", fp[i].transform.position.x.ToString());
			fpm.SetAttribute ("PosY", fp[i].transform.position.y.ToString());
			fpm.SetAttribute ("PosZ", fp[i].transform.position.z.ToString());
			xe.AppendChild (fpm);
		}

		System.IO.Directory.CreateDirectory ("Parts/" +inf.text);
		xdoc.Save ("Parts/" + inf.text + "/" + inf.text + "_part.xml" );
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
