using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogWindow : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    public Player_Controller player_con;

    // 문자열
    public string [] sta;
    public string next_text;
    private int DialogCount;

    public bool CurrentDialogEnd = false; // 현재 대화 끗
    bool NextScene = false; // 다음씬으로 가는가
    public int NextSceneNumber = 1;

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
            StartCoroutine(Typing(next_text));
            NextScene = true;
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
            SceneManager.LoadScene(NextSceneNumber);
        }

        if(sta.Length > ++DialogCount) // 다음 번쨰 배열이 배열길이보다 길떄
        {
            dialogText.text = "";
            StartCoroutine(Typing(sta[DialogCount]));
        }
        else
        {
            player_con.enabled = true; // 플레이어 활성화

            CurrentDialogEnd = true;
            this.gameObject.SetActive(false); // 창 닫기
        }
    }
}