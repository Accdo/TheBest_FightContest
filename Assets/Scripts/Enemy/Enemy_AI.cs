using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f; // 이동속도
    [SerializeField] float m_dashspeed = 15.0f; // 이동속도

    private Animator m_animator; // 애니메이터
    private Rigidbody2D m_body2d; // Rigidbody 움직임 관련
    private SpriteRenderer m_spriterend;

    public Transform Traget_Player;

    float pattern_timer1;

    bool DashFinish = false;
    bool AttackFinish = false;

    int HP = 10;

    public GameObject m_AttackSensor;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_spriterend = GetComponent<SpriteRenderer>();

        m_AttackSensor.SetActive(false); // 공격 반경 비활성화
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
            m_animator.SetInteger("Pattern_State1", 1); // 달리기

            m_speed = 4f;
            
            if(pattern_timer1 >= 3.5f)
            {
                m_animator.SetInteger("Pattern_State1", 2); // 대쉬
                m_speed = m_dashspeed;

                if(DashFinish && pattern_timer1 >= 3.6f)
                {
                    m_animator.SetBool("Attack", true); // 공격

                    m_speed = 0;

                    pattern_timer1 = 0.0f;
                }
            }
        }
        
        
        if(Traget_Player.position.x > transform.position.x) // 왼쪽 방향 보기
        {
            m_spriterend.flipX = false;

            m_AttackSensor.transform.localPosition = new Vector3(0.3f, 0.0f, 0.0f);
        }
        else if(Traget_Player.position.x < transform.position.x) // 오른쪽 방향 보기
        {
            m_spriterend.flipX = true;

            m_AttackSensor.transform.localPosition = new Vector3(-0.3f, 0.0f, 0.0f);
        }
    }

    void DashEnd()
    {
        DashFinish = true;
    }

    public void AttackStart()
    {
        m_AttackSensor.SetActive(true);
    }
    public void AttackEnd()
    {
        m_AttackSensor.SetActive(false);
    }

    IEnumerator OnHeatTime()
    {
        int countTime = 0;

        while(countTime < 10){
            if(countTime%2 == 0)
                m_spriterend.color = new Color32(255,150,150,255);
            else
                m_spriterend.color = new Color32(255,50,50,255);

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }

        m_spriterend.color = new Color32(255,255,255,255);

        // isUnBeatTime = false;

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("PlayerAttack"))
        {
            Debug.Log("Hit");
            StartCoroutine(OnHeatTime());
        }
    }
}