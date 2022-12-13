using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ulti_Arrow : MonoBehaviour
{
    Vector3 dir = Vector3.left;

    public float speed = 30.0f;
    public float lifeTime = 3.0f;

    public int num;

    

    private void Start()
    {
        //dir = Vector3.left;

        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    public void SetXpos(int _pos) // Left : 1 Right : 2
    {
        num = _pos;

        if (_pos == 1)
        {
            dir = Vector3.left;
        }
        if (_pos == 2)
        {
            dir = Vector3.right;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerParring"))
        {
            Debug.Log("Parring");

            EffectManager.Instance.PlayEffect("player_parry_bomb", transform.position);

            gameObject.tag = "ParriedUltiArrow";

            if (num == 1)
            {
                dir = Vector3.right;
            }
            if (num == 2)
            {
                dir = Vector3.left;
            }

            speed = 0;
            lifeTime = 7.0f;

            EffectManager.Instance.ParringComplete = true;
            Invoke("PlayerParringShotting", 2.0f);
        }
    }

    void PlayerParringShotting()
    {
        speed = 30;
    }
}