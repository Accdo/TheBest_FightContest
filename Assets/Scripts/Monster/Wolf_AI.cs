using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wolf_AI : MonoBehaviour
{
    [SerializeField] float m_hp = 100;

    [SerializeField] float m_speed = 1.0f;
    [SerializeField] float m_dashspeed = 15.0f;

    public GameObject MonsterHP;

    private Animator m_animator;
    private SpriteRenderer m_spriterend;

    // Walk Pattern 

    public float leftMove_targetPos; // 좌우 거리 7
    public float rightMove_targetPos;
    int move_num; // 1 : left , 2 : right

    // Attack Pattern

    public Wolf_DetectSensor DetectSensor; // 플레이어 감지 스크립트
    public Transform m_AttackSensor_pos; // 공격 트리거 위치

    public Transform Traget_Player; // 플레이어 타겟 위치

    public GameObject m_AttackSensor; // 공격 트리거

    // ETC
    bool HealCheck = false; // 힐팩에 있는지 체크
    bool GetHit = false;
    float Hit_Timer = 0.0f; // =====================================================================================================================================================

    public Boss_UI boss_ui;
    bool IsDie = false;

    Color color = new Color32(255,255,255,255);

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_spriterend = GetComponent<SpriteRenderer>();

        move_num = 1;
    }
    
    void Update()
    {
        if(m_hp <= 0.0f)
        {
            m_animator.SetTrigger("Die");
        }
        else
        {
            if(GetHit) // �±�
            {
                Hit_Timer += Time.deltaTime;
                if(Hit_Timer >= 1.0f)
                {
                    GetHit = false;
                    Hit_Timer = 0.0f;
                }
            }
            else
            {
                if(DetectSensor.AttackStart)
                {
                    Attack_Pattern();
                }
                else
                {
                    Walk_Pattern();
                }
            }
        }

        if (HealCheck)
        {
            if (m_hp <= 100)
            {
                m_hp += 30.0f * Time.deltaTime;
                boss_ui.GiveBossHp(m_hp);
            }
        }

        if(IsDie)
        {
            color.a -= Time.deltaTime;
            m_spriterend.color = color;
        }
    }

    void Walk_Pattern()
    {
        if(move_num == 1)
        {
            m_spriterend.flipX = false;
            m_AttackSensor_pos.localPosition = new Vector3(-2.5f, -2.0f, 0.0f);

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(leftMove_targetPos, transform.position.y, transform.position.z), m_speed * Time.deltaTime);

            if(transform.position.x == leftMove_targetPos)
                move_num = 2;
        }
        else if(move_num == 2)
        {
            m_spriterend.flipX = true;
            m_AttackSensor_pos.localPosition = new Vector3(2.5f, -2.0f, 0.0f);

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(rightMove_targetPos, transform.position.y, transform.position.z), m_speed * Time.deltaTime);

            if(transform.position.x == rightMove_targetPos)
                move_num = 1;
        }
    }

    void Attack_Pattern()
    {
        m_animator.SetBool("pattern_TF", true);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Traget_Player.position.x, transform.position.y, transform.position.z), m_speed * Time.deltaTime);

        if(Traget_Player.position.x > transform.position.x)
        {
            m_spriterend.flipX = true;
            m_AttackSensor_pos.localPosition = new Vector3(2.5f, -2.0f, 0.0f);
        }
        else if(Traget_Player.position.x < transform.position.x)
        {
            m_spriterend.flipX = false;
            m_AttackSensor_pos.localPosition = new Vector3(-2.5f, -2.0f, 0.0f);
        }
    }

    void RushStart()
    {
        m_speed = m_dashspeed;

        SoundManager.Instance.PlaySFXSound("Wolf_Atk", 0.5f);

        m_AttackSensor.SetActive(true);
    }
    void RushEnd()
    {
        m_speed = 4.0f;

        m_animator.SetBool("pattern_TF", false);
        DetectSensor.AttackStart = false;

        m_AttackSensor.SetActive(false);
    }
    public void MonsterDie()
    {
        IsDie = true;

        StopCoroutine(OnHeatTime());

        Destroy(this.gameObject, 2.0f);
    }

    IEnumerator OnHeatTime()
    {
        int countTime = 0;

        while(countTime < 10){
            if(IsDie)
                break;

            if(countTime%2 == 0)
                m_spriterend.color = new Color32(255,150,150,255);
            else
                m_spriterend.color = new Color32(255,50,50,255);

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }
        if(!IsDie)
            m_spriterend.color = new Color32(255,255,255,255);

        yield return null;
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(m_hp > 0.0f)
        {
            if(other.gameObject.CompareTag("PlayerAttack"))
            {
                MonsterHP.SetActive(true);
                m_hp -= 10.0f;
                boss_ui.GiveBossHp(m_hp); // ========================================================================================================================
    
                EffectManager.Instance.PlayEffect("player_atk_Bomb", transform.position);
    
                Debug.Log("Hit");
                if(!IsDie)
                    StartCoroutine(OnHeatTime());
    
                Hit_Timer = 0.0f;
            }

            if(other.gameObject.CompareTag("PlayerBasicSkill"))
            {
                MonsterHP.SetActive(true);
                m_hp -= 30.0f;
                boss_ui.GiveBossHp(m_hp); // ========================================================================================================================

                EffectManager.Instance.PlayEffect("Basic_Skill", transform.position);
                
                Debug.Log("BasicHit");
                if(!IsDie)
                    StartCoroutine(OnHeatTime());
                
                Hit_Timer = 0.0f;
            }
        }

        if (other.gameObject.CompareTag("Stone"))
        {
            m_animator.SetTrigger("Die");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("HealRage"))
        {
            HealCheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HealRage"))
        {
            HealCheck = false;
        }
    }

}