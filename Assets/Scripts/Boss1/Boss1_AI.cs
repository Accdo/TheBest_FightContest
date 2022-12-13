using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_AI : MonoBehaviour
{
    [SerializeField] float m_hp = 300;

    [SerializeField] float m_speed = 6.0f; //
    [SerializeField] float m_dashspeed = 20.0f; // 

    private Animator m_animator; // 
    private Rigidbody2D m_body2d; // Rigidbody 
    private SpriteRenderer m_spriterend;

    // ==========================================================

    public Transform Traget_Player;

    [SerializeField] float pattern_timer1;

    bool DashFinish = false;

    public GameObject m_AttackSensor;

    // ==========================================================
    bool GetHit = false;
    float Hit_Timer = 0.0f;

    public Boss_UI boss_ui; // UI

    bool IsDie = false; //

    bool StartDie = false;

    // ==========================================================
    [SerializeField]
    int Pattern_Count = 0;

    bool IsShield = false; // 쉴드 상태인가

    bool Shield_Start = false;
    float Shield_Timer = 0.0f;

    bool Charge_Start = false;
    float Charge_Timer = 0.0f;

    [SerializeField]
    int Pattern_State = 1; // 1 shield 2 UltiSkill

    public GameObject potal;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_spriterend = GetComponent<SpriteRenderer>();

        m_AttackSensor.SetActive(false); //
    }

    Color color = new Color32(255, 255, 255, 255);
    void Update()
    {
        if (m_hp <= 0.0f)
        {
            if(!StartDie)
            {
                m_animator.SetTrigger("Die");
                StartDie = true;
            }
        }
        else
        {
            if (GetHit)
            {
                Hit_Timer += Time.deltaTime;
                if (Hit_Timer >= 1.0f)
                {
                    GetHit = false;
                    Hit_Timer = 0.0f;
                }
            }

            if (Pattern_Count >= 3)
            {
                if (Pattern_State == 1)
                {
                    Shield_Pattern();
                }
                else if (Pattern_State == 2)
                {
                    UltiSkll_Pattern();
                }
            }
            else
                Pattern1();
        }

        if (IsDie)
        {
            color.a -= Time.deltaTime;
            m_spriterend.color = color;
        }
        else
        {
            if(Pattern_State != 2)
            {
                color.a += Time.deltaTime;
                m_spriterend.color = color;
            }        
        }
    }

    void Pattern1()
    {
        pattern_timer1 += Time.deltaTime;

        if (pattern_timer1 >= 1.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Traget_Player.position.x, transform.position.y, 0.0f), m_speed * Time.deltaTime);

            m_animator.SetBool("Attack", false);
            m_animator.SetInteger("Pattern_State1", 1);

            m_speed = 6f;

            if (pattern_timer1 >= 3.5f)
            {
                m_animator.SetInteger("Pattern_State1", 2);

                m_speed = m_dashspeed;

                if (DashFinish && pattern_timer1 >= 3.6f)
                {
                    m_animator.SetBool("Attack", true);

                    m_speed = 0;
                    ++Pattern_Count;
                    pattern_timer1 = 0.0f;
                }
            }
        }

        if (Traget_Player.position.x > transform.position.x)
        {
            m_spriterend.flipX = true;

            m_AttackSensor.transform.localPosition = new Vector3(3f, 0.0f, 0.0f);
        }
        else if (Traget_Player.position.x < transform.position.x)
        {
            m_spriterend.flipX = false;

            m_AttackSensor.transform.localPosition = new Vector3(-2f, 0.0f, 0.0f);
        }
    }

    void Shield_Pattern()
    {
        Shield_Timer += Time.deltaTime;

        if (!Shield_Start)
        {
            m_animator.SetBool("Shield_End", false);
            m_animator.SetTrigger("Shield");
            Shield_Start = true;
        }

        if (Shield_Timer >= 3.5f)
        {
            Pattern_Count = 0;
            Pattern_State = 2;
            IsShield = false;

            m_animator.SetBool("Shield_End", true);
            Shield_Start = false;

            Shield_Timer = 0.0f;
        }
    }

    void UltiSkll_Pattern()
    {
        Charge_Timer += Time.deltaTime;

        if (!Charge_Start)
        {
            m_animator.SetTrigger("UltiGo");
            Charge_Start = true;
        }

        if (Charge_Timer >= 2.0f)
        {
            Pattern_Count = 0;
            Pattern_State = 1;

            transform.position = new Vector3(0.0f, transform.position.y, transform.position.z);

            m_animator.SetTrigger("UltiGoGo");
            Charge_Start = false;

            Charge_Timer = 0.0f;
        }
        else
        {
            color.a -= Time.deltaTime;
            m_spriterend.color = color;
        }
    }

    void DashEnd()
    {
        DashFinish = true;
    }

    void AttackStart()
    {
        m_AttackSensor.SetActive(true);
        SoundManager.Instance.PlaySFXSound("Boss1_Atk_1", 0.5f);
    }
    void AttackEnd()
    {
        m_AttackSensor.SetActive(false);
        SoundManager.Instance.PlaySFXSound("Boss1_Atk_2", 0.5f);
    }

    public void ShieldStart()
    {
        IsShield = true;
    }
    public void ShieldStand() // 안쓰는중
    {
        
    }

    public void UltiStart()
    {
        EffectManager.Instance.PlayEffect("eff_boss1__teleport", transform.position);
        SoundManager.Instance.PlaySFXSound("Teleport", 0.5f);
    }

    public void UltiDeathSlash1()
    {
        EffectManager.Instance.PlayEffect("eff_boss1_UpAttack", transform.position);
        SoundManager.Instance.PlaySFXSound("Ulti_1", 0.5f);
    }

    public void UltiDeathSlash2()
    {
        EffectManager.Instance.PlayEffect("eff_boss1_scratchX", transform.position);
        SoundManager.Instance.PlaySFXSound("Ulti_2,3", 0.5f);
    }


    public void BossDie()
    {
        IsDie = true;

        StopCoroutine(OnHeatTime());

        potal.SetActive(true);
        Destroy(this.gameObject, 2.0f);
    }

    IEnumerator OnHeatTime()
    {
        int countTime = 0;

        while (countTime < 10)
        {
            if (IsDie)
                break;

            if (countTime % 2 == 0)
                m_spriterend.color = new Color32(255, 150, 150, 255);
            else
                m_spriterend.color = new Color32(255, 50, 50, 255);

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }
        if (!IsDie)
            m_spriterend.color = new Color32(255, 255, 255, 255);

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (m_hp > 0.0f && !IsShield)
        {
            if (other.gameObject.CompareTag("PlayerAttack"))
            {
                m_hp -= 10.0f;
                boss_ui.GiveBossHp(m_hp);

                EffectManager.Instance.PlayEffect("player_atk_Bomb", transform.position);

                GetHit = true;
                Debug.Log("Hit");
                if (!IsDie)
                    StartCoroutine(OnHeatTime());

                Hit_Timer = 0.0f;
            }

            if (other.gameObject.CompareTag("PlayerUltiSkill"))
            {
                m_hp -= 30.0f;
                boss_ui.GiveBossHp(m_hp);

                EffectManager.Instance.PlayEffect("player_atk_Bomb", transform.position);

                GetHit = true;
                Debug.Log("Hit");
                if (!IsDie)
                    StartCoroutine(OnHeatTime());

                Hit_Timer = 0.0f;
            }

            if (other.gameObject.CompareTag("PlayerBasicSkill"))
            {
                m_hp -= 30.0f;
                boss_ui.GiveBossHp(m_hp);

                EffectManager.Instance.PlayEffect("Basic_Skill", transform.position);

                GetHit = true;
                Debug.Log("BasicHit");
                if (!IsDie)
                    StartCoroutine(OnHeatTime());

                Hit_Timer = 0.0f;
            }
        }
    }
}
