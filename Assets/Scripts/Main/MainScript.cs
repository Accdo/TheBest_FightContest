using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    private void Awake() 
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {   
        if (SceneManager.GetActiveScene().name == "Tutorial_Scene_0" || SceneManager.GetActiveScene().name == "Tutorial_CutScene")
        {
            SoundManager.Instance.PlayBGMSound(0.2f);
        }

        StartCoroutine(FadeInFadeOut.Instance.FadeInStart());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene(4);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.Window_On("Pause_Window_panel");
        }

        
    }
}
