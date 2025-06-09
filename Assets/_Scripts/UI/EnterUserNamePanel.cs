using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterUserNamePanel : PanelBase
{
    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_InputField userNameInput;

    protected override void Awake()
    {
        base.Awake();
        submitButton.onClick.AddListener(OnSubmitClick);
    }
    public void OnSubmitClick()
    {
        if (!string.IsNullOrEmpty(userNameInput.text))
        {
            PlayFabManager.Instance.SubmitUserNameName(userNameInput.text);
            UIManager.Instance.UICanvas.TutorialPanel.ShowPanel();
            HidePanel();
        }
    }
}
