using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Localization : MonoBehaviour {
	[SerializeField] string pathtxtLocal;
	Dictionary<int, string> texts = new Dictionary<int, string>();
	[SerializeField] Text[] text;

	void local(){
		string[] allLines = System.IO.File.ReadAllLines (pathtxtLocal);
		for(int i =0; i < allLines.Length; i++){
			string[] t = allLines [i].Split ('#');
			texts [System.Convert.ToInt32 (t [0])] = t [1];
		}
	}

	void Start () {
		local ();
		for(int i =0 ; i < text.Length; i++){
			text [i].text = texts [i];
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
