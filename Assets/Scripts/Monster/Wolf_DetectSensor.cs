using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_DetectSensor : MonoBehaviour
{
    private BoxCollider2D collider;

    public bool AttackStart;

    Transform player_past_Pos;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();

        AttackStart = false;
    }

    void Update()
    {
        
    }

    public Transform GetPlayerPastPos() // get collision player pos
    {
        return player_past_Pos;
    }

    public void TriggerOff() // attackSensor off
    {
        collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            AttackStart = true;

            player_past_Pos = other.gameObject.transform;
        }
    }
}