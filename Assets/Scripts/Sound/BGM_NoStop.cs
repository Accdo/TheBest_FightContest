using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM_NoStop : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;

    bool Onoff;

    void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Onoff = true;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4 && Onoff)
        {
            audioSource.clip = audioClip;
            audioSource.Play();

            Onoff = false;
        }
    }
}
