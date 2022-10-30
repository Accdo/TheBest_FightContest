using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    public Image player_hp;
    public Image player_mp;

    float get_hp = 100;
    float get_mp = 100;

    void Start()
    {
        
    }

    void Update()
    {
        player_hp.fillAmount = get_hp / 100.0f;
        player_mp.fillAmount = get_mp / 100.0f;
    }

    public void GivePlayerHp(float _hp)
    {
        get_hp = _hp;
    }
    public void GivePlayerMp(float _mp)
    {
        get_mp = _mp;
    }
}
