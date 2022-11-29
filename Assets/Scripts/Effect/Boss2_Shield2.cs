using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_Shield2 : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ShieldStand()
    {
        EffectManager.Instance.PlayEffect("eff_boss2_Shielding", transform.position);
    }
}
