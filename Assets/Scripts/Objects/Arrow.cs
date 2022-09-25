using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10.0f;
    public float lifeTime = 3.0f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        Vector3 dir = Vector3.left;

        transform.position += dir * speed * Time.deltaTime;
    }

}
