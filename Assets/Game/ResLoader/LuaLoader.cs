using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using LuaInterface;


class GameApiLua{


	
	void Log(object message){
		Debug.Log (message);
	}

	GameObject CreateGameObject(){
		return new GameObject();
	}

	GameObject LoadMeshOBJ(string pathMesh){
		GameObject go = OBJLoader.LoadOBJFile (pathMesh);
		go.transform.GetChild(0).gameObject.GetComponent<MeshRenderer> ().material = LuaLoader.matObjMeshes;
		return go;
	}

	void SetTextureMesh(GameObject go, string texture){
		go.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.mainTexture = TexturesLoader.texturesDiffuse[texture];
	}

	Vector3 CreateVector3(float x, float y, float z){
		return new Vector3 (x, y, z);
	}

	GameObject[] GetGameObjectsTag(string tag){
		return GameObject.FindGameObjectsWithTag (tag);
	}

	void AddComponentGameObject(GameObject go, string component){
		go.AddComponent (Type.GetType(component));
	}

	void SetPositionObject(GameObject go,Vector3 pos){
		go.transform.position = pos;
	}

	void SetRotationObject(GameObject go, Vector3 rot){
		go.transform.rotation = Quaternion.Euler (rot);
	}

	void SetScaleObject(GameObject go, Vector3 scale){
		go.transform.localScale = scale;
	}
		
	Vector3 GetPositionObject(GameObject go){
		return go.transform.position;
	}

	bool GetKey(string key){
		return Input.GetKey (key);
	}

	bool GetKeyDown(string key){
		return Input.GetKeyDown (key);
	}

	bool GetKeyUp(string key)
	{
		return Input.GetKeyUp (key);
	}

	float GetAxis(string axis){
		return Input.GetAxis (axis);
	}

}

public static class LuaLoader {
	public static Dictionary<string, Lua> luaScpipts = new Dictionary<string, Lua>();
	public static Dictionary<string, LuaFunction> luaMethodsStart = new Dictionary<string, LuaFunction>();
	public static Dictionary<string, LuaFunction> luaMethodsUpdate = new Dictionary<string, LuaFunction> ();
	public static Material matObjMeshes;
	public static string[] pathScripts;
	static Lua lua = new Lua();

	static void LoadingLuaMemory(){
		lua ["GameAPI"] = new GameApiLua ();
		
		pathScripts = System.IO.Directory.GetFiles ("Scripts/lua/", "*.lua", System.IO.SearchOption.AllDirectories);
		for(int i =0;i < pathScripts.Length;i++){
			luaScpipts.Add (pathScripts[i], lua);
			luaScpipts [pathScripts [i]].DoFile (pathScripts [i]);
			luaMethodsStart.Add (pathScripts[i], luaScpipts [pathScripts [i]] ["Start"] as LuaFunction);
			luaMethodsUpdate.Add (pathScripts[i], luaScpipts[pathScripts[i]]["Update"] as LuaFunction);
		}
	}

	public static void RunScpipt(string scriptName){
		try{
			luaScpipts ["Scripts/lua/" + scriptName].DoFile ("Scripts/lua/" + scriptName);
		}catch{
			Debug.LogError ("Error");
		}
	}

	public static void RunMethodStart(string scriptName){
		luaMethodsStart ["Scripts/lua/" + scriptName].Call ();
	}

	public static void RunMethodUpdate(string scriptName){
		luaMethodsUpdate ["Scripts/lua/" + scriptName].Call ();
	}

	public static void StartLua(){
		LoadingLuaMemory ();
	}
}
