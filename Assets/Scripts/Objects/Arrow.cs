using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rigid;

    public float speed = 10.0f;
    public float lifeTime = 3.0f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        rigid.velocity = transform.up * -1.0f * speed;
    }

}
