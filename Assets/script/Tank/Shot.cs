using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
	public GameObject spawn;

	public GameObject bul;

	public float speed;

	private bool reload;

	public float waitTime;

	public int countAmmu;

	public AudioSource as2;

	public AudioClip as3;

	private GameObject pul;

	public Text text1;

	private void Awake()
	{
		as2.clip = as3;
		as2.volume = 1f;
		as2.pitch = 1f;
		as2.loop = false;
	}

	private void FixedUpdate()
	{
		text1.text = string.Format("{0:0}", countAmmu);
		if (Input.GetMouseButtonDown(0) && !reload && countAmmu > 0)
		{
			Vector3 position = spawn.transform.position;
			Quaternion rotation = spawn.transform.rotation;
			GameObject gameObject = Instantiate(bul, position, rotation);
			Rigidbody component = gameObject.GetComponent<Rigidbody>();
			component.AddForce(gameObject.transform.up * speed, ForceMode.Impulse);
			countAmmu--;
			Reload();
			UnityEngine.Object.Destroy(gameObject, 4f);
		}
	}

	private void Reload()
	{
		this.reload = true;
		if (this.reload)
		{
			base.StartCoroutine(this.zaderjka());
		}
	}
		
	private IEnumerator zaderjka()
	{
		yield return new WaitForSeconds (waitTime);
		reload = false;
	}
}
