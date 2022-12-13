using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle_Before : MonoBehaviour
{
    public GameObject Start_Dialog;

    public GameObject CutScened;

    float delay = 0.0f;

    bool dialogOn = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(dialogOn)
        {
            delay += Time.deltaTime;
            if(delay >= 3.0f)
            {
                Start_Dialog.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CutScened.SetActive(true);

            dialogOn = true;
        }
    }
}