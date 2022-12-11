using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_DetectSensor : MonoBehaviour
{
    public bool AttackStart;

    void Start()
    {
        AttackStart = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            AttackStart = true;
        }
    }
}
