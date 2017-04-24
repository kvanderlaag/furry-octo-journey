using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class BullshitCharacterController : MonoBehaviour {

    public GameObject m_bulletPrefab;
    public Transform m_bulletSpawn;
    public float m_fireRepeatTime = 0.25f;
    float m_fireCooldown;

    public Player m_player;

    public Transform m_planet;

    Animator m_anim;
    int m_walkHash = Animator.StringToHash("Walk");
    int m_idleHash = Animator.StringToHash("Idle");
    bool m_walking = false;

    float m_planetRadius;
    float m_angle = 0f;

    public float m_moveSpeed = 5f;
    public float m_rotateSpeed = 2f;

	// Use this for initialization
	void Start () {
        m_planet = GameObject.Find("Planet").transform;
        m_fireCooldown = 0f;
        m_anim = GetComponent<Animator>();
        m_planetRadius = m_planet.localScale.x / 2f;
        m_player = ReInput.players.GetPlayer(0);
        transform.position = m_planet.position + (Random.onUnitSphere * m_planetRadius);
        Vector3 normal = (transform.position - m_planet.position).normalized;
        transform.up = normal;
    }
	
	// Update is called once per frame
	void Update () {
        DealWithBullshitInput();
        UpdateOrientation();
        if (m_fireCooldown > 0)
        {
            m_fireCooldown -= Time.deltaTime;
            m_fireCooldown = Mathf.Max(m_fireCooldown, 0f);
        }
    }

    void DealWithBullshitInput ()
    {
        Vector2 direction = new Vector2(m_player.GetAxis("StickX"), m_player.GetAxis("StickY"));
        direction.Normalize();
        Vector2 stickRotate = new Vector2(m_player.GetAxis("RStickX"), m_player.GetAxis("RStickY"));

        if (stickRotate.x != 0)
        {
            m_angle += Time.deltaTime * m_rotateSpeed * stickRotate.x;
            if (m_angle < 0)
            {
                m_angle += 360f;
            }
            if (m_angle >= 360f)
            {
                m_angle -= 360f;
            }
            transform.Rotate(Vector3.up, Time.deltaTime * m_rotateSpeed * stickRotate.x);
        }
        
        if (direction.x != 0 || direction.y != 0)
        {
            if (!m_walking)
            {
                m_anim.SetTrigger(m_walkHash);
                m_walking = true;
            }

            transform.RotateAround(m_planet.position, transform.right, (Time.deltaTime * m_moveSpeed * direction.y) / m_planetRadius * Mathf.Rad2Deg);
            transform.RotateAround(m_planet.position, transform.forward, (Time.deltaTime * m_moveSpeed * -direction.x) / m_planetRadius * Mathf.Rad2Deg);
        }
        else
        {
            if (m_walking)
            {
                m_anim.SetTrigger(m_idleHash);
                m_walking = false;
            }   
        }

        if (m_player.GetButton("Attack") && m_fireCooldown <= 0f)
        {
            GameObject go = Instantiate(m_bulletPrefab) as GameObject;
            go.transform.parent = null;
            BulletController bc = go.GetComponent<BulletController>();
            if (bc)
            {
                bc.m_owner = gameObject;
                bc.transform.position = m_bulletSpawn.position;
                bc.transform.rotation = m_bulletSpawn.rotation;
            }
            m_fireCooldown = m_fireRepeatTime;
        }
        
    }

    void UpdateOrientation()
    {
        
        //Vector3 normal = (transform.position - m_planet.position).normalized;
        // Rotate the player
        //Quaternion q1 = Quaternion.Euler(0f, m_angle, 0f);
        //Quaternion q2 = Quaternion.FromToRotation(Vector3.up, normal);
        //transform.rotation = q2 * q1;
        
    }
}
