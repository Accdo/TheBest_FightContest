using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scarecrow_Shake : MonoBehaviour
{
    public float zRot; // 각도
    public float time; // 시간

    bool firstHit = false; // 첫번쨰 피격

    Sequence ShakeSequence;

    void Start()
    {
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("PlayerAttack"))
        {
            Debug.Log("Hit");
            
            Shake_it();
        }
    }

    void Shake_it() // 양쪽으로 기울어짐
    {
        ShakeSequence = DOTween.Sequence()
        .OnStart(() => {})
        .Append(transform.DORotate(new Vector3(0.0f,0.0f, zRot), time, RotateMode.Fast))
        .Append(transform.DORotate(new Vector3(0.0f,0.0f, -zRot), time, RotateMode.Fast))
        .Append(transform.DORotate(new Vector3(0.0f,0.0f, zRot), time, RotateMode.Fast))
        .Append(transform.DORotate(new Vector3(0.0f,0.0f, -zRot), time, RotateMode.Fast))
        .Append(transform.DORotate(new Vector3(0.0f,0.0f, 0.0f), time, RotateMode.Fast));
    }

    void Shake_itt() // 오른쪽으로 기울어짐
    {
        ShakeSequence = DOTween.Sequence()
        .OnStart(() => {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, -zRot);
        })
        .Append(transform.DOShakeRotation(time, new Vector3(0.0f,0.0f, zRot), 0).SetLoops(2))
        .Append(transform.DORotate(new Vector3(0.0f,0.0f, 0.0f), time, RotateMode.Fast));
    }
}
