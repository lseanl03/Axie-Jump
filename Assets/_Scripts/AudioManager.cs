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

    public void PlayBGM(string soundName)
    {
        Sound bgm = Array.Find(BGMSoundList, s => s.name == soundName);
        if (bgm != null && !bgm.audioSource.isPlaying) bgm.audioSource.Play();

        foreach (Sound bgmSound in BGMSoundList)
        {
            if (bgmSound.audioSource.isPlaying && bgmSound.name != soundName)
                bgmSound.audioSource.Stop();
        }
    }
    public void PlaySFX(string soundName)
    {
        Sound sfx = Array.Find(SFXSoundList, s => s.name == soundName);
        if (sfx != null) sfx.audioSource.PlayOneShot(sfx.audioClip);
    }
    public void StopSFX(string soundName)
    {
        foreach (Sound bgmSound in BGMSoundList)
        {
            if (bgmSound.audioSource.isPlaying && bgmSound.name == soundName)
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
    public void PlayTransition()
    {
        PlaySFX("Transition");
    }
    public void PlayButtonClick()
    {
        PlaySFX("ButtonClick");
    }
    public void PlayArrowClick()
    {
        PlaySFX("ArrowClick");
    }
    public void PlaySelectClick()
    {
        PlaySFX("SelectClick");
    }
    public void PlayCollectItem()
    {
        PlaySFX("CollectItem");
    }
    public void PlayCollectPrimogem()
    {
        PlaySFX("CollectPrimogem");
    }
    public void PlayCollectBuff()
    {
        PlaySFX("CollectBuff");
    }
    public void PlayCloseClick()
    {
        PlaySFX("CloseClick");
    }
    public void PlayUpgradeClick()
    {
        PlaySFX("UpgradeClick");
    }
    public void PlayTime()
    {
        PlaySFX("Time");
    }
    public void PlayDie()
    {
        PlaySFX("Die");
    }
    public void PlayJump()
    {
        PlaySFX("Jump");
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;

    public AudioSource audioSource;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(0, 1f)]
    public float pitch = 1f;

    public bool loop;

    public bool mute;
}
