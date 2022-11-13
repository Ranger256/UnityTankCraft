
using UnityEngine;

public class CameraMapEditor : MonoBehaviour {
	[SerializeField] GameObject cam;
	[SerializeField] float speed;
	[SerializeField] float speedRot;
	[SerializeField] float scrollSpeed;
	float xpos;
	float ypos;
	float xrot;
	float yrot;
	float scroll;

	void Start () {
		
	}

	void FixedUpdate () {
		if(Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftControl)){
			xpos = cam.transform.position.x;
			ypos = cam.transform.position.y;
			xpos += (Input.GetAxis ("Mouse X") * speed * Time.fixedDeltaTime * -1);
			ypos += (Input.GetAxis ("Mouse Y") * speed * Time.fixedDeltaTime * -1) ;
			scroll += (Input.mouseScrollDelta.y * scrollSpeed * Time.fixedDeltaTime);
			Vector3 direction = new Vector3(xpos , ypos,scroll);
			cam.transform.position = direction;
		}
		if(!Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftControl)){
			xrot = cam.transform.rotation.eulerAngles.x;
			yrot = cam.transform.rotation.eulerAngles.y;
			yrot += Input.GetAxis ("Mouse X") * speedRot * Time.fixedDeltaTime;
			xrot += Input.GetAxis ("Mouse Y") * speedRot * Time.fixedDeltaTime;
			cam.transform.rotation = Quaternion.Euler ( new Vector3(xrot, yrot, 0));
		}
	}
}
