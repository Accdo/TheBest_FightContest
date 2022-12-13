using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_End : MonoBehaviour
{
    int count = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Eff_End()
    {
        Destroy(this.gameObject);
    }

    public void Eff_Thd_End()
    {
        Destroy(this.gameObject, 1.0f);
    }

    public void Eff_ThreeCycle_End()
    {
        ++count;
        if(count >= 3)
        {
            Destroy(this.gameObject);
        }
    }
}
