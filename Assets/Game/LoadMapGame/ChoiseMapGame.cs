using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiseMapGame : MonoBehaviour {

	[SerializeField] GameObject go;
	[SerializeField] GameObject ButtPref;
	[SerializeField] UnityEngine.UI.ScrollRect sr;
	[SerializeField] int numberScene;
	LoadScene l = new LoadScene();
	bool iscl1;
	bool iscl2;
	string[] maps;
	GameObject[] buttons;

	public void isClicked(){
		iscl1 = !iscl1;

		go.SetActive (iscl1);
	}

	public void isExitChoiseMap(){
		iscl1 = false;
		go.SetActive (iscl1);
	}

	public void isClicked2(){
		iscl2 = !iscl2;
		sr.gameObject.SetActive (iscl2);
	}

	void ButtFunc(int n){
		Mediator.nameMap = maps [n];
		l.startScene ();
	}

	// Use this for initialization
	void Start () {
		l.numScene = numberScene;
		maps = System.IO.Directory.GetFiles ("Maps/levels/", "*_map.xml", System.IO.SearchOption.AllDirectories);
		buttons = new GameObject[maps.Length];

		for(int i = 0; i < buttons.Length; i++){
			buttons[i] = Instantiate (ButtPref);
			buttons [i].transform.SetParent (sr.content.transform);
			buttons [i].transform.localScale = new Vector3 (0.7f , 0.7f, 0);
			int n = i;
			buttons [i].GetComponent<UnityEngine.UI.Button> ().onClick.AddListener (() => ButtFunc(n));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
