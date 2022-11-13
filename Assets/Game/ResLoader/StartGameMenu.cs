using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameMenu : MonoBehaviour {

	[SerializeField] Material mat;

	void Awake(){
		TexturesLoader.LoadedTextures ();
		LuaLoader.StartLua ();
		LuaLoader.matObjMeshes = mat;
	}
	void Start ()
	{
		//LuaLoader.RunMethodStart("test.lua");
	}

	void Update(){
		//LuaLoader.RunMethodUpdate ("test.lua");
	}
}
