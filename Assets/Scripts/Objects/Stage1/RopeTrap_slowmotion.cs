using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTrap_slowmotion : MonoBehaviour
{
    public float TimeScale_value;

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
            Time.timeScale = TimeScale_value;
        }
    }
}
