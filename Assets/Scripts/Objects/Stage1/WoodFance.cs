using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFance : MonoBehaviour
{
    // wood trap
    private SpriteRenderer m_sprite;

    public Sprite[] wood_sprite;


    [SerializeField] int wood_count = 0;

    [SerializeField] bool IsHit = false;

    // rope trap On
    public GameObject rope_trap;

    // ´Á´ë
    public GameObject wolf;

    void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (IsHit)
        {
            m_sprite.sprite = wood_sprite[wood_count];
            IsHit = false;
        }
            
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            if(wood_count < 5)
            {
                ++wood_count;
                if(wood_count == 5)
                {
                    rope_trap.SetActive(true);
                    wolf.SetActive(true);
                }
            }
            IsHit = true;
        }
    }
}