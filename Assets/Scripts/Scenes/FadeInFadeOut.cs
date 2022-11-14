using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInFadeOut : MonoBehaviour
{
    private static FadeInFadeOut instance;
    
    public static FadeInFadeOut Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FadeInFadeOut>();
            }

            return instance;
        }
    }

    public GameObject FadePannel;

    private void Awake()
    {
        // if (Instance != this)
        // {
        //     Destroy(this.gameObject);
        // }
        // DontDestroyOnLoad(this.gameObject); //¿©·¯ ¾À¿¡¼­ »ç¿ëÇÒ °Í.
    }
    void Update()
    {
        
    }

    //ÆäÀÌµå ÀÎ, ¹à¾ÆÁü
    public IEnumerator FadeInStart()
    {
        FadePannel.SetActive(true);
        for (float f = 1f; f >= 0; f -= 0.001f)
        {
            Color c = FadePannel.GetComponent<Image>().color;
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        FadePannel.SetActive(false);
    }
    
    //ÆäÀÌµå ¾Æ¿ô, ¾îµÎ¿öÁü
    public IEnumerator FadeOutStart(int _sceneNum)
    {
        FadePannel.SetActive(true);
        for (float f = 0f; f <= 1; f += 0.001f)
        {
            Debug.Log(f);
            Color c = FadePannel.GetComponent<Image>().color;
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            
            yield return null;
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(_sceneNum);
    }

}
