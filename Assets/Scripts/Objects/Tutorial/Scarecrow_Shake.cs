using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scarecrow_Shake : MonoBehaviour
{
    public float zRot; 
    public float time; 

    bool firstHit = false;

    Sequence ShakeSequence;

    public GameObject Eff_dust;
    public GameObject Eff_straw;

    // ==============================

    public int HitCount; // 튜토리얼 조건
    public GameObject dialogwindow; // 대화창

    public GameObject PlayerBomb; // 플레이어 이펙트
    
    void Start()
    {
        HitCount = 0;
    }

    void Update()
    {
        if(HitCount >= 10)
        {
            dialogwindow.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("PlayerAttack"))
        {
            //PlayerBomb.SetActive(true);
            Instantiate(PlayerBomb, transform.position + new Vector3(0,3f,0), Quaternion.identity);

            ++HitCount;
            
            Shake_it();
        }
    }

    void Shake_it() // 
    {
        ShakeSequence = DOTween.Sequence()
        .OnStart(() => {})
        .Append(transform.DORotate(new Vector3(0.0f,0.0f, zRot), time, RotateMode.Fast))
        .Append(transform.DORotate(new Vector3(0.0f,0.0f, -zRot), time, RotateMode.Fast))
        .Append(transform.DORotate(new Vector3(0.0f,0.0f, zRot), time, RotateMode.Fast))
        .Append(transform.DORotate(new Vector3(0.0f,0.0f, -zRot), time, RotateMode.Fast))
        .Append(transform.DORotate(new Vector3(0.0f,0.0f, 0.0f), time, RotateMode.Fast));

        Instantiate(Eff_dust, new Vector3(4,0,0), transform.rotation);
        Instantiate(Eff_straw, new Vector3(4,0,0), transform.rotation);
    }

    void Shake_itt() // 
    {
        ShakeSequence = DOTween.Sequence()
        .OnStart(() => {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, -zRot);
        })
        .Append(transform.DOShakeRotation(time, new Vector3(0.0f,0.0f, zRot), 0).SetLoops(2))
        .Append(transform.DORotate(new Vector3(0.0f,0.0f, 0.0f), time, RotateMode.Fast));
    }
}
