using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eff_PinkFog : MonoBehaviour
{
    private SpriteRenderer m_sprite;

    Color FullAlpha;
    Color HalfAlpha;

    [SerializeField]float time = 0.0f;
    [SerializeField]bool UpDown = false; // true : 업, false : 다운

    void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();

        FullAlpha = m_sprite.color;
        HalfAlpha = new Color(1,1,1,0.5f);
    }

    void Update()
    {
        if(m_sprite.color.a >= 1.0f)
        {
            time = 0.0f;
            UpDown = false;
        }
        else if(m_sprite.color.a <= 0.5f)
        {
            time = 0.0f;
            UpDown = true;
        }


        if(UpDown) // 업
        {
            time += Time.deltaTime;
            m_sprite.color = new Color(1,1,1,0.5f + (0.5f * time));
        }
        else // 다운
        {
            time += Time.deltaTime;
            m_sprite.color = new Color(1,1,1,1 - (0.5f * time));
        }
    }
}
