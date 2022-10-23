using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f; // 이동속도
    [SerializeField] float m_jumpForce = 7.5f; // 점프 가속
    [SerializeField] float m_rollForce = 6.0f;

    private Animator m_animator; // 애니메이터
    private Rigidbody2D m_body2d; // Rigidbody 움직임 관련
    private SpriteRenderer m_sprite;
    private Player_Sensor m_groundSensor;
    private Player_Sensor m_wallSensorR1;
    private Player_Sensor m_wallSensorR2;
    private Player_Sensor m_wallSensorL1;
    private Player_Sensor m_wallSensorL2;

    private bool m_grounded = true; // 땅에 있는가
    [SerializeField] private bool m_rolling = false; // 구르고 있는가
    private int m_facingDirection = 1; // 좌우

    private int m_currentAttack = 0; // 몇번째 공격인지
    private float m_timeSinceAttack = 0.0f; // 연속 공격의 딜레이를 주기위한 타이머
    private float m_delayToIdle = 0.0f; // 달리기 동작에서 기본자세로 가는 0.05초의 지연시간(애니메이션이 자연스러워짐)
    
    private float               m_rollDuration = 0.5f;
    private float               m_rollCurrentTime;

    // ===========================================================================================================

    public GameObject m_AttackSensor;
    public GameObject m_ParringSensor;

    bool GetHit = false; // 피격 얻음
    float AlphaRedTime = 0.0f; // 알파값 붉어지는 시간

    float Parring_Timer = 0.0f; // 패링 타이머
    bool can_Parring = true; // 패링을 할수 있는가

    float Rolling_Timer = 0.0f; // 구르기 타이머
    bool can_Rolling = true; // 굴러도 되는가

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Player_Sensor>();

        m_animator.SetBool("Grounded", m_grounded); // 플레이어가 땅에 있다

        m_AttackSensor.SetActive(false); // 공격 반경 비활성화
        m_ParringSensor.SetActive(false); // 패링 반경 비활성화
    }

    void Update()
    {
        // 연속 공격의 딜레이를 주기위한 타이머
        m_timeSinceAttack += Time.deltaTime;

        // 구르기 지속 시간을 확인하는 타이머 증가
        if(m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // 타이머가 지속 시간을 연장하는 경우 구르기 비활성화
        if(m_rollCurrentTime > m_rollDuration)
        {
            m_rolling = false;
            m_rollCurrentTime = 0.0f;
        }

        // 땅에 잇
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // 땅에 없
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

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

        // Move
        if (!m_rolling) // 구르지 않고 있다면?
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        
        // 공중에 있을 시?
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        if(!can_Parring)
        {
            Parring_Timer += Time.deltaTime;
            if(Parring_Timer >= 3.0f)
            {
                can_Parring = true;
                Parring_Timer = 0.0f;
            }
        }

        if(!can_Rolling)
        {
            Rolling_Timer += Time.deltaTime;
            if(Rolling_Timer >= 3.0f)
            {
                can_Rolling = true;
                Rolling_Timer = 0.0f;
            }
        }

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

        // Parring
        else if (Input.GetKeyDown(KeyCode.X) && !m_rolling && can_Parring)
        {
            m_animator.SetTrigger("Parring");
            //m_animator.SetBool("IdleBlock", true);
            can_Parring = false;
        }

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling && can_Rolling)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");

            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
            ///Debug.Log("Roll!");
            can_Rolling = false;
        }
    
        //Jump
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
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

        if(GetHit) // 맞기
        {
            StartCoroutine(OnHeatTime());

            GetHit = false;
        }

    }

    IEnumerator OnHeatTime()
    {
        int countTime = 0;

        while(countTime < 10){
            if(countTime%2 == 0)
                m_sprite.color = new Color32(255,150,150,255);
            else
                m_sprite.color = new Color32(255,50,50,255);

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }

        m_sprite.color = new Color32(255,255,255,255);

        // isUnBeatTime = false;

        yield return null;
    }

    public void AttackStart()
    {
        m_AttackSensor.SetActive(true);
    }
    public void AttackEnd()
    {
        m_AttackSensor.SetActive(false);
    }

    public void ParringStart()
    {
        m_ParringSensor.SetActive(true);
    }
    public void ParringEnd()
    {
        m_ParringSensor.SetActive(false);
    }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     if(other.gameObject.CompareTag("EnemyAttack"))
    //     {
    //         Debug.Log("Hit");
    //         GetHit = true;
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("EnemyAttack"))
        {
            Debug.Log("Hit");
            GetHit = true;
        }
    }
}
