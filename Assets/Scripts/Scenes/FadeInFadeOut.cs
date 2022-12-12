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
        
    }
    void Update()
    {
        
    }

    //���̵� ��, �����
    public IEnumerator FadeInStart()
    {
        FadePannel.SetActive(true);
        for (float f = 1f; f >= 0; f -= 0.01f)
        {
            Color c = FadePannel.GetComponent<Image>().color;
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        FadePannel.SetActive(false);
    }
    
    //���̵� �ƿ�, ��ο���
    public IEnumerator FadeOutStart(int _sceneNum)
    {
        FadePannel.SetActive(true);
        for (float f = 0f; f <= 1; f += 0.01f)
        {
            Color c = FadePannel.GetComponent<Image>().color;
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            
            yield return null;
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(_sceneNum);
    }

    public IEnumerator FadeOutStart(int _sceneNum, float _delayTime)
    {
        yield return new WaitForSeconds(_delayTime);

        FadePannel.SetActive(true);
        for (float f = 0f; f <= 1; f += 0.01f)
        {
            Color c = FadePannel.GetComponent<Image>().color;
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;

            yield return null;
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(_sceneNum);
    }

}
