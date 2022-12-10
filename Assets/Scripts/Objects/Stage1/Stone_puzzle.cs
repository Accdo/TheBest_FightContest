using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone_puzzle : MonoBehaviour
{
    public GameObject [] Word;
    int word_count;

    public SpriteRenderer word_OutLine;

    public GameObject eff_heal;

    void Start()
    {
        word_count = 0;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("PlayerAttack"))
        {
            if(word_count < 4)
            {
                if(word_count == 3)
                {
                    Debug.Log("Step1");
                    StartCoroutine(AlphaTime());

                    Destroy(eff_heal);
                }

                Word[word_count].SetActive(true);
                ++word_count;
            }
            
        }
    }

    IEnumerator AlphaTime()
    {
        for (float f = 0f; f < 1; f += 0.02f)
        {
            Color c = word_OutLine.color;
            c.a = f;
            word_OutLine.color = c;
            
            yield return null;
        }
    }
}
