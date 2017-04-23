using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	/*void Start () {
		
	}*/
	
	public void PlayLocal() {
		// TODO-DG: Go to local scene
		SceneManager.LoadScene("PlanetLoad");
	}

	public void PlayMulti() {
		// TODO-DG: Show additional options for multiplayer setup.
	}
}