using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cut_Load : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    AudioSource audioSource;

    public Sprite []sprite;
    int sprite_Num;

    public int NextSceneNumber = 1;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        sprite_Num = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            audioSource.Play();

            ++sprite_Num;

            if(sprite_Num > 2)
                StartCoroutine(FadeInFadeOut.Instance.FadeOutStart(NextSceneNumber));
            else
                spriteRenderer.sprite = sprite[sprite_Num];
        }
    }
}
