using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class BullshitCharacterController : MonoBehaviour {

    Vector2 m_position;
    float m_rotation;

    public Player m_player;

    public Transform m_planet;

    float m_planetRadius;

    public float m_moveSpeed = 1f;

	// Use this for initialization
	void Start () {
        m_planetRadius = m_planet.localScale.x / 2f;
        m_player = ReInput.players.GetPlayer(0);
        m_position = new Vector2(0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        DealWithBullshitInput();
        Quaternion q = Quaternion.Euler
        transform.position = m_planet.position + (transform.position - m_planet.position).normalized * m_planetRadius;
        transform.up = (transform.position - m_planet.position).normalized;
        transform.Rotate(transform.forward, m_rotation);

    }

    void DealWithBullshitInput ()
    {
        Vector2 direction = new Vector2(m_player.GetAxis("StickX"), m_player.GetAxis("StickY"));

        if (direction.magnitude > 0)
        {
            if (direction.y >= 0)
            {
                if (direction.x >= 0)
                {
                    m_rotation = Vector2.Angle(Vector2.up, direction);
                    m_position.x += m_moveSpeed * Time.deltaTime;
                    if (m_position.x >= 360f)
                    {
                        m_position.x -= 360f;
                    }
                }
                else
                {
                    m_rotation = Vector2.Angle(Vector2.up, direction);
                }
            }
            else
            {
                if (direction.x >= 0)
                {
                    m_rotation = 360f - Vector2.Angle(Vector2.up, direction);
                }
                else
                {
                    m_rotation = 360f - Vector2.Angle(Vector2.up, direction);
                }
            }
        }

    }
}
