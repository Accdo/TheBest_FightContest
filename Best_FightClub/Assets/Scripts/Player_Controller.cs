using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f; // �̵��ӵ�

    private Animator m_animator; // �ִϸ�����
    private Rigidbody2D m_body2d; // Rigidbody ������ ����
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;

    private bool m_grounded = true; // ���� �ִ°�
    private bool m_rolling = false; // ������ �ִ°�
    private int m_facingDirection = 1; // �¿�
    private float m_delayToIdle = 0.0f;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();

        m_animator.SetBool("Grounded", m_grounded); // �÷��̾ ���� �ִ�
    }

    void Update()
    {
        float inputX = UnityEngine.Input.GetAxis("Horizontal");

        if (inputX > 0) // ������?
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
        else if (inputX < 0) // ����?
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }

        if (!m_rolling) // ������ �ʰ� �ִٸ�?
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);


        // �ִϸ��̼� ============================================================================


        //Run
        if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }


    }
}
