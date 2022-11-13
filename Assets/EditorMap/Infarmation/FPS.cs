using UnityEngine;

public class FPS : MonoBehaviour {

	[SerializeField] UnityEngine.UI.Text Fpstext;
	int frames;
	[SerializeField] float updateInterval;
	double lastinterval;
	float fps;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		frames++;
		float timenow = Time.realtimeSinceStartup;
		if(timenow > lastinterval + updateInterval){
			fps = (float)(frames / (timenow - lastinterval));
			frames = 0;
			lastinterval = timenow;
		}
		Fpstext.text = fps.ToString();
	}
}
