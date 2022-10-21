using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow_Copy : MonoBehaviour
{
    private SpriteRenderer my_animator;

    public SpriteRenderer main_animator;

    void Start()
    {
        my_animator = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        my_animator.sprite = main_animator.sprite;
    }
}
