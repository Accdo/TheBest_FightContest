using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFire : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void NextFire()
    {
        EffectManager.Instance.PlayEffect("effect_torchlighit_loop", transform.position);

        Destroy(this.gameObject);
    }
}
