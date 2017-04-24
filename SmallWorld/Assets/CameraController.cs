using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform m_target;
    public Transform m_planet;
    public float m_baseDistance = 5f;
    public float m_cameraAngle = 70f;

    public float m_lerpSpeed = 2f;

    Vector3 m_targetPos;

	// Use this for initialization
	void Start () {
        transform.position = m_target.position + (m_target.up * m_baseDistance);
	}
	
	// Update is called once per frame
	void Update () {
        m_targetPos = m_target.position + (m_target.up * Mathf.Cos(m_cameraAngle * Mathf.Deg2Rad) * m_baseDistance) + (-m_target.forward * Mathf.Sin(m_cameraAngle * Mathf.Deg2Rad) * m_baseDistance * 0.75f);
        transform.LookAt(m_target.position + m_target.forward * 0.5f, m_target.up);
        transform.position = Vector3.Lerp(transform.position, m_targetPos, Time.deltaTime * m_lerpSpeed);

    }
}
