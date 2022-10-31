using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Skill : MonoBehaviour
{
    private Animator m_ani;

    void Start()
    {
        m_ani = GetComponent<Animator>();
    }

    void Update()
    {
        
    }  

    public void Finish()
    {
        this.gameObject.SetActive(false);
    }
}
