using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SavingTank : MonoBehaviour {

	[SerializeField] Text t;
	string pathTank;
	string[] p;
	string[] id = new string[5];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SaveTank(){
		pathTank = "Tanks/" + t.text + ".tank";
	
		StreamWriter sr = new StreamWriter (pathTank);
		GameObject[] go = GameObject.FindGameObjectsWithTag ("parts");
		int i = -1;
		while(i < go.Length){
			i++;
			id[i] = "G" + System.Convert.ToString(i);
		}
		foreach(var g in go){
			p = g.name.Split ('(');
			int ij = -1;
			ij++;
			sr.WriteLine("OBJECT<" + p[0] + "<" + id[ij]);

		}
		sr.Close ();
	}
}
