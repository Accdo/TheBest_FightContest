using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : MonoBehaviour
{
    public GameObject window;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Continue()
    {
        this.gameObject.SetActive(false);
    }
    public void Setting()
    {
        // 설정
    }
    public void Exit()
    {
        Application.Quit();
    }
}
