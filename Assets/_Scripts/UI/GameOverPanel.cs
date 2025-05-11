using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    private float pointsCount;
    private float primogemCount;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI primogemText;
    [SerializeField] private CanvasGroup bgCanvasGroup;
    [SerializeField] private GameObject gameOverMenu;

    private Coroutine onGameOverCoroutine;
    private void Awake()
    {
        bgCanvasGroup.gameObject.SetActive(false);
        gameOverMenu.SetActive(false);
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
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
    public void ShowGameOverPanel()
    {
        gameOverMenu.SetActive(true);
        bgCanvasGroup.gameObject.SetActive(true);
        bgCanvasGroup.alpha = 0;
        bgCanvasGroup.DOFade(1, 0.5f).SetUpdate(true);

    }
    public void HideGameOverPanel()
    {
        gameOverMenu.SetActive(false);
        bgCanvasGroup.DOFade(0, 0.5f).SetUpdate(true);
        bgCanvasGroup.gameObject.SetActive(false);

        AudioManager.Instance.PlayCloseClick();
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
        ShowGameOverPanel();
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
        HideGameOverPanel();
    }
}
