using UnityEngine.UI;
using UnityEngine;

public class ChoiseLoadTank : MonoBehaviour {
	[SerializeField] ScrollRect sr;
	[SerializeField] GameObject butPref;
	[SerializeField] LoadTank lt;
	GameObject[] buts;
	string[] pathTanks;
	bool isc;

	void ButtF(int n){
		lt.pathTank = pathTanks [n];
		lt.LoadingTank ();
	}

	void CreateButtons(){
		pathTanks = System.IO.Directory.GetFiles ("Tanks/", "*.xml", System.IO.SearchOption.AllDirectories);
		buts = new GameObject[pathTanks.Length];

		for(int i = 0; i < pathTanks.Length; i++){
			buts[i] = Instantiate (butPref);
			buts [i].transform.parent = sr.content;
			buts [i].transform.localScale = new Vector3 (0.4f, 0.7f, 0);
			int n = i;
			buts [i].GetComponent<Button> ().onClick.AddListener ( () => ButtF(n));
		}
	}

	public void isClicked(){
		isc = !isc;

		sr.gameObject.SetActive (isc);
		sr.transform.parent.gameObject.SetActive (isc);
	}

	void Start(){
		CreateButtons ();
	}
}
