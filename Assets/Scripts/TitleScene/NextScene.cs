using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public int SceneNumber = 1;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SceneChange()
    {
        SceneManager.LoadScene(SceneNumber);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            SceneChange();
            Debug.Log("SceneChange");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            SceneChange();
            Debug.Log("SceneChange");
        }
    }
}