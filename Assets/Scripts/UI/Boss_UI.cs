using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_UI : MonoBehaviour
{
    public Image boss_hp;
    float get_hp = 100;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        boss_hp.fillAmount = get_hp / 100.0f;
    }

    public void GiveBossHp(float _hp)
    {
        get_hp = _hp;
    }
}
