using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDialog : MonoBehaviour
{
    public GameObject Dialog_Window;

    void Start()
    {
        Invoke("DelayWindow", 3.0f);
    }

    void Update()
    {
        
    }

    void DelayWindow()
    {
        Dialog_Window.SetActive(true);
    }
}