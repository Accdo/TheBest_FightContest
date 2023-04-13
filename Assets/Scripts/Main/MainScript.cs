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
        if (SceneManager.GetActiveScene().name == "Tutorial_Scene_0")
        {
            SoundManager.Instance.PlayBGMSound(1, 0.2f);
        }
        if(SceneManager.GetActiveScene().name == "Tutorial_CutScene")
        {
            SoundManager.Instance.PlayBGMSound(2, 0.2f);
        }
        if (SceneManager.GetActiveScene().name == "Stage0")
        {
            SoundManager.Instance.PlayBGMSound(3, 0.2f);
        }
        if (SceneManager.GetActiveScene().name == "Stage1_BossBattle")
        {
            SoundManager.Instance.PlayBGMSound(4, 0.2f);
        }
        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            SoundManager.Instance.PlayBGMSound(5, 0.2f);
        }
        if (SceneManager.GetActiveScene().name == "Tutorial_Scene_3")
        {
            SoundManager.Instance.PlayBGMSound(6, 0.2f);
        }


        StartCoroutine(FadeInFadeOut.Instance.FadeInStart());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Stage1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Stage2");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.Window_On("Pause_Window_panel");
        }
    }
}
