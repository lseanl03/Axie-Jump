using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private CanvasGroup bgCanvasGroup;
    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        bgCanvasGroup.gameObject.SetActive(false);
        pauseMenu.SetActive(false);
    }
    public void ShowPausePanel()
    {
        pauseMenu.SetActive(true);
        bgCanvasGroup.gameObject.SetActive(true);
        bgCanvasGroup.alpha = 0;
        bgCanvasGroup.DOFade(1, 0.5f).SetUpdate(true);

        GameManager.Instance.PauseGame();
    }
    public void HidePausePanel()
    {
        pauseMenu.SetActive(false);
        bgCanvasGroup.DOFade(0, 0.5f).SetUpdate(true);
        bgCanvasGroup.gameObject.SetActive(false);

        GameManager.Instance.ContinueGame();
    }

    public void OnClickContinue()
    {
        HidePausePanel();
        GameManager.Instance.ContinueGame();
    }

    public void OnClickSound()
    {
    }

    public void OnClickMainMenu()
    {
        HidePausePanel();
        LoadingManager.Instance.TransitionLevel(SceneType.MainMenu);
    }
}
