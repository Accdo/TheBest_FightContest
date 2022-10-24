using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eff_BackCloud : MonoBehaviour
{
    public float Left_End = -30.0f;
    public float ChangeRight = 46.78f;
    public float speed;

    public bool FirstNSecond = true; // true : ù���� ���, false : �ι�° ���

    void Start()
    {
        
    }

    void Update()
    {
        if(transform.position.x > Left_End)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else{
            if(FirstNSecond)
                transform.position = new Vector2(ChangeRight, transform.position.y);
        }

    }
}
