using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f; // 이동속도

    private Animator m_animator; // 애니메이터
    private Rigidbody2D m_body2d; // Rigidbody 움직임 관련
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;

    private bool m_grounded = true; // 땅에 있는가
    private bool m_rolling = false; // 구르고 있는가
    private int m_facingDirection = 1; // 좌우
    private float m_delayToIdle = 0.0f;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();

        m_animator.SetBool("Grounded", m_grounded); // 플레이어가 땅에 있다
    }

    void Update()
    {
        float inputX = UnityEngine.Input.GetAxis("Horizontal");

        if (inputX > 0) // 오른쪽?
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
        else if (inputX < 0) // 왼쪽?
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }

        if (!m_rolling) // 구르지 않고 있다면?
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);


        // 애니메이션 ============================================================================


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
