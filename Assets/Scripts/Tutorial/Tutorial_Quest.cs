using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Quest : MonoBehaviour
{
    public int Section = 1; // 1은 첫번쨰 퀘, 2는 두번째 퀘, 이 이외엔 종료

    public GameObject dialogwindow; // 대화창

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
