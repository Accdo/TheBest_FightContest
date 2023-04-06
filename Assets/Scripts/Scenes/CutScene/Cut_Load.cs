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

    bool LastCut = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        sprite_Num = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetMouseButtonDown(0) && !LastCut)
        {
            audioSource.Play();

            ++sprite_Num;
            
            if (sprite_Num > sprite.Length-1)
            {
                StartCoroutine(FadeInFadeOut.Instance.FadeOutStart(NextSceneNumber));
                LastCut = true;
            }
            else
                spriteRenderer.sprite = sprite[sprite_Num];
        }
    }
}
