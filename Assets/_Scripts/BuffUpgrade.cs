using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
        progressIndex = GameManager.LoadIntData(
            $"BuffUpgrade{buffUpgradeConfig.buffType}");
        if (progressIndex == 0)
        {
            progressIndex = 1;
            currentPrice = buffUpgradeConfig.startPrice;
            GetProgressListInit();
            SetProgressInit();
        }
        else
        {
            currentPrice = GameManager.LoadIntData(
                $"BuffUpgrade{buffUpgradeConfig.buffType}CurrentPrice");
            GetProgressListInit();
            SetProgressInit();
        }
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
        if(price == -1)
        {
            priceText.text = "MAX";
        }
        else
        {
            priceText.text = price.ToString();
        }
    }
    public void OnUpgradeClick()
    {
        if (progressList[progressList.Count - 1].IsUnlock ||
            progressIndex == -1) return;

        AudioManager.Instance.PlaySFX(AudioType.UpgradeClick);
        if (GameManager.Instance.PrimogemOwn >= currentPrice)
        {
            GameManager.Instance.PrimogemOwn -= currentPrice;
            UIManager.Instance.UICanvas.MainMenuPanel.SetPrimogem(GameManager.Instance.PrimogemOwn);
            PlayFabManager.Instance.SubPrimogem(currentPrice);
            SetBuffTimeWithType();

            progressList[progressIndex].IsUnlock = true;

            if(progressIndex == progressList.Count - 1)
            {
                progressList[progressIndex].SetCurrentProgressState(false);
                progressIndex = -1;
                currentPrice = -1;
                SetPriceText(currentPrice);
            }
            else
            {
                progressIndex += 1;
                currentPrice += currentPrice;
                SetPriceText(currentPrice);
                SetProgress();
            }
            GameManager.SaveIntData(
                $"BuffUpgrade{buffUpgradeConfig.buffType}", progressIndex);
            GameManager.SaveIntData(
                $"BuffUpgrade{buffUpgradeConfig.buffType}CurrentPrice", currentPrice);
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
            if(progressIndex == -1)
            {
                progress.IsUnlock = true;
                progress.SetCurrentProgressState(false);
            }
            else if(i < progressIndex)
            {
                progress.IsUnlock = true;
                progress.SetCurrentProgressState(false);
            }
            else if(i == progressIndex)
            {
                progress.SetCurrentProgressState(true);
                SetCostProgressTextWithType(progress);
            }
            else
            {
                progress.IsUnlock = false;
                progress.SetHideColor();
                progress.SetCostState(false);
            }
        }

        SetPriceText(currentPrice);
    }

    public void SetProgress()
    {
        if (progressIndex == -1) return;
        for (int i = 0; i < progressList.Count; i++)
        {
            var progress = progressList[i];
            if (i == progressIndex)
            {
                progress.SetCurrentProgressState(true);
                SetCostProgressTextWithType(progress);
            }
            else
            {
                if (progress.IsUnlock)
                {
                    progress.SetCurrentProgressState(false);
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
                progress.SetCostText(buffManager.SpeedBuffTime + 1);
                break;
            case BuffType.Point:
                progress.SetCostText(buffManager.PointBuffTime + 1);
                break;
            case BuffType.Shield:
                progress.SetCostText(buffManager.ShieldBuffTime + 1);
                break;
            case BuffType.Time:
                progress.SetCostText(buffManager.ValueTimeBuff + 1);
                break;
            case BuffType.Primogem:
                progress.SetCostText(buffManager.PrimogemBuffTime + 1);
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
                GameManager.SaveBuffData(
                    BuffType.Speed.ToString(), buffManager.SpeedBuffTime);
                break;
            case BuffType.Point:
                buffManager.PointBuffTime += 1;
                GameManager.SaveBuffData(
                    BuffType.Point.ToString(), buffManager.PointBuffTime);
                break;
            case BuffType.Shield:
                buffManager.ShieldBuffTime += 1;
                GameManager.SaveBuffData(
                    BuffType.Shield.ToString(), buffManager.ShieldBuffTime);
                break;
            case BuffType.Time:
                buffManager.ValueTimeBuff += 1;
                GameManager.SaveBuffData(
                    BuffType.Time.ToString(), buffManager.ValueTimeBuff);
                break;
            case BuffType.Primogem:
                buffManager.PrimogemBuffTime += 1;
                GameManager.SaveBuffData(
                    BuffType.Primogem.ToString(), buffManager.PrimogemBuffTime);
                break;
        }
    }
}
