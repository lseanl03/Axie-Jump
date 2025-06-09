using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : PanelBase
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button closeButton;

    protected override void Awake()
    {
        base.Awake();
        closeButton.onClick.AddListener(HidePanel);
    }

    public void OnMusicVolumeChange()
    {
        AudioManager.Instance.SetMusicVolume(musicSlider.value);
    }

    public void OnSFXVolumeChange() 
    {
        AudioManager.Instance.SetSFXVolume(sfxSlider.value);
    }
}
