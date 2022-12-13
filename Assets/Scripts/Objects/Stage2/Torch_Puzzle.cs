using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch_Puzzle : MonoBehaviour
{
    bool IsFire = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            if(!IsFire)
            {
                EffectManager.Instance.PlayEffect("effect_torchlighit_starting", transform.position + new Vector3(0.0f, 1.25f, 0.0f));

                IsFire = true;
            }
        }
    }
}
