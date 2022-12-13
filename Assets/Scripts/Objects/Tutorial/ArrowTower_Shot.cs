using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower_Shot : MonoBehaviour
{
    public GameObject Arrow_B;
    
    public Transform Shot_pos;

    float Attack_Time;
    public float Attack_delay;

    // ==============================

    public int HitCount;
    public GameObject dialogwindow;

    void Start()
    {
        Attack_Time = 0.0f;

        HitCount = 0;
    }

    void Update()
    {
        if(HitCount >= 5)
        {
            dialogwindow.SetActive(true);
        }
        
        if(dialogwindow.activeSelf == false)
        {
            Attack_Time += Time.deltaTime;

            if(Attack_Time >= Attack_delay)
            {
               SoundManager.Instance.PlaySFXSound("Arrow_Basic", 1.0f);

                Instantiate(Arrow_B, Shot_pos.position, Arrow_B.transform.rotation);
                Attack_Time = 0.0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("ParriedArrow"))
        {
            ++HitCount;
        }
    }
}
