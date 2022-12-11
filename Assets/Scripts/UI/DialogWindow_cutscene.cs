using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogWindow_cutscene : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    public string[] sta;
    private int DialogCount;

    bool NextScene = false; // 
    public int NextSceneNumber = 1; // 

    void Start()
    {
        dialogText.text = "";
        DialogCount = 0;
        StartCoroutine(Typing(sta[DialogCount]));
    }

    void Update()
    {
        
    }

    IEnumerator Typing(string text)
    {
        foreach (char letter in text.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void CloseWindow()
    {
        if (NextScene)
        {
            StartCoroutine(FadeInFadeOut.Instance.FadeOutStart(NextSceneNumber));
        }
        else
        {
            if (sta.Length > ++DialogCount) // 
            {
                dialogText.text = "";
                StartCoroutine(Typing(sta[DialogCount]));
            }
            else
            {
                NextScene = true;
            }
        }
    }
}