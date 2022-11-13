using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScprpt : MonoBehaviour {

	[SerializeField] GameObject water;
	[SerializeField] Material matWater;
	[SerializeField] float power;
	[SerializeField] float mult;
	[SerializeField] float speedWater  = 0.5f;
	MeshRenderer watRend;
	MeshFilter watFilt;
	MaterialPropertyBlock mb;
	float b;

	void Awake(){
		mb = new MaterialPropertyBlock ();
	}

	void Start () {
		watRend = water.GetComponent<MeshRenderer> ();
		watFilt = water.GetComponent<MeshFilter> ();
		watRend.material = matWater;
		WaterCreate ();
	
	}
	
	// Update is called once per frame
	void Update () {
		MoveWater ();

	}

	float GenerateNoise (float min, float max){
		float noise = 0;
		System.Random r = new System.Random();
		noise = System.Convert.ToSingle(r.NextDouble ()) * (max - min) + min; 
		return noise;
	}

	void MoveWater(){
		if (b < 1) {
			b += speedWater / 100 * Time.deltaTime;
		} else {
			b = 0;
		}
		watRend.GetPropertyBlock (mb);
		mb.SetFloat ("_OffsetX",b);
		watRend.SetPropertyBlock (mb);
	}

	void WaterCreate(){
		Vector3[] vertices = watFilt.mesh.vertices;
		for(int i = 0; i < vertices.Length; i ++){
			for(int j = 0; j < vertices.Length; j++){
				float perlinNoise = Mathf.PerlinNoise (GenerateNoise(0, i * power), GenerateNoise(0, j * power));
				vertices [i].y = perlinNoise * mult;
			}
		}
		watFilt.mesh.vertices = vertices;
	}
}
