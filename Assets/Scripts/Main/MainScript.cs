using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
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
    }
}
