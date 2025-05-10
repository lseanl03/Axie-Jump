using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EventManager;

public class RequestPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI requestText;
    [SerializeField] private Button agreeText;
    [SerializeField] private Button rejectText;
    [SerializeField] private CanvasGroup bgCanvasGroup;
    [SerializeField] private GameObject requestMenu;
    [SerializeField] private TextMeshProUGUI notificationText;

    private void Awake()
    {
        bgCanvasGroup.gameObject.SetActive(false);
        requestMenu.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.onOpenRequest += OnOpenRequest;
    }

    private void OnDisable()
    {
        EventManager.onOpenRequest -= OnOpenRequest;

    }

    public void OnRejectClick()
    {
        HideRequetPanel();
        GameManager.Instance.RejectRequest();
        AudioManager.Instance.PlayArrowClick();
    }

    public void OnAgreeClick()
    {
        if (GameManager.Instance.AgreeRequet())
        {
            HideRequetPanel();
            GameManager.Instance.AgreeRequet();
        }
    }

    private void OnOpenRequest(Request request)
    {
        ShowRequestPanel();

        var r = request;
        SetRequestText(r.requestText);
    }

    public void ShowRequestPanel()
    {
        bgCanvasGroup.gameObject.SetActive(true);
        bgCanvasGroup.alpha = 0;
        bgCanvasGroup.DOFade(1, 0.5f).SetUpdate(true);
        requestMenu.SetActive(true);

        GameManager.Instance.PauseGame();
        AudioManager.Instance.PlayButtonClick();

    }

    public void HideRequetPanel()
    {
        bgCanvasGroup.DOFade(0, 0.5f).SetUpdate(true);
        bgCanvasGroup.gameObject.SetActive(false);
        requestMenu.SetActive(false);

        GameManager.Instance.ContinueGame();
        AudioManager.Instance.PlayCloseClick();

    }

    private void SetRequestText(string text)
    {
        requestText.text = text;
    }

    public void ShowNotificationText(string text)
    {
        if (notificationText.gameObject.activeSelf) return;

        notificationText.gameObject.SetActive(true);
        notificationText.text = text;
        notificationText.alpha = 1f;
        notificationText.DOFade(0f, 2f).SetUpdate(true).OnComplete(() =>
        {
            notificationText.gameObject.SetActive(false);
        });
    }
}
