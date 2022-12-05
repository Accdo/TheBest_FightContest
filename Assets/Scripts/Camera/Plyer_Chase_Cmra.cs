using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plyer_Chase_Cmra : MonoBehaviour
{
    public float cameraSpeed = 5.0f;

    public GameObject player;


    void Start()
    {
        
    }

    void Update()
    {
        if(transform.position.x >= 16.1f)
        {
            transform.position = new Vector3(16.1f, 0, transform.position.z);

            if(player.transform.position.x < 10.0f)
            {
                transform.position = new Vector3(16.09999f, 0, transform.position.z);
            }
        }
        else
            transform.position = new Vector3(player.transform.position.x + 6.0f, 0, transform.position.z);

        
        
    }
}
