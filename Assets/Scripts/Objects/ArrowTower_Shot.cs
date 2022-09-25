using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower_Shot : MonoBehaviour
{
    public GameObject Arrow_B;
    
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
            Instantiate(Arrow_B, transform.position, Arrow_B.transform.rotation);
            Attack_Time = 0.0f;
        }
    }
}
