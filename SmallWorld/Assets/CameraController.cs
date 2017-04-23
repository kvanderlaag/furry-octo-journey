using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform m_target;
    public Transform m_planet;
    public float m_baseDistance = 5f;

	// Use this for initialization
	void Start () {
        transform.position = m_target.position + (m_target.up * m_baseDistance);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = m_target.position + (m_target.up * m_baseDistance);
        transform.LookAt(m_target, Vector3.up);

    }
}
