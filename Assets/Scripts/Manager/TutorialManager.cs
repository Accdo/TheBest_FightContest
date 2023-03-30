using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private static TutorialManager instance;
    
    public static TutorialManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TutorialManager>();
            }

            return instance;
        }
    }

    public GameObject dialogwindow;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        //DontDestroyOnLoad(this.gameObject);
    }

    public void OpenDialogWindow()
    {
        dialogwindow.SetActive(true);
    }
}
