using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope_puzzle : MonoBehaviour
{
    public int rope_truth; // 1 : true rope 2 : false rope

    public GameObject eff_heal;

    public GameObject tree_trap;

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
            if(rope_truth == 1)
            {
                StartCoroutine(TrapTime());
                Destroy(eff_heal);
            }

            if(rope_truth == 2)
            {
                StartCoroutine(TrapTime());
            }
        }
    }

    IEnumerator TrapTime()
    {
        for (float f = 6f; f > -0.5; f -= 0.05f)
        {
            tree_trap.transform.localPosition = new Vector3(-13.0f, f, 0.0f);
            
            yield return null;
        }
    }
}
