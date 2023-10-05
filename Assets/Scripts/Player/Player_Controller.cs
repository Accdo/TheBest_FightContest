using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;
using Input = UnityEngine.Input;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] float m_hp = 100;
    [SerializeField] float m_mp = 100;

    [SerializeField] float m_speed = 8.0f; // 이동속도
    [SerializeField] float m_jumpForce = 12.0f; // 점프 가속
    [SerializeField] float m_rollForce = 20.0f;

    private Animator m_animator; // 애니메이터
    private Rigidbody2D m_body2d; // Rigidbody 움직임 관련
    private SpriteRenderer m_sprite;
    private Player_Sensor m_groundSensor;
    private Player_Sensor m_wallSensorR1;
    private Player_Sensor m_wallSensorR2;
    private Player_Sensor m_wallSensorL1;
    private Player_Sensor m_wallSensorL2;
    private bool m_isWallSliding = false;

    [SerializeField] private bool m_grounded = true; // 땅에 있는가
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

    float Hit_Timer = 0.0f; // 패링 무적 시간
    bool GetHit = false; // 피격 얻음

    float Parring_Timer = 0.0f; // 패링 타이머
    bool can_Parring = true; // 패링을 할수 있는가

    bool NoDamage = false;

    float Rolling_Timer = 0.0f; // 구르기 타이머
    bool can_Rolling = true; // 굴러도 되는가

    // ===========================================================================================================

    public GameObject m_UltiSkill; // 궁극기 통합
    public GameObject m_SecEffect; // 두번쨰 레이저
    bool state_Ulti = true; // 궁극기 상태 true : 기본 배경, false : 궁극기 배경

    public GameObject B_Skill; // 기본 스킬
    public Transform B_Skill_pos; // 기본 스킬 생성 좌표
    float B_Skill_Timer; // 기본스킬 타이머

    public Player_UI player_ui;

    bool IsDie = false;

    bool Charge_Start = false;
    [SerializeField] float Charge_Timer = 0.0f;

    bool UltiSkill_On = false; // 궁극기스킬 가능한지 (1스테이지, 2스테이지)

    void Start()
    {
        m_hp = 100;
        m_mp = 100;

        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Player_Sensor>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Player_Sensor>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Player_Sensor>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Player_Sensor>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Player_Sensor>();

        m_animator.SetBool("Grounded", m_grounded); // 플레이어가 땅에 있다

        m_AttackSensor.SetActive(false); // 공격 반경 비활성화
        m_ParringSensor.SetActive(false); // 패링 반경 비활성화
        m_UltiSkill.SetActive(false); // 패링 반경 비활성화

        B_Skill_Timer = 5.0f;
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
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = 1;

            m_AttackSensor.transform.localPosition = new Vector3(8f, -4f, 0.0f);

            B_Skill_pos.localPosition = new Vector3(13f, -3.4f, 0.0f);
            B_Skill.transform.localRotation  = Quaternion.Euler(0, 0, 0);
        }
        else if (inputX < 0) // 왼쪽?
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = -1;

            m_AttackSensor.transform.localPosition = new Vector3(-8f, -4f, 0.0f);

            B_Skill_pos.localPosition = new Vector3(-13f, -3.4f, 0.0f);
            B_Skill.transform.localRotation  = Quaternion.Euler(0, 180, 0);
        }

        // Move
        if (!m_rolling) // 구르지 않고 있다면?
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        
        // 공중에 있을 시?
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        //Wall Slide
        m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());

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

        if (SceneManager.GetActiveScene().buildIndex == 8 || SceneManager.GetActiveScene().buildIndex == 10)
        {
            UltiSkill_On = true;
        }
        else if(SceneManager.GetActiveScene().buildIndex == 9)
        {
            UltiSkill_On = false;
        }

        // 애니메이션 ============================================================================

        B_Skill_Timer += Time.deltaTime;

        //Die
        if (m_hp <= 0.0f)
        {
            IsDie = true;
            m_animator.SetTrigger("Die");

            StartCoroutine(FadeInFadeOut.Instance.FadeOutStart(SceneManager.GetActiveScene().buildIndex, 3.0f));

            this.gameObject.GetComponent<Player_Controller>().enabled = false;
        }

        else if (EffectManager.Instance.ParringComplete)
        {
            Charge_Timer += Time.deltaTime;

            if (!Charge_Start)
            {
                m_animator.SetTrigger("UltiCharging");
                Charge_Start = true;
            }

            if (Charge_Timer >= 2.0f)
            {
                m_animator.SetTrigger("UltiChargingShot");
                Charge_Start = false;

                EffectManager.Instance.ParringComplete = false;
                Charge_Timer = 0.0f;
            }
        }

        //Attack
        else if (Input.GetKeyDown(KeyCode.Z) && m_timeSinceAttack > 0.25f && !m_rolling && !GetHit)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 4)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3", "Attack4"
            m_animator.SetTrigger("Attack" + m_currentAttack);
            SoundManager.Instance.PlaySFXSound("Atk" + m_currentAttack, 0.5f);

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }

        // Heal
        else if (Input.GetKeyDown(KeyCode.B) && !m_rolling && can_Parring && !GetHit && m_mp > 0)
        {
            m_animator.SetTrigger("Heal");
            SoundManager.Instance.PlaySFXSound("Heal", 1.0f);

            if (!m_sprite.flipX)
                EffectManager.Instance.PlayEffect("effect_player_heal", transform.position + new Vector3(-0.4f, -1.0f, 0.0f));

            if (m_sprite.flipX)
                EffectManager.Instance.PlayEffect("effect_player_heal", transform.position + new Vector3(0.4f, -1.0f, 0.0f));

            //SoundManager.Instance.PlaySFXSound("Faring", 0.5f); 사운드

            if (m_hp < 100)
                m_hp += 20.0f;
                player_ui.GivePlayerHp(m_hp, 20.0f);

            if (m_mp < 100)
                m_mp += 20.0f;
                player_ui.GivePlayerMp(m_mp, 20.0f);
        }

        // BasicSkill
        else if (Input.GetKeyDown(KeyCode.C) && !m_rolling && can_Parring && !GetHit && m_mp > 0)
        {
            if(B_Skill_Timer >= 5.0f)
            {
                m_animator.SetTrigger("BasicSkill");
                //SoundManager.Instance.PlaySFXSound("Atk1", 0.5f);

                m_mp -= 10.0f;
                player_ui.GivePlayerMp(m_mp, -10.0f);

                B_Skill_Timer = 0.0f;
            }
        }

        // UltiSkill
        else if (Input.GetKeyDown(KeyCode.V) && !m_rolling && can_Parring && !GetHit && m_mp > 0 && UltiSkill_On)
        {
            if(state_Ulti) // 기본 배경, 궁극기 쓰기전
            {
                m_animator.SetTrigger("UltiSkill");
                SoundManager.Instance.PlaySFXSound("UltiSkill_Start_01", 0.5f);

                m_mp -= 20.0f;
                player_ui.GivePlayerMp(m_mp, -20.0f);

                state_Ulti = false;
            }
            else // 궁극기 배경, 궁극기 쓴 상태
            {
                m_animator.SetTrigger("UltiSkill_Fin");
                SoundManager.Instance.PlaySFXSound("UltiSkill_End_01", 0.5f);

                state_Ulti = true;
            }
        }

        // Parring
        else if (Input.GetKeyDown(KeyCode.X) && !m_rolling && can_Parring && !GetHit && m_mp > 0)
        {
            m_animator.SetTrigger("Parring");
            SoundManager.Instance.PlaySFXSound("Faring", 0.5f);
            can_Parring = false;

            m_mp -= 10.0f;
            player_ui.GivePlayerMp(m_mp, -10.0f);
        }

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling && can_Rolling && !GetHit && !m_isWallSliding)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            SoundManager.Instance.PlaySFXSound("Dash_01", 1.0f);

            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
            ///Debug.Log("Roll!");
            can_Rolling = false;
        }
    
        //Jump
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling && !GetHit)
        {
            m_animator.SetTrigger("Jump");
            SoundManager.Instance.PlaySFXSound("Player_Jump", 1.0f);

            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            //m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_body2d.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
            m_groundSensor.Disable(0.2f);

        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            //SoundManager.Instance.PlaySFXSound("walk", 1.0f);

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
            //StartCoroutine(OnHeatTime());
            
            Hit_Timer += Time.deltaTime;
            if(Hit_Timer >= 1.0f)
            {
                GetHit = false;
                Hit_Timer = 0.0f;
            }
        }

        
    }

    IEnumerator OnHeatTime()
    {
        int countTime = 0;

        while(countTime < 10){
            if (IsDie)
                break;

            if (countTime%2 == 0)
                m_sprite.color = new Color32(255,150,150,255);
            else
                m_sprite.color = new Color32(255,50,50,255);

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }
        if (!IsDie)
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

        NoDamage = true;
    }
    public void ParringEnd()
    {
        m_ParringSensor.SetActive(false);

        NoDamage = false;
    }

    public void UltiSkillStart()
    {
        m_UltiSkill.SetActive(true);
        //EffectManager.Instance.PlayEffect("Effect_BG");
    }

    public void UltiSkillFinish()
    {
        m_SecEffect.SetActive(true);
        //EffectManager.Instance.PlayEffect("SecondLager");
    }

    public void BasickillStart()
    {
        //Instantiate(B_Skill, B_Skill_pos.position, Quaternion.identity);
        EffectManager.Instance.PlayEffect("Basic_Skill_Fog", B_Skill_pos.position);
    }

    public void ChargeStart()
    {
        EffectManager.Instance.PlayEffect("eff_boss2_charg1", transform.position, 2.0f);
        EffectManager.Instance.PlayEffect("efft_boss2_chargingGround", transform.position);
        EffectManager.Instance.PlayEffect("efft_boss2_chargingWind", transform.position);
        SoundManager.Instance.PlaySFXSound("Ulti_Charge", 0.5f);
    }

    public void UltiBombStart()
    {
        EffectManager.Instance.PlayEffect("eff_boss2_chargeBomb", transform.position);

        //GameObject _gameObject;
        //Ulti_Arrow _ultiArrow;

        //_gameObject = UltiArrow;
        //_ultiArrow = UltiArrow.GetComponent<Ulti_Arrow>();

        //if (!m_spriterend.flipX)
        //    _ultiArrow.SetXpos(1);
        //else
        //    _ultiArrow.SetXpos(2);
        //Instantiate(_gameObject, transform.position, Quaternion.identity);
    }

    public void UltiWaveStart()
    {
        if (m_sprite.flipX)
            EffectManager.Instance.PlayEffectS("eff_boss2_ultiWave", transform.position + new Vector3(6.0f, 1.3f, 0.0f), -1.0f);
        else
            EffectManager.Instance.PlayEffectS("eff_boss2_ultiWave", transform.position + new Vector3(6.0f, 1.3f, 0.0f), 1.0f);

        SoundManager.Instance.PlaySFXSound("Ulti_Explosion", 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(!NoDamage && !GetHit)
        {
            if (other.gameObject.CompareTag("Boss1_Attack"))
            {
                m_hp -= 10.0f;
                player_ui.GivePlayerHp(m_hp, -10.0f);

                EffectManager.Instance.PlayEffect("eff_boss1_atkbomb", transform.position + new Vector3(0, -1, 0));
                if (!IsDie)
                    StartCoroutine(OnHeatTime());
                GetHit = true;
            }

            if (other.gameObject.CompareTag("Boss2_Attack"))
            {
                m_hp -= 10.0f;
                player_ui.GivePlayerHp(m_hp, -10.0f);

                EffectManager.Instance.PlayEffect("eff_boss2_atk_hit", transform.position + new Vector3(0, -1, 0));
                if (!IsDie)
                    StartCoroutine(OnHeatTime());
                GetHit = true;
            }

            if (other.gameObject.CompareTag("Arrow"))
            {
                m_hp -= 10.0f;
                player_ui.GivePlayerHp(m_hp, -10.0f);
                if (!IsDie)
                    StartCoroutine(OnHeatTime());
                GetHit = true;
            }

            if (other.gameObject.CompareTag("WolfAttack"))
            {
                m_hp -= 10.0f;
                player_ui.GivePlayerHp(m_hp, -10.0f);
                if (!IsDie)
                    StartCoroutine(OnHeatTime());
                GetHit = true;
            }

            if (other.gameObject.CompareTag("UltiArrow"))
            {
                m_hp -= 40.0f;
                player_ui.GivePlayerHp(m_hp, -40.0f);
                if (!IsDie)
                    StartCoroutine(OnHeatTime());
                GetHit = true;
            }
        }
        
    }
}
