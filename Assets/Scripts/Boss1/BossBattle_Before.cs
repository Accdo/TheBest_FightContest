using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle_Before : MonoBehaviour
{
    public GameObject Start_Dialog;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Start_Dialog.SetActive(true);
        }
    }
}