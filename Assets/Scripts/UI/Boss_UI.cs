using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_UI : MonoBehaviour
{
    public Image boss_hp;
    public float get_hp = 300;
    public float Full_hp;

    public string WhoIs;

    float DeleteTime = 0.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        boss_hp.fillAmount = get_hp / Full_hp;

        if(WhoIs == "Monster")
        {
            DeleteTime += Time.deltaTime;
            if (DeleteTime >= 3.0f)
            {
                this.gameObject.SetActive(false);
                DeleteTime = 0.0f;
            }
        }
    }

    public void GiveBossHp(float _hp)
    {
        get_hp = _hp;
    }
}
