using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class MeshCombiner : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.L)){
			MeshComb ();
		}
	}

	void MeshComb(){
		MeshFilter[] meshf = GetComponentsInChildren<MeshFilter> ();
		CombineInstance[] ci = new CombineInstance[meshf.Length];
		for(int i = 0; i < meshf.Length; i++){
			ci [i].mesh = meshf [i].sharedMesh;
			ci [i].transform = meshf [i].transform.localToWorldMatrix;
			meshf [i].gameObject.SetActive (false);
		}
		MeshFilter mf2 = transform.GetComponent<MeshFilter> ();
		mf2.mesh = new Mesh ();
		mf2.mesh.CombineMeshes (ci);
		GetComponent<MeshCollider> ().sharedMesh = mf2.mesh;
		transform.gameObject.SetActive (true);
	}
}
