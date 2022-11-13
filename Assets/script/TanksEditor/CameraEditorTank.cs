using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEditorTank : MonoBehaviour {

	[SerializeField]
	GameObject target;
	[SerializeField]
	GameObject cam;
	float y;
	float x;
	public float sens;
	float eulerX;
	float eulerY;
	Vector3 mousePos;
	Vector2 mp;


	void Update(){
		Event currentEvent = Event.current;
		if(Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftShift)){
			y = Input.GetAxis ("Mouse X") * sens * Time.deltaTime;
			x = Input.GetAxis ("Mouse Y") * sens * Time.deltaTime;

			eulerX = (cam.transform.rotation.eulerAngles.x + x) % 360;
			eulerY = (cam.transform.rotation.eulerAngles.y + y) % 360;
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftShift)){

			mousePos = UnityEngine.Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, UnityEngine.Camera.main.nearClipPlane));

			Vector3 posCam = new Vector3(mousePos.x + 72, mousePos.y - 170, 0);

			cam.transform.Translate (new Vector3(posCam.x * 0.01f, posCam.y * 0.01f, 0));

			print (posCam);
		}
		cam.transform.rotation = Quaternion.Euler (eulerX,eulerY, 0);
	}

}
