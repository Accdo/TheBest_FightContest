using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }

            return instance;
        }
    } // Sound를 관리해주는 스크립트는 하나만 존재해야하고 instance프로퍼티로 언제 어디에서나 불러오기위해 싱글톤 사용

    private AudioSource bgmPlayer;
    private AudioSource sfxPlayer;

    public float masterVolumeSFX = 1f;
    public float masterVolumeBGM = 1f;

    [SerializeField]
    private AudioClip TutorialBgmAudioClip; // 튜토리얼 배경음
    [SerializeField]
    private AudioClip CutSceneBgmAudioClip; // 배틀 배경음
    [SerializeField]
    private AudioClip Stage1_BgmAudioClip; // 배틀 배경음
    [SerializeField]
    private AudioClip StageBoss1_BgmAudioClip; // 배틀 배경음
    [SerializeField]
    private AudioClip Stage2_BgmAudioClip; // 배틀 배경음
    [SerializeField]
    private AudioClip StageBoss2_BgmAudioClip; // 배틀 배경음


    [SerializeField]
    private AudioClip[] sfxAudioClips; //효과음들

    Dictionary<string, AudioClip> audioClipsDic = new Dictionary<string, AudioClip>(); //효과음 딕셔너리
    // AudioClip을 Key,Value 형태로 관리하기 위해 딕셔너리 사용

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject); //여러 씬에서 사용할 것.

        //bgmPlayer = GameObject.Find("BGMSoundPlayer").GetComponent<AudioSource>();
        //sfxPlayer = GameObject.Find("SFXSoundPlayer").GetComponent<AudioSource>();
        
        bgmPlayer = GetComponentsInChildren<AudioSource>()[0];
        sfxPlayer = GetComponentsInChildren<AudioSource>()[1];

        foreach (AudioClip audioclip in sfxAudioClips)
        {
            audioClipsDic.Add(audioclip.name, audioclip);
        }
    }

    // 효과 사운드 재생 : 이름을 필수 매개변수, 볼륨을 선택적 매개변수로 지정
    public void PlaySFXSound(string name, float volume = 1f)
    {
        if (audioClipsDic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained audioClipsDic");
            return;
        }
        sfxPlayer.PlayOneShot(audioClipsDic[name], volume * masterVolumeSFX); // 일반 Play와 다르게 중첩 가능
    }

    //BGM 사운드 재생 : 볼륨을 선택적 매개변수로 지정
    public void PlayBGMSound(int bgm_num, float volume = 1f)
    {
        bgmPlayer.loop = true; //BGM 사운드이므로 루프설정
        bgmPlayer.volume = volume * masterVolumeBGM;

        if (bgm_num == 1)
        {
            bgmPlayer.clip = TutorialBgmAudioClip;
            bgmPlayer.Play();
        }
        else if (bgm_num == 2)
        {
            bgmPlayer.clip = CutSceneBgmAudioClip;
            bgmPlayer.Play();
        }
        else if (bgm_num == 3)
        {
            bgmPlayer.clip = Stage1_BgmAudioClip;
            bgmPlayer.Play();
        }
        else if (bgm_num == 4)
        {
            bgmPlayer.clip = StageBoss1_BgmAudioClip;
            bgmPlayer.Play();
        }
        else if (bgm_num == 5)
        {
            bgmPlayer.clip = Stage2_BgmAudioClip;
            bgmPlayer.Play();
        }
        else if (bgm_num == 6)
        {
            bgmPlayer.clip = StageBoss2_BgmAudioClip;
            bgmPlayer.Play();
        }


    }


}
