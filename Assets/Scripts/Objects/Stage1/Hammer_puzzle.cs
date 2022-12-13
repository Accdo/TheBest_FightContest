using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hammer_puzzle : MonoBehaviour
{
    public GameObject Hammer;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("PlayerAttack"))
        {
            StartCoroutine(HammerTime());
            SoundManager.Instance.PlaySFXSound("RopeAtk", 1.0f);
        }
    }

    IEnumerator HammerTime()
    {
        for (float f = 120f; f > 0; f -= 0.05f)
        {
            Hammer.transform.DORotate(new Vector3(0,0,-40), 0.4f);
            
            yield return null;
        }
    }
}
