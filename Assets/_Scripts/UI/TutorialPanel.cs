using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private int currentIndex = 0;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private CanvasGroup bgCanvasGroup;
    [SerializeField] private GameObject tutorialMenu;
    [SerializeField] private GameObject tutorialListObj;
    private List<GameObject> tutorialList = new List<GameObject>();

    private void Awake()
    {
        bgCanvasGroup.gameObject.SetActive(false);
        tutorialMenu.SetActive(false);

        GetTutorialListInit();
    }
    public void ShowTutorialPanel()
    {
        tutorialMenu.SetActive(true);
        bgCanvasGroup.gameObject.SetActive(true);
        bgCanvasGroup.alpha = 0;
        bgCanvasGroup.DOFade(1, 0.5f).SetUpdate(true);

        TutorialViewChange();

        AudioManager.Instance.PlayButtonClick();

    }
    public void HideTutorialPanel()
    {
        tutorialMenu.SetActive(false);
        bgCanvasGroup.DOFade(0, 0.5f).SetUpdate(true);
        bgCanvasGroup.gameObject.SetActive(false);

        AudioManager.Instance.PlayCloseClick();

    }

    public void OnBeforeArrowClick()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = tutorialList.Count - 1;
        TutorialViewChange();
        AudioManager.Instance.PlayArrowClick();


    }
    public void OnAfterArrowClick()
    {
        currentIndex++;
        if (currentIndex >= tutorialList.Count) currentIndex = 0;
        TutorialViewChange();

        AudioManager.Instance.PlayArrowClick();

    }

    void GetTutorialListInit()
    {
        for (int i = 0; i < tutorialListObj.transform.childCount; i++)
        {
            var obj = tutorialListObj.transform.GetChild(i);
            tutorialList.Add(obj.gameObject);
        }
    }

    public void TutorialViewChange()
    {
        for (int i = 0; i < tutorialList.Count; i++)
        {
            var obj = tutorialList[i].gameObject;
            obj.SetActive(i == currentIndex ? true : false);
        }
    }
}
