using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower_Shot : MonoBehaviour
{
    public GameObject Arrow_B;
    
    public Transform Shot_pos;

    float Attack_Time;

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

        Attack_Time += Time.deltaTime;

        if(Attack_Time >= 1.0f)
        {
            Debug.Log("Shot");
            Instantiate(Arrow_B, Shot_pos.position, Arrow_B.transform.rotation);
            Attack_Time = 0.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("ParriedArrow"))
        {
            ++HitCount;
        }
    }
}
