using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : PanelBase
{
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject buffUpgradeHolder;

    [SerializeField] private Color currentProgressColor;
    [SerializeField] private Color unlockColor;

    private List<BuffUpgrade> buffUpgradeList = new List<BuffUpgrade>();

    protected override void Awake()
    {
        base.Awake();
        closeButton.onClick.AddListener(HidePanel);
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
