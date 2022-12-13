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

    // Wolf
    public GameObject wolf;

    // BossBattle_Potal_Open

    public GameObject Potal_Open;

    void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (IsHit)
        {
            SoundManager.Instance.PlaySFXSound("Fence_Atk", 0.5f);

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
                    SoundManager.Instance.PlaySFXSound("Fence_Broken", 0.5f);

                    wolf.SetActive(true);
                    SoundManager.Instance.PlaySFXSound("Wolf_RunStart", 0.5f);

                    Potal_Open.SetActive(true);
                }
            }
            IsHit = true;
        }
    }
}