using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
	[SerializeField] MeshFilter mf;
	[SerializeField] float power;
	[SerializeField] float scale;
	Vector2 startPoint;


	void Start(){
		mf = GetComponent<MeshFilter> ();
		startPoint = new Vector2(0f, 0f);
		Vector3[] vertices = mf.mesh.vertices;
		for(int i = 0; i < vertices.Length; i++){
			float x = startPoint.x + vertices [i].x * scale;
			float z = startPoint.y + vertices [i].z * scale;
			vertices [i].y = (Mathf.PerlinNoise (x, z) - 0.5f) * power;
		}
		mf.mesh.vertices = vertices;
		mf.mesh.RecalculateBounds();
		mf.mesh.RecalculateNormals();
	}
}
