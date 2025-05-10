using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private CanvasGroup bgCanvasGroup;
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private GameObject buffUpgradeHolder;

    [SerializeField] private Color currentProgressColor;
    [SerializeField] private Color unlockColor;

    private List<BuffUpgrade> buffUpgradeList = new List<BuffUpgrade>();
    private void Awake()
    {
        bgCanvasGroup.gameObject.SetActive(false);
        upgradeMenu.SetActive(false);
    }
    private void Start()
    {
        GetBuffUpgradeInit();
        GetBuffUpgradeDataInit();
    }

    public Color CurrentProgressColor
    {
        get { return currentProgressColor; }
    }
    public Color UnlockColor
    {
        get { return unlockColor; }
    }
    public void ShowUpgradePanel()
    {
        upgradeMenu.SetActive(true);
        bgCanvasGroup.gameObject.SetActive(true);
        bgCanvasGroup.alpha = 0;
        bgCanvasGroup.DOFade(1, 0.5f).SetUpdate(true);

        AudioManager.Instance.PlayButtonClick();
    }
    public void HideUpgradePanel()
    {
        upgradeMenu.SetActive(false);
        bgCanvasGroup.DOFade(0, 0.5f).SetUpdate(true);
        bgCanvasGroup.gameObject.SetActive(false);

        AudioManager.Instance.PlayCloseClick();

    }

    private void GetBuffUpgradeInit()
    {
        for (int i = 0; i < buffUpgradeHolder.transform.childCount; i++)
        {
            var buffUpgrade = buffUpgradeHolder.transform
                .GetChild(i).GetComponent<BuffUpgrade>();
            if (buffUpgrade) buffUpgradeList.Add(buffUpgrade);
        }
    }

    private void GetBuffUpgradeDataInit()
    {
        var buffUpgradeData = BuffManager.Instance.BuffUpgradeData;
        for (int i = 0; i < buffUpgradeList.Count; i++)
        {
            var data = buffUpgradeData.buffUpgradeConfigs[i];
            buffUpgradeList[i].BuffUpgradeConfig = data;
            buffUpgradeList[i].SetBuffImage(data.buffImage);
            buffUpgradeList[i].SetBuffName(data.buffName);
        }
    }
}
