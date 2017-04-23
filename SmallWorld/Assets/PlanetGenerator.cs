using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour {

	private const float PLANET_SCALE = 10.0f;

	private GameObject planet;

	// Use this for initialization
	void Start () {
		planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		planet.transform.position = new Vector3(0, 0, 0);
		planet.transform.localScale = new Vector3(PLANET_SCALE, PLANET_SCALE, PLANET_SCALE);
	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/
}
