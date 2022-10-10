using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f; // �̵��ӵ�
    [SerializeField] float m_dashspeed = 15.0f; // �̵��ӵ�

    private Animator m_animator; // �ִϸ�����
    private Rigidbody2D m_body2d; // Rigidbody ������ ����
    private SpriteRenderer m_spriterend;

    public Transform Traget_Player;

    float pattern_timer1;

    bool DashFinish = false;
    bool AttackFinish = false;

    int HP = 10;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_spriterend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Pattern1();
    }

    void Pattern1()
    {
        pattern_timer1 += Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Traget_Player.position.x, transform.position.y, 0.0f), m_speed * Time.deltaTime);

        if(pattern_timer1 >= 1.5f)
        {
            m_animator.SetBool("Attack", false);
            m_animator.SetInteger("Pattern_State1", 1); // �޸���

            m_speed = 4f;
            
            if(pattern_timer1 >= 3.5f)
            {
                m_animator.SetInteger("Pattern_State1", 2); // �뽬
                m_speed = m_dashspeed;

                if(DashFinish && pattern_timer1 >= 3.6f)
                {
                    m_animator.SetBool("Attack", true); // ����

                    m_speed = 0;

                    pattern_timer1 = 0.0f;
                }
            }
        }
        
        
        if(Traget_Player.position.x > transform.position.x) // ���� ���� ����
        {
            m_spriterend.flipX = false;
        }
        else if(Traget_Player.position.x < transform.position.x) // ������ ���� ����
        {
            m_spriterend.flipX = true;
        }
    }

    void DashEnd()
    {
        DashFinish = true;
    }
    void AttackEnd()
    {
        AttackFinish = true;
    }
}