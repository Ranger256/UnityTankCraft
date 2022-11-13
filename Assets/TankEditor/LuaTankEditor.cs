using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuaTankEditor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		LuaLoader.RunMethodStart ("test.lua");
	}
	
	// Update is called once per frame
	void Update () {
		LuaLoader.RunMethodUpdate ("test.lua");
	}
}
