using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : PanelBase
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button mainMenuButton;

    protected override void Awake()
    {
        base.Awake();
        continueButton.onClick.AddListener(OnClickContinue);
        mainMenuButton.onClick.AddListener(OnClickMainMenu);
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

    public void OnClickContinue()
    {
        HidePanel();
        GameManager.Instance.ContinueGame();
    }
    public void OnClickMainMenu()
    {
        HidePanel();
        LoadingManager.Instance.TransitionLevel(SceneType.MainMenu);
    }
}
