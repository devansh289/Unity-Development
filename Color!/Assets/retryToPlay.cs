using UnityEngine;
using UnityEngine.SceneManagement;

public class retryToPlay : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			SceneManager.LoadScene("Main");
		}
	}
}
