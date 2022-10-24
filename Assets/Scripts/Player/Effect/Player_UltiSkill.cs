using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_UltiSkill : MonoBehaviour
{
    public GameObject Sec_Effect;


    void Start()
    {
        Invoke("On_Eff", 0.3f);
    }

    void Update()
    {
        
    }

    void On_Eff()
    {
        Sec_Effect.SetActive(true);
    }
}