using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Vector3 dir;

    public float speed = 10.0f;
    public float lifeTime = 3.0f;

    private void Start()
    {
        dir = Vector3.left;

        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {

        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("PlayerParring"))
        {
            Debug.Log("Parring");
            dir = Vector3.right;

            speed = 15;
            lifeTime = 4.5f;
        }
    }
}
