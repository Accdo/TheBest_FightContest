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

    private string tutorial_zero_text = "������ �뽬�� ������ �غ��Ŷ�. �뽬�� (Shift)��ư, ������ (Space)��ư�� �Է��ϸ� ����� �� �ִ�.";
    public bool Dialogplus = false; // ��ȭ �� �ϴ���
    public bool CurrentDialogEnd = false; // ���� ��ȭ ��
    bool NextScene = false; // ���������� ���°�
    public int NextSceneNumber = 1; // ���� �� ��ȣ


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
            if(sta.Length > ++DialogCount) // ���� ���� �迭�� �迭���̺��� �拚
            {
                dialogText.text = "";
                StartCoroutine(Typing(sta[DialogCount]));
            }
            else
            {
                player_con.enabled = true; // �÷��̾� Ȱ��ȭ
    
                CurrentDialogEnd = true;
                this.gameObject.SetActive(false); // â �ݱ�
            }
        }

        
    }
}