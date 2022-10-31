using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_UltiSkill : MonoBehaviour
{
    public GameObject Sec_Effect;


    void Start()
    {
        Invoke("On_Eff", 0.3f);

        Invoke("End_UltiSkill", 15);
    }

    void Update()
    {
        
    }

    void On_Eff()
    {
        Sec_Effect.SetActive(true);
    }

    void End_UltiSkill()
    {
        this.gameObject.SetActive(false);
    }
}