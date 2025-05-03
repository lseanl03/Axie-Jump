using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffUpgrade : MonoBehaviour
{
    private int progressIndex = 0;
    private int currentPrice = 0;
    [SerializeField] private BuffUpgradeConfig buffUpgradeConfig;
    [SerializeField] private Image buffImage;
    [SerializeField] private TextMeshProUGUI buffNameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private GameObject progressHolder;

    private List<Progress> progressList = new List<Progress>();
    private void Start()
    {
        GetProgressListInit();
        SetProgressInit();
        currentPrice = buffUpgradeConfig.startPrice;
    }

    public BuffUpgradeConfig BuffUpgradeConfig
    {
        get { return buffUpgradeConfig; }
        set { buffUpgradeConfig = value; }
    }

    public void SetBuffImage(Sprite sprite)
    {
        buffImage.sprite = sprite;
    }

    public void SetBuffName(string name)
    {
        buffNameText.text = name;
    }
    public void SetPriceText(int price)
    {
        priceText.text = price.ToString();
    }
    public void OnUpgradeClick()
    {
        if(progressIndex >= progressList.Count - 1) return;
        if (GameManager.Instance.PrimogemOwn >= currentPrice)
        {
            GameManager.Instance.PrimogemOwn -= currentPrice;
            progressIndex += 1;
            currentPrice += currentPrice;
            SetPriceText(currentPrice);
            SetProgress();
            SetBuffTimeWithType();
        }
    }

    public void GetProgressListInit()
    {
        for (int i = 0; i < progressHolder.transform.childCount; i++)
        {
           var progress = progressHolder.transform
                .GetChild(i).GetComponent<Progress>();
            if(progress) progressList.Add(progress);
        }
    }

    public void SetProgressInit()
    {
        for (int i = 0; i < progressList.Count; i++)
        {
            var progress = progressList[i];
            if(i == 0)
            {
                progress.IsUnlock = true;
                progress.SetCurrentProgressState(true);
                SetCostProgressTextWithType(progress);
            }
            else
            {
                progress.IsUnlock = false;
                progress.SetCurrentProgressState(false);
                progress.SetCostText();
            }
        }
    }

    public void SetProgress()
    {
        for (int i = 0; i < progressList.Count; i++)
        {
            var progress = progressList[i];
            if (i == progressIndex)
            {
                progress.IsUnlock = true;
                progress.SetCurrentProgressState(true);
                SetCostProgressTextWithType(progress);
            }
            else
            {
                if (progress.IsUnlock)
                {
                    progress.SetCostText();
                    progress.SetColorProgressImage(
                        UIManager.Instance.UICanvas.UpgradePanel.UnlockColor);
                }
            }
        }
    }

    private void SetCostProgressTextWithType(Progress progress)
    {
        var buffManager = BuffManager.Instance;
        switch (buffUpgradeConfig.buffType)
        {
            case BuffType.Speed:
                progress.SetCostText(buffManager.SpeedBuffTime);
                break;
            case BuffType.Point:
                progress.SetCostText(buffManager.PointBuffTime);
                break;
            case BuffType.Shield:
                progress.SetCostText(buffManager.ShieldBuffTime);
                break;
            case BuffType.Time:
                progress.SetCostText(buffManager.TimeBuffTime);
                break;
            case BuffType.Primogem:
                progress.SetCostText(buffManager.PrimogemBuffTime);
                break;
        }
    }

    private void SetBuffTimeWithType()
    {
        var progress = progressList[progressIndex];
        var buffManager = BuffManager.Instance;
        switch (buffUpgradeConfig.buffType)
        {
            case BuffType.Speed:
                buffManager.SpeedBuffTime += 1;
                progress.SetCostText(buffManager.SpeedBuffTime);
                break;
            case BuffType.Point:
                buffManager.PointBuffTime += 1;
                progress.SetCostText(buffManager.PointBuffTime);
                break;
            case BuffType.Shield:
                buffManager.ShieldBuffTime += 1;
                progress.SetCostText(buffManager.ShieldBuffTime);
                break;
            case BuffType.Time:
                buffManager.TimeBuffTime += 1;
                progress.SetCostText(buffManager.TimeBuffTime);
                break;
            case BuffType.Primogem:
                buffManager.PrimogemBuffTime += 1;
                progress.SetCostText(buffManager.PrimogemBuffTime);
                break;
        }
    }
}
