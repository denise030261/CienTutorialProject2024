using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } = null;

    public AudioSource bgmSource;
    public AudioSource[] sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        bgmSource.volume = PlayerPrefs.GetFloat("BGM", 0.5f);

        for (int i = 0; i < sfxSource.Length; ++i)
        {
            if (sfxSource[i].isPlaying == false)
            {
                sfxSource[i].volume = PlayerPrefs.GetFloat("SFX", 0.5f);
            }
        }
    }

    public void PlayBGM(string name)
    {
        AudioClip bgmClip = Resources.Load<AudioClip>("Music/BGM/" + name);
        if (bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.volume = PlayerPrefs.GetFloat("BGM", 0.5f);
            bgmSource.Play();
        }
        else
        {
            Debug.LogError("BGM�� ������ �ʽ��ϴ�");
        }
    }


    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(string name)
    {
        AudioClip sfxClip = Resources.Load<AudioClip>("Music/SFX/" + name);

        if (sfxClip != null)
        {
            sfxSource[0].clip = sfxClip;
            sfxSource[0].volume = PlayerPrefs.GetFloat("SFX", 0.6f);
            sfxSource[0].spatialBlend = 0;
            sfxSource[0].Play();
        }
        else
        {
            Debug.LogError("SFX�� �����ϴ�");
        }
    }

    public void StopSFX()
    {
        for (int i = 0; i < sfxSource.Length; ++i)
        {
            if (sfxSource[i].isPlaying)
            {
                sfxSource[i].Stop();
            }
        }
    }
}
