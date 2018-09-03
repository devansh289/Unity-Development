using UnityEngine;
using UnityEngine.SceneManagement;

public class dabombAfter: MonoBehaviour {
	
	void Update () {
		if (Input.touchCount > 0){
			SceneManager.LoadScene("Menu");
		}
	}
}
