using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_UltiSkill : MonoBehaviour
{
    public GameObject BG_Eff;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void End_BG_Eff()
    {
        BG_Eff.SetActive(false);
    }

    public void End_Sec_Eff()
    {
        this.gameObject.SetActive(false);
    }
}