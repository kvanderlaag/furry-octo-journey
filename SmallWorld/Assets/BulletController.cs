using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public GameObject m_owner;
    public Transform m_planet;
    public float m_planetRadius;
    public float m_speed = 5f;
    public float m_timeToLive = 2f;

	// Use this for initialization
	void Start () {
        GameObject pl = GameObject.Find("Planet");
        if (pl)
        {
            m_planet = pl.transform;
            m_planetRadius = pl.transform.localScale.x / 2f;
        }
	}
	
	// Update is called once per frame
	void Update () {
        m_timeToLive -= Time.deltaTime;
        if (m_timeToLive <= 0)
        {
            Destroy(gameObject);
        }
        transform.RotateAround(m_planet.position, transform.right, (Time.deltaTime * m_speed) / m_planetRadius * Mathf.Rad2Deg);


    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision between " + gameObject.name + " and " + other.gameObject.name);
        if (other.GetComponent<HealthComponent>())
        {
            other.GetComponent<HealthComponent>().Damage();
            Destroy(gameObject);
        }
    }
}
