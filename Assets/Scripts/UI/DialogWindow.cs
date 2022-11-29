using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogWindow : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    public Player_Controller player_con;

    // ���ڿ�
    public string [] sta;
    public string next_text;
    private int DialogCount;

    private string tutorial_zero_text = "Shift가 대쉬";
    public bool Dialogplus = false; // 
    public bool CurrentDialogEnd = false; // 
    bool NextScene = false; // 
    public int NextSceneNumber = 1; // 


    void Start()
    {
        player_con.enabled = false;

        dialogText.text = "";
        DialogCount = 0;
        StartCoroutine(Typing(sta[DialogCount]));
    }

    void Update()
    {
        if(CurrentDialogEnd)
        {
            dialogText.text = "";

            if(Dialogplus)
            {
                StartCoroutine(Typing(tutorial_zero_text));
            }
            else
            {
                StartCoroutine(Typing(next_text));
                NextScene = true;
            }

            Dialogplus = false;
            CurrentDialogEnd = false;
        }
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
        if(NextScene)
        {
            StartCoroutine(FadeInFadeOut.Instance.FadeOutStart(NextSceneNumber));
        }
        else
        {
            if(sta.Length > ++DialogCount) // 
            {
                dialogText.text = "";
                StartCoroutine(Typing(sta[DialogCount]));
            }
            else
            {
                player_con.enabled = true; // 
    
                CurrentDialogEnd = true;
                this.gameObject.SetActive(false); // 
            }
        }

        
    }
}