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
}