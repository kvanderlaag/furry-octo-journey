using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    GameObject m_player;
    public float m_searchDistance = 3f;

    public GameObject m_bulletPrefab;

    public Animator m_anim;
    bool m_walking;

    public float m_moveSpeed = 1f;
    public Transform m_planet;
    float m_planetRadius;

    enum State
    {
        idle = 0,
        tracking = 1,
        attacking = 2,
        count = 3
    }

    State m_state;

	// Use this for initialization
	void Start () {
        m_planet = GameObject.Find("Planet").transform;
        m_planetRadius = m_planet.localScale.x / 2;
        transform.position = m_planet.position + (Random.onUnitSphere * m_planetRadius);
        transform.up = (transform.position - m_planet.position).normalized;
        m_anim = GetComponent<Animator>();
        GameObject go = GameObject.Find("Player");
        if (go)
        {
            m_player = go;
        }
        m_state = State.idle;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_player)
        {
            UpdateState();
        }
	}

    void UpdateState()
    {
        switch (m_state)
        {
            case State.idle:
                transform.Rotate(Vector3.up, Random.Range(-30f, 30f) * Time.deltaTime);
                transform.RotateAround(m_planet.position, transform.right, (Time.deltaTime * m_moveSpeed) / m_planetRadius * Mathf.Rad2Deg);
                break;
            case State.tracking:
                break;
            case State.attacking:
                break;
        }
    }
}
