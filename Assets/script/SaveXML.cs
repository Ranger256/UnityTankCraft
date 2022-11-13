using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class SaveXML : MonoBehaviour {
	float posx;
	float posy;
	float posz;
	float rotx;
	float roty;
	float rotz;
	float posbx;
	float posby;
	float posbz;
	float rotbx;
	float rotby;
	float rotbz;

	public GameObject players6;
	public GameObject bot;
	public GameObject pause;

	string dann;
	[SerializeField] private int key = 14;
	public string path;

	void Update () {
		
	}

	public void saving(){
		posx = players6.transform.position.x;
		rotx = players6.transform.eulerAngles.x;
		posy = players6.transform.position.y;
		roty = players6.transform.eulerAngles.y;
		posz = players6.transform.position.z;
		rotz = players6.transform.eulerAngles.z;

		posbx = bot.transform.position.x;
		rotbx = bot.transform.eulerAngles.x;
		posby = bot.transform.position.y;
		rotby = bot.transform.eulerAngles.y;
		posbz = bot.transform.position.z;
		rotbz = bot.transform.eulerAngles.z;
		PlayerXML player = new PlayerXML (posx,posy,posz, rotx, roty, rotz, posbx, posby, posbz, rotbx, rotby, rotbz);
		XmlSerializer xmlserial = new XmlSerializer (typeof(PlayerXML));
		using (FileStream fs = new FileStream(path, FileMode.Create)){
			xmlserial.Serialize (fs, player);
			fs.Close ();
			dann = File.ReadAllText (path);
		}
		dann = Encrpypt (dann, key);
		File.WriteAllText (path, dann);

	}

	static string Encrpypt (string dann, int key){
		string result = "";
		for(int i = 0; i < dann.Length; i++){
			result += (char)(dann [i] ^ key);

		}
		return result;
	}
	static string Decrypt(string dann, int key){
		return Encrpypt(dann, key);
	}
		

	public void loading(){
		dann = Decrypt (dann, key);
		File.WriteAllText (path, dann);
		pause.SetActive (false);
		Time.timeScale = 1;
		FileStream fs = new FileStream (path, FileMode.Open);
		XmlSerializer xl = new XmlSerializer (typeof(PlayerXML));
		PlayerXML pxml = xl.Deserialize (fs) as PlayerXML;
		fs.Close ();

		posy = pxml.Posy;
		posx = pxml.Posx;
		posz = pxml.Posz;
		rotx = pxml.Rotx;
		roty = pxml.Roty;
		rotz = pxml.Rotz;

		posbx = pxml.Posbx;
		posby = pxml.Posby;
		posbz = pxml.Posbz;
		rotbx = pxml.Rotbx;
		rotby = pxml.Rotby;
		rotbz = pxml.Rotz;

		players6.transform.position = new Vector3 (posx, posy, posz);
		players6.transform.rotation = Quaternion.Euler (rotx, roty, rotz);

		bot.transform.position = new Vector3 (posbx, posby, posbz);
		bot.transform.rotation = Quaternion.Euler (rotbx, rotby, rotbz);

		dann = Encrpypt (dann, key);
		File.WriteAllText (path, dann);
	}

}


