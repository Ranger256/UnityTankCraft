using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour {
	public int numScene;

	public void startScene(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (numScene);
	}

}
