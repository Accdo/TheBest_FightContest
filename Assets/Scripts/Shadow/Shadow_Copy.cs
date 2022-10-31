using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow_Copy : MonoBehaviour
{
    private SpriteRenderer my_animator;

    public SpriteRenderer main_animator;

    public bool IsGround = true;

    private Vector3 first_p_vec; // 처음 플레이어 좌표
    public Transform player_transform; // 변하는 플레이어 좌표

    private Vector3 first_shadow_vec; // 처음 그림자 좌표

    float Increase; // 증감

    void Start()
    {
        my_animator = GetComponent<SpriteRenderer>();

        first_p_vec = player_transform.position;
        first_shadow_vec = transform.position;
    }

    void Update()
    {
        my_animator.sprite = main_animator.sprite;
        my_animator.flipX = main_animator.flipX;
        
        Increase = player_transform.position.y - first_p_vec.y;

        if(!IsGround)
            transform.position = new Vector3(transform.position.x, first_shadow_vec.y - (Increase * 0.2f), transform.position.z);
    }
}
