using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : MonoBehaviour {

	int [] acceptVertices = new int[10];
	public float mass;
	const float g = 9.8f;
	public Terrain ter;
	int resolution=  512;
	bool OnTheGround = true;
	GameObject col =null;
	Vector3 PositionObjectOnTerrain;
	float scaleMud = 0.1f;
	float P = 0;
	float[,] VertecesMud;

	void Start () {
		acceptVertices [0] = 0;
		acceptVertices [1] = 0;
		acceptVertices [2] = 0;
		acceptVertices [3] = 0;
		acceptVertices [4] = 0;
		acceptVertices [5] = 0;
		acceptVertices [6] = 0;
		acceptVertices [7] = 0;
		acceptVertices [8] = 0;
		acceptVertices [9] = 0;
		VertecesMud = new float[resolution, resolution];


				VertecesMud [90,80] = 1;

				ter.terrainData.SetHeights (0, 0, VertecesMud);

	}

	void OnCollisionStay(Collision collision){
		if (OnTheGround) {
			PositionObjectOnTerrain = collision.gameObject.transform.position - ter.gameObject.transform.position;
			P = (mass * g) / collision.transform.localScale.x;  

			VertecesMud [90, 80] = P * scaleMud * 0.1f;
			ter.terrainData.heightmapResolution = resolution;

			ter.terrainData.SetHeights (0, 0, VertecesMud);
			print ("G");
			OnTheGround = false;
		}

	}

	void OnCollisionExit(Collision collision){
		OnTheGround = false;
		col = null;
		P = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
