using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterUserNamePanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup bgCanvasGroup;
    [SerializeField] private GameObject enterUserNameMenu;
    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_InputField userNameInput;
    private void Awake()
    {
    }
    public void ShowEnterUserNamePanel()
    {
        enterUserNameMenu.SetActive(true);
        bgCanvasGroup.gameObject.SetActive(true);
        bgCanvasGroup.alpha = 0;
        bgCanvasGroup.DOFade(1, 0.5f).SetUpdate(true);
    }
    public void HideEnterUserNamePanel()
    {
        enterUserNameMenu.SetActive(false);
        bgCanvasGroup.DOFade(0, 0.5f).SetUpdate(true);
        bgCanvasGroup.gameObject.SetActive(false);
    }

    public void OnSubmitClick()
    {
        if (!string.IsNullOrEmpty(userNameInput.text))
        {
            PlayFabManager.Instance.SubmitUserNameName(userNameInput.text);
            HideEnterUserNamePanel();

            UIManager.Instance.UICanvas.TutorialPanel.ShowTutorialPanel();
        }
    }
}
