using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;


public class AudioManager : Singleton<AudioManager>
{
    public bool isMutingBGM = false;
    public bool isMutingSFX = false;

    [Range(0f, 1f)]
    public float BGMVolume = 1f;
    [Range(0f, 1f)]
    public float SFXVolume = 1f;

    public Sound[] BGMSoundList;
    public Sound[] SFXSoundList;

    protected override void Awake()
    {
        base.Awake();
        foreach (Sound bgmSound in BGMSoundList)
        {
            bgmSound.audioSource = gameObject.AddComponent<AudioSource>();
            bgmSound.audioSource.clip = bgmSound.audioClip;
            bgmSound.audioSource.volume = bgmSound.volume * BGMVolume;
            bgmSound.audioSource.loop = bgmSound.loop;
            bgmSound.audioSource.mute = bgmSound.mute;
        }
        foreach (Sound sfxSound in SFXSoundList)
        {
            sfxSound.audioSource = gameObject.AddComponent<AudioSource>();
            sfxSound.audioSource.clip = sfxSound.audioClip;
            sfxSound.audioSource.volume = sfxSound.volume * SFXVolume;
            sfxSound.audioSource.loop = sfxSound.loop;
            sfxSound.audioSource.mute = sfxSound.mute;
        }
    }

    public void PlayBGM(AudioType type)
    {
        Sound bgm = Array.Find(BGMSoundList, s => s.audioType == type);
        if (bgm != null && !bgm.audioSource.isPlaying) bgm.audioSource.Play();

        foreach (Sound bgmSound in BGMSoundList)
        {
            if (bgmSound.audioSource.isPlaying && bgmSound.audioType != type)
                bgmSound.audioSource.Stop();
        }
    }
    public void PlaySFX(AudioType type, bool loop = false)
    {
        Sound sfx = Array.Find(SFXSoundList, s => s.audioType == type);
        if (sfx != null)
        {
            if (!loop) sfx.audioSource.PlayOneShot(sfx.audioClip);
            else sfx.audioSource.Play();
        }
    }
    public void StopSFX(AudioType type)
    {
        foreach (Sound bgmSound in SFXSoundList)
        {
            if (bgmSound.audioSource.isPlaying && bgmSound.audioType == type)
                bgmSound.audioSource.Stop();
        }
    }
    public void ToggleBGMState(bool state)
    {
        foreach (Sound bgmSound in BGMSoundList)
        {
            bgmSound.mute = !state;
            bgmSound.audioSource.mute = bgmSound.mute;
        }
        if (state) isMutingBGM = false;
        else isMutingBGM = true;
    }
    public void ToggleSFXState(bool state)
    {
        foreach (Sound sfxSound in SFXSoundList)
        {
            sfxSound.mute = !state;
            sfxSound.audioSource.mute = sfxSound.mute;
        }
        if (state) isMutingSFX = false;
        else isMutingSFX = true;
    }
    public void SetMusicVolume(float volume)
    {
        foreach (Sound sound in BGMSoundList)
        {
            sound.audioSource.volume = volume * sound.volume;
        }

    }
    public void SetSFXVolume(float volume)
    {
        foreach (Sound sound in SFXSoundList)
        {
            sound.audioSource.volume = volume * sound.volume;
        }
    }
}

[System.Serializable]
public class Sound
{
    public AudioType audioType;
    public AudioClip audioClip;
    public AudioSource audioSource;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0f, 1f)]
    public float pitch = 1f;
    public bool loop;
    public bool mute;
}
