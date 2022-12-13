using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private SpriteRenderer m_sprite;
    public SpriteRenderer shadow_sprite;

    Vector3 dir;

    public float speed = 10.0f;
    public float lifeTime = 3.0f;

    public GameObject PlayerBomb; 

    private void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();

        dir = Vector3.left;

        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("PlayerParring"))
        {
            EffectManager.Instance.PlayEffect("player_parry_bomb", transform.position);

            gameObject.tag = "ParriedArrow";

            dir = Vector3.right;

            m_sprite.flipX = true;
            shadow_sprite.flipX = true;

            speed = 30;
            lifeTime = 4.5f;
        }
    }
}