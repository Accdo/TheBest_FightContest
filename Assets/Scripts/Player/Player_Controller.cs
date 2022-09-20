using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

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

    private int m_currentAttack = 0; // 몇번째 공격인지
    private float m_timeSinceAttack = 0.0f; // 연속 공격의 딜레이를 주기위한 타이머
    private float m_delayToIdle = 0.0f; // 달리기 동작에서 기본자세로 가는 0.05초의 지연시간(애니메이션이 자연스러워짐)

    // ===========================================================================================================

    public GameObject m_AttackSensor;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();

        m_animator.SetBool("Grounded", m_grounded); // 플레이어가 땅에 있다

        m_AttackSensor.SetActive(false); // 공격 반경 비활성화
    }

    void Update()
    {
        // 연속 공격의 딜레이를 주기위한 타이머
        m_timeSinceAttack += Time.deltaTime;

        float inputX = Input.GetAxis("Horizontal");

        if (inputX > 0) // 오른쪽?
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;

            m_AttackSensor.transform.localPosition = new Vector3(0.8f, 0.8f, 0.0f);
        }
        else if (inputX < 0) // 왼쪽?
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;

            m_AttackSensor.transform.localPosition = new Vector3(-0.8f, 0.8f, 0.0f);
        }

        if (!m_rolling) // 구르지 않고 있다면?
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);


        // 애니메이션 ============================================================================

        //Attack
        if (Input.GetKeyDown(KeyCode.Z) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }
    
        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
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

    public void AttackStart()
    {
        m_AttackSensor.SetActive(true);
    }
    public void AttackEnd()
    {
        m_AttackSensor.SetActive(false);
    }
}
