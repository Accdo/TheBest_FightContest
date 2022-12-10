using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obejct_Effect : MonoBehaviour
{
    public float deltaTime = 0.7f;

    void Start()
    {
        
    }

    void Update()
    {
        deltaTime -= Time.deltaTime;

        if(deltaTime <= 0)
            Destroy(this.gameObject);
    }
}
