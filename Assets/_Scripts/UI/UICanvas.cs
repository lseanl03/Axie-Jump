using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static EventManager;

public class UICanvas : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button characterButton;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button leaderBoardButton;
    [SerializeField] private Button leftClickButton;
    [SerializeField] private Button rightClickButton;
    [SerializeField] private LevelTransiton levelTransiton;
    [SerializeField] private SettingPanel settingPanel;
    [SerializeField] private CharacterPanel characterPanel;
    [SerializeField] private UpgradePanel upgradePanel;
    [SerializeField] private MainMenuPanel mainMenuPanel;
    [SerializeField] private GamePanel gamePanel;
    [SerializeField] private RequestPanel requestPanel;
    [SerializeField] private PausePanel pausePanel;
    [SerializeField] private GameOverPanel gameOverPanel;
    [SerializeField] private LeaderboardPanel leaderboardPanel;
    [SerializeField] private EnterUserNamePanel enterUserNamePanel;

    private void Awake()
    {
        restartButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        settingButton.gameObject.SetActive(true);
        characterButton.gameObject.SetActive(true);
        upgradeButton.gameObject.SetActive(true);
        leaderBoardButton.gameObject.SetActive(true);
        levelTransiton.gameObject.SetActive(true);
    }

    public LeaderboardPanel LeaderboardPanel
    {
        get { return leaderboardPanel; }
    }
    public EnterUserNamePanel EnterUserNamePanel
    {
        get { return enterUserNamePanel; }
    }
    public GameOverPanel GameOverPanel
    {
        get { return gameOverPanel; }
    }
    public MainMenuPanel MainMenuPanel
    {
        get { return mainMenuPanel; }
    }
    public GamePanel GamePanel
    {
        get { return gamePanel; }
    }
    public RequestPanel RequestPanel
    {
        get { return requestPanel; }
    }
    public PausePanel PausePanel
    {
        get { return pausePanel; }
    }
    public Button RestartButton
    {
        get { return restartButton; }
    }
    public LevelTransiton LevelTransiton
    {
        get { return levelTransiton; }
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
        EventManager.onSceneChanged += OnSceneChanged;
        EventManager.onGameOver += OnGameOver;
    }
    private void OnDisable()
    {
        EventManager.onSceneChanged -= OnSceneChanged;
        EventManager.onGameOver -= OnGameOver;
    }
    private void OnGameOver()
    {
        restartButton.gameObject.SetActive(true);
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

    public void OnLeaderboardClick()
    {
        leaderboardPanel.ShowLeaderboardPanel();
    }
    public void OnLeftClick()
    {
        if (!GameManager.Instance.GameStarted)
        {
            EventManager.OnGameStartAction();
        }
        else
        {
            EventManager.ClickJumpAction(true);
        }
    }
    public void OnRightClick()
    {
        if (!GameManager.Instance.GameStarted)
        {
            EventManager.OnGameStartAction();
        }
        else
        {
            EventManager.ClickJumpAction(false);
        }
    }

    private void OnSceneChanged(SceneType sceneType)
    {
        settingButton.gameObject.SetActive(sceneType == SceneType.MainMenu);
        characterButton.gameObject.SetActive(sceneType == SceneType.MainMenu);
        upgradeButton.gameObject.SetActive(sceneType == SceneType.MainMenu);
        pauseButton.gameObject.SetActive(sceneType == SceneType.Game);
        leaderBoardButton.gameObject.SetActive(sceneType == SceneType.MainMenu);
        leftClickButton.gameObject.SetActive(sceneType == SceneType.Game);
        rightClickButton.gameObject.SetActive(sceneType == SceneType.Game);
    }
}
