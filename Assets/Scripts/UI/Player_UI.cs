using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    public Image player_hp;
    public Image player_mp;

    float current_hp = 100;
    float get_hp = 100;
    float increase_Hp; // 증감

    float current_mp = 100;
    float get_mp = 100;
    float increase_Mp; // 증감

    [SerializeField]
    float increase_speed = 30.0f;
    [SerializeField]
    float plus_mp_speed = 1.0f;

    void Start()
    {
        increase_Hp = 0.0f;
        increase_Mp = 0.0f;
    }

    void Update()
    {
        if(current_mp <= 100) // 마나 자동 재생
            current_mp += plus_mp_speed * Time.deltaTime;



        if(increase_Hp >= 0) // 증가
        {
            if(current_hp <= get_hp)
                current_hp += increase_speed * Time.deltaTime;
        }
        else // 감소
        {
            if(current_hp >= get_hp)
                current_hp -= increase_speed * Time.deltaTime;
        }
        if(increase_Mp < 0)// 감소
        {
            if(current_mp >= get_mp)
            {
                current_mp -= increase_speed * Time.deltaTime;
            }
            else
            {
                increase_Mp = 0.0f;
            }
        }

        player_hp.fillAmount = current_hp / 100.0f;
        player_mp.fillAmount = current_mp / 100.0f;
    }
    
    public void GivePlayerHp(float _hp, float _increaseHp)
    {
        get_hp = _hp;
        increase_Hp = _increaseHp;
    }
    public void GivePlayerMp(float _mp, float _increaseMp)
    {
        get_mp = _mp;
        increase_Mp = _increaseMp;
    }
}