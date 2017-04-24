using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour {

    public int m_maxHealth;
    int m_currentHealth;

    bool m_flashing;

	// Use this for initialization
	void Start () {
        m_currentHealth = m_maxHealth;
        m_flashing = false;
	}

    public void Damage()
    {
        m_currentHealth = Mathf.Max(0, m_currentHealth - 1);
        if (!m_flashing)
        {
            Renderer[] ren = GetComponentsInChildren<Renderer>();
            if (ren.Length > 0)
            {
                StartCoroutine(Flash(ren));
            }
        }
    }

    IEnumerator Flash(Renderer[] ren)
    {
        m_flashing = true;
        List<Material> mats = new List<Material>();
        List<Color> colors = new List<Color>();
        foreach (Renderer r in ren)
        {
            mats.Add(r.material);
            colors.Add(r.material.color);
            r.material.color = Color.red;
        }

        float fadeDur = 0.5f;
        float fadeElapsed = 0f;
        while (fadeElapsed <= fadeDur)
        {
            fadeElapsed += Time.deltaTime;
            for (int i = 0; i < mats.Count; ++i)
            {
                Color fadeCol = Color.Lerp(Color.red, colors[i], (fadeElapsed / fadeDur));
                mats[i].color = fadeCol;
            }
            
            yield return null;
        }

        for (int i = 0; i < mats.Count; ++i)
        {
            mats[i].color = colors[i];
        }
        m_flashing = false;
    }
}
