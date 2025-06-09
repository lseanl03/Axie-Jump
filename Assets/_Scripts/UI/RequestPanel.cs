using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EventManager;

public class RequestPanel : PanelBase
{
    [SerializeField] private TextMeshProUGUI requestText;
    [SerializeField] private Button agreeText;
    [SerializeField] private Button rejectText;
    [SerializeField] private TextMeshProUGUI notificationText;

    protected override void Awake()
    {
        base.Awake();
        agreeText.onClick.AddListener(OnAgreeClick);
        rejectText.onClick.AddListener(OnRejectClick);
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
        HidePanel();
        GameManager.Instance.RejectRequest();
        AudioManager.Instance.PlaySFX(AudioType.ArrowClick);
    }

    public void OnAgreeClick()
    {
        if (GameManager.Instance.AgreeRequet())
        {
            HidePanel();
            GameManager.Instance.AgreeRequet();
        }
    }

    private void OnOpenRequest(Request request)
    {
        ShowPanel();

        var r = request;
        SetRequestText(r.requestText);
    }
    public override void ShowPanel()
    {
        base.ShowPanel();
        GameManager.Instance.PauseGame();
    }
    public override void HidePanel()
    {
        base.HidePanel();
        GameManager.Instance.ContinueGame();
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
