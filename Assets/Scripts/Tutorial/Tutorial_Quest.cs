using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Quest : MonoBehaviour
{
    public int Section = 1; // 1�� ù���� ��, 2�� �ι�° ��, �� �̿ܿ� ����

    public GameObject dialogwindow; // ��ȭâ

    bool quest1 = false;
    bool quest2 = false;

    void Start()
    {
        Section = 1;
    }

    void Update()
    {
        if(Section == 1)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                quest1 = true;
            }
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                quest2 = true;
            }
            if (quest1 && quest2 && dialogwindow.activeSelf == false)
            {
                quest1 = false;
                quest2 = false;

                TutorialManager.Instance.OpenDialogWindow();

                Section = 2;
            }
        }
        else if(Section == 2)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                quest1 = true;
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                quest2 = true;
            }
            if (quest1 && quest2 && dialogwindow.activeSelf == false)
            {
                TutorialManager.Instance.OpenDialogWindow();
                Section = 2;
            }
        }
        else
        {

        }
    }
}
