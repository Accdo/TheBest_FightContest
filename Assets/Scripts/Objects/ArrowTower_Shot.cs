using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower_Shot : MonoBehaviour
{
    public GameObject Arrow_B;
    
    public Transform Shot_pos;

    float Attack_Time;

    void Start()
    {
        Attack_Time = 0.0f;
    }

    void Update()
    {
        Attack_Time += Time.deltaTime;

        if(Attack_Time >= 1.0f)
        {
            Debug.Log("Shot");
            Instantiate(Arrow_B, Shot_pos.position, Arrow_B.transform.rotation);
            Attack_Time = 0.0f;
        }
    }
}
