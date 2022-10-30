using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cut_Load : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Sprite []sprite;
    int sprite_Num;

    public int NextSceneNumber = 1;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        sprite_Num = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ++sprite_Num;

            if(sprite_Num > 2)
                SceneManager.LoadScene(NextSceneNumber);
            else
                spriteRenderer.sprite = sprite[sprite_Num];
        }
    }
}