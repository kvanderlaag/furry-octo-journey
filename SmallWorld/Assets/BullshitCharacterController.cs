using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class BullshitCharacterController : MonoBehaviour {

    public Player m_player;

    public Transform m_planet;

    float m_planetRadius;

    public float m_moveSpeed = 5f;

	// Use this for initialization
	void Start () {
        m_planetRadius = m_planet.localScale.x / 2f;
        m_player = ReInput.players.GetPlayer(0);
        transform.position = m_planet.position - (m_planet.forward * m_planetRadius);
	}
	
	// Update is called once per frame
	void Update () {
        DealWithBullshitInput();
        
    }

    void DealWithBullshitInput ()
    {
        Vector2 direction = new Vector2(m_player.GetAxis("StickX"), m_player.GetAxis("StickY"));

        float angle = 0f;
        if (direction.x != 0 || direction.y != 0)
        {
            angle = Mathf.Atan2(-direction.y, direction.x) * Mathf.Rad2Deg;

            Vector3 normal = (transform.position - m_planet.position).normalized;
            // Rotate the player
            Quaternion q1 = Quaternion.Euler(new Vector3(0f, angle + 90f, 0f));
            Quaternion q2 = Quaternion.FromToRotation(Vector3.up, normal);
            transform.rotation = q2 * q1;

            transform.position += transform.forward * Time.deltaTime * m_moveSpeed * direction.magnitude;
            transform.position = m_planet.position + ((transform.position - m_planet.position).normalized * m_planetRadius);
        }
    }
}
