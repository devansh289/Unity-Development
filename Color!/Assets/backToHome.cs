using UnityEngine;
using UnityEngine.SceneManagement;

public class backToHome : MonoBehaviour {
	
	void Update () {
		if (Input.touchCount > 0){
			SceneManager.LoadScene("Menu");
		}
	}
}
