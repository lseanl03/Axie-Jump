using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup bgCanvasGroup;
    [SerializeField] private GameObject settingMenu;

    private void Awake()
    {
        bgCanvasGroup.gameObject.SetActive(false);
        settingMenu.SetActive(false);
    }
    public void ShowSettingPanel()
    {
        settingMenu.SetActive(true);
        bgCanvasGroup.gameObject.SetActive(true);
        bgCanvasGroup.alpha = 0;
        bgCanvasGroup.DOFade(1, 0.5f).SetUpdate(true);
    }
    public void HideSettingPanel()
    {
        settingMenu.SetActive(false);
        bgCanvasGroup.DOFade(0, 0.5f).SetUpdate(true);
        bgCanvasGroup.gameObject.SetActive(false);
    }
}
