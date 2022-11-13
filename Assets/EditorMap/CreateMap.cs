using UnityEngine;
using System.Xml;

public class CreateMap : MonoBehaviour {
	
	[SerializeField] GameObject go;
	[SerializeField] UnityEngine.UI.InputField inf;
	bool isclic;
	string nameMap;
	string pathheightMap;
	string pathIcon;

	public void isClicked(){
		isclic = !isclic;

		go.SetActive (isclic);
	}

	void CreateFileTexture(){

	}

	public void CreatingMap(){
		nameMap = inf.text;

		if (!System.IO.Directory.Exists("Maps/levels/" + nameMap)) {
			System.IO.Directory.CreateDirectory ( "Maps/levels/"+ nameMap);
			Texture2D texheight = new Texture2D (512, 512);
			for(int i =0; i < 512; i++){
				for(int j = 0; j < 512; j++){
					texheight.SetPixel (i, j,  Color.blue);
				}
			}
			XmlDocument xdoc = new XmlDocument ();
			XmlElement el = xdoc.CreateElement ("Map");
			XmlElement heightMapEl = xdoc.CreateElement ("HeightMap");
			heightMapEl.SetAttribute ("path","Maps/levels/" + nameMap + "/map_height.png" );
			xdoc.AppendChild (el);
			el.AppendChild (heightMapEl);
			xdoc.Save ("Maps/levels/" + nameMap + "/" + (nameMap + "_map.xml"));
			System.IO.File.WriteAllBytes ("Maps/levels/" + nameMap + "/map_height.png", texheight.EncodeToPNG());
			texheight.hideFlags = HideFlags.HideAndDontSave;
		}

	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
