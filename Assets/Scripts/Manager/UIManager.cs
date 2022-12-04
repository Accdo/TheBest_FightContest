using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    
    [SerializeField]
    private GameObject[] UI_Window_Array;
    Dictionary<string, GameObject> UI_Window_Dic = new Dictionary<string, GameObject>();

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    private void Awake() 
    {
        // if (Instance != this)
        // {
        //     Destroy(this.gameObject);
        // }
        // DontDestroyOnLoad(this.gameObject);

        foreach (GameObject obj in UI_Window_Array)
        {
            UI_Window_Dic.Add(obj.name, obj);
        }
    }

    public void Window_On(string name)
    {
        if (UI_Window_Dic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained UI_Window_Dic");
            return;
        }
        UI_Window_Dic[name].SetActive(true);
    }

    public void Window_Off(string name)
    {
        if (UI_Window_Dic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained UI_Window_Dic");
            return;
        }
        UI_Window_Dic[name].SetActive(false);
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
