using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static EventManager;

public class UICanvas : MonoBehaviour
{
    [SerializeField] private LevelTransiton levelTransiton;
    [SerializeField] private Button restartButton;
    [SerializeField] private SettingPanel settingPanel;
    [SerializeField] private CharacterPanel characterPanel;
    [SerializeField] private UpgradePanel upgradePanel;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button characterButton;
    [SerializeField] private Button upgradeButton;
    private Coroutine transitionCoroutine;

    private void Awake()
    {
        restartButton.gameObject.SetActive(false);
        levelTransiton.gameObject.SetActive(false);
        settingButton.gameObject.SetActive(true);
        characterButton.gameObject.SetActive(true);
        upgradeButton.gameObject.SetActive(true);
    }

    public SettingPanel SettingPanel
    {
        get { return settingPanel; }
    }

    public UpgradePanel UpgradePanel
    {
        get { return upgradePanel; }
    }

    public CharacterPanel CharacterPanel
    {
        get { return characterPanel; }
    }

    private void OnEnable()
    {
        EventManager.onGameOver += OnGameOver;
    }
    private void OnDisable()
    {
        EventManager.onGameOver -= OnGameOver;
        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
    }
    private void OnGameOver()
    {
        restartButton.gameObject.SetActive(true);
    }

    public void TransitionLevel()
    {
        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
        transitionCoroutine = StartCoroutine(TransitionCoroutine());
    }

    private IEnumerator TransitionCoroutine()
    {
        levelTransiton.gameObject.SetActive(true);
        yield return new WaitForSeconds(GameConfig.closeOverlay);

        restartButton.gameObject.SetActive(false);
        levelTransiton.TransitionState(true);
        DOTween.KillAll();
        var loadScene = SceneManager.LoadSceneAsync("Main");

        yield return new WaitForSeconds(GameConfig.openOverlay);

        levelTransiton.TransitionState(false);
        levelTransiton.gameObject.SetActive(false);
    }

    public void OnSettingClick()
    {
        settingPanel.ShowSettingPanel();
    }

    public void OnCharacterClick()
    {
        characterPanel.ShowCharacterPanel();
    }

    public void OnUpgradeClick()
    {
        upgradePanel.ShowUpgradePanel();
    }
}
