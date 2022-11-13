using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadingTank : MonoBehaviour {

	public string nameTank;
	GameObject[] go = new GameObject [5];
	string[] id = new string[100];
	int dO = -1;


	public void LoadingT(){
		string[] parts = File.ReadAllLines ("Tanks/" + nameTank + ".tank");
	
		for(int i = 0; i < parts.Length; i++){
			string[] p = parts [i].Split (new char[] {'<'}, System.StringSplitOptions.RemoveEmptyEntries);
			for(int j = 0; j < p.Length; j++){
				switch(p[j]){
				case "OBJECT":
					dO++;
					id [dO] = p [j + 2];
					go [dO] = Instantiate (Resources.Load<GameObject> (p [j + 1]));
					go[dO].transform.parent = GameObject.FindGameObjectWithTag ("container").transform;
					break;
				case "PARENT":
					for (int po = 0; po <= dO; po++) {
						if (p [j + 1] == id [po]) {
							if(p[j + 2] == id[po + 1]){
								go [po].transform.parent = go [po + 1].transform;
							}
						}
					}
					break;
				case "POSITION":
					for (int po = 0; po <= dO; po++) {
						if (p [j + 1] == id [po]) {
							string[] posOb = p [j + 2].Split (new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
							for (int com = 0; com < posOb.Length; com++) {
								go [po].transform.localPosition = new Vector3 (float.Parse (posOb [0]), float.Parse (posOb [1]), float.Parse (posOb [2]));
							}

						}
					}
					break;
				case "ROTATION":
					for(int ro = 0; ro <= dO; ro++){
						if(p[j + 1] == id[ro]){
							string[] posOb = p [j + 2].Split (new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
							for (int com = 0; com < posOb.Length; com++) {
								go [ro].transform.localRotation = Quaternion.Euler(float.Parse (posOb [0]), float.Parse (posOb [1]), float.Parse (posOb [2]));
							}
						}
					}
					break;
				case "COMPONENTS":
					for(int h = 0; h <= dO; h++){
						if(p[j + 1] == id[h]){
							string[] compo = p [j + 2].Split (new char[] {','}, System.StringSplitOptions.RemoveEmptyEntries);
							for(int com = 0; com < compo.Length; com++){
								go [h].AddComponent (System.Type.GetType(compo[com]));
							}

						}
					}
					break;
				}
			}
		}
	}
	
}
