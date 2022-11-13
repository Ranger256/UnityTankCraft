using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load : MonoBehaviour {
	public int sceneiD;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Loading(){
		SceneManager.LoadScene (sceneiD);
		Time.timeScale = 1;
	}
}
