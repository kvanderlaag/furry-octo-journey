using UnityEngine.SceneManagement;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour {

	private const float PLANET_SCALE = 10.0f;

	private GameObject planet;
    [SerializeField] private Material[] skyboxes;

	// Use this for initialization
	void Start () {
        // Choose a random skybox
        int skyboxIndex = Random.Range(0, skyboxes.Length - 1);
        RenderSettings.skybox = skyboxes[skyboxIndex];

        // Generate the planet terrain
		planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		planet.transform.position = new Vector3(0, 0, 0);
		planet.transform.localScale = new Vector3(PLANET_SCALE, PLANET_SCALE, PLANET_SCALE);



        // Place doodads
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F5)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}
}
