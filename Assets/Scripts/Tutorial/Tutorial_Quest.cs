using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Quest : MonoBehaviour
{
    public int Section = 1; // 1�� ù���� ��, 2�� �ι�° ��, �� �̿ܿ� ����

    public GameObject dialogwindow; // ��ȭâ

    void Start()
    {
        Section = 1;
    }

    void Update()
    {
        if(Section == 1)
        {
            if (Input.GetKeyDown(KeyCode.O) && dialogwindow.activeSelf == false)
            {
                dialogwindow.SetActive(true);

                Section = 2;
            }
        }
        else if(Section == 2)
        {
            if (Input.GetKeyDown(KeyCode.P) && dialogwindow.activeSelf == false)
            {
                dialogwindow.SetActive(true);
            }
        }
        else
        {

        }
    }
}
