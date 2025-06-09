using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class TutorialPanel : PanelBase
{
    [SerializeField] private int currentIndex = 0;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject tutorialListObj;
    private List<GameObject> tutorialList = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        GetTutorialListInit();
        leftButton.onClick.AddListener(OnBeforeArrowClick);
        rightButton.onClick.AddListener(OnAfterArrowClick);
        closeButton.onClick.AddListener(HidePanel);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        TutorialViewChange();
    }
    public void OnBeforeArrowClick()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = tutorialList.Count - 1;
        TutorialViewChange();
        AudioManager.Instance.PlaySFX(AudioType.ArrowClick);


    }
    public void OnAfterArrowClick()
    {
        currentIndex++;
        if (currentIndex >= tutorialList.Count) currentIndex = 0;
        TutorialViewChange();

        AudioManager.Instance.PlaySFX(AudioType.ArrowClick);
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
