using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : PanelBase
{
    private float pointsCount;
    private float primogemCount;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI primogemText;

    private Coroutine onGameOverCoroutine;

    protected override void Awake()
    {
        base.Awake();
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        restartButton.onClick.AddListener(OnRestartClick);
        mainMenuButton.onClick.AddListener(OnMainMenuClick);
    }

    private void OnEnable()
    {
        EventManager.onGameOver += OnGameOver;
        EventManager.onSceneChanged += OnSceneChanged;
    }
    private void OnDisable()
    {
        EventManager.onGameOver -= OnGameOver;
        EventManager.onSceneChanged -= OnSceneChanged;
    }
    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetPrimogem(int primogem)
    {
        primogemText.text = primogem.ToString();
    }

    public void OnRestartClick()
    {
        LoadingManager.Instance.TransitionLevel(SceneType.Game);
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
    }
    public void OnMainMenuClick()
    {
        LoadingManager.Instance.TransitionLevel(SceneType.MainMenu);
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
    }
    public void OnGameOver()
    {
        if (onGameOverCoroutine != null) StopCoroutine(onGameOverCoroutine);
        onGameOverCoroutine = StartCoroutine(OnShowGameOverPanel());
    }

    private IEnumerator OnShowGameOverPanel()
    {
        pointsCount = 0;
        primogemCount = 0;
        scoreText.text = ((int)pointsCount).ToString();
        primogemText.text = ((int)primogemCount).ToString();

        yield return new WaitForSeconds(1f);
        ShowPanel();
        yield return new WaitForSeconds(0.5f);

        DOTween.To(() => pointsCount, value =>
        {
            pointsCount = value;
            scoreText.text = ((int)pointsCount).ToString();
            scoreText.rectTransform.localScale = Vector2.one * 1.2f;
        }, GameManager.Instance.Points, 1f).SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            scoreText.rectTransform.localScale = Vector2.one;
            PrimogemTween();
        });
    }

    private void PrimogemTween()
    {
        DOTween.To(() => primogemCount, value =>
        {
            primogemCount = value;
            primogemText.text = ((int)primogemCount).ToString();
            primogemText.rectTransform.localScale = Vector2.one * 1.2f;
        }, GameManager.Instance.Primogems, 1f).SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            primogemText.rectTransform.localScale = Vector2.one;
            restartButton.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
        });
    }

    private void OnSceneChanged(SceneType sceneType)
    {
        HidePanel();
    }
}
