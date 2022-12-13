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
    } // Sound�� �������ִ� ��ũ��Ʈ�� �ϳ��� �����ؾ��ϰ� instance������Ƽ�� ���� ��𿡼��� �ҷ��������� �̱��� ���

    private AudioSource bgmPlayer;
    private AudioSource sfxPlayer;

    public float masterVolumeSFX = 1f;
    public float masterVolumeBGM = 1f;

    [SerializeField]
    private AudioClip TutorialBgmAudioClip; // Ʃ�丮�� �����
    [SerializeField]
    private AudioClip CutSceneBgmAudioClip; // ��Ʋ �����
    [SerializeField]
    private AudioClip Stage1_BgmAudioClip; // ��Ʋ �����
    [SerializeField]
    private AudioClip StageBoss1_BgmAudioClip; // ��Ʋ �����
    [SerializeField]
    private AudioClip Stage2_BgmAudioClip; // ��Ʋ �����
    [SerializeField]
    private AudioClip StageBoss2_BgmAudioClip; // ��Ʋ �����


    [SerializeField]
    private AudioClip[] sfxAudioClips; //ȿ������

    Dictionary<string, AudioClip> audioClipsDic = new Dictionary<string, AudioClip>(); //ȿ���� ��ųʸ�
    // AudioClip�� Key,Value ���·� �����ϱ� ���� ��ųʸ� ���

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject); //���� ������ ����� ��.

        //bgmPlayer = GameObject.Find("BGMSoundPlayer").GetComponent<AudioSource>();
        //sfxPlayer = GameObject.Find("SFXSoundPlayer").GetComponent<AudioSource>();
        
        bgmPlayer = GetComponentsInChildren<AudioSource>()[0];
        sfxPlayer = GetComponentsInChildren<AudioSource>()[1];

        foreach (AudioClip audioclip in sfxAudioClips)
        {
            audioClipsDic.Add(audioclip.name, audioclip);
        }
    }

    // ȿ�� ���� ��� : �̸��� �ʼ� �Ű�����, ������ ������ �Ű������� ����
    public void PlaySFXSound(string name, float volume = 1f)
    {
        if (audioClipsDic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained audioClipsDic");
            return;
        }
        sfxPlayer.PlayOneShot(audioClipsDic[name], volume * masterVolumeSFX); // �Ϲ� Play�� �ٸ��� ��ø ����
    }

    //BGM ���� ��� : ������ ������ �Ű������� ����
    public void PlayBGMSound(int bgm_num, float volume = 1f)
    {
        bgmPlayer.loop = true; //BGM �����̹Ƿ� ��������
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
