using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI primogemText;
    [SerializeField] private CanvasGroup bgCanvasGroup;
    [SerializeField] private GameObject gameOverMenu;
    private void Awake()
    {
        bgCanvasGroup.gameObject.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.onGameOver += ShowGameOverPanel;
    }
    private void OnDisable()
    {
        EventManager.onGameOver -= ShowGameOverPanel;
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
    }
    public void OnMainMenuClick()
    {
        LoadingManager.Instance.TransitionLevel(SceneType.MainMenu);
    }
}
