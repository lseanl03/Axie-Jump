using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    [SerializeField] private bool isCurrentProgress;
    [SerializeField] private bool isUnlock;

    [SerializeField] private Image progressImage;
    [SerializeField] private TextMeshProUGUI costText;

    public void SetCurrentProgressState(bool state)
    {
        var uiManager = UIManager.Instance;
        isCurrentProgress = state;
        if (isCurrentProgress)
        {
            SetCurrentProgressColor();
            SetCostState(true);
        }
        else
        {
            SetUnlockColor();
            SetCostState(false);
        }
    }
    public bool IsUnlock
    {
        get { return isUnlock; }
        set { isUnlock = value; }
    }
    public bool IsCurrentProgress
    {
        get { return isCurrentProgress; }
        set { isCurrentProgress = value; }
    }
    public void SetHideColor()
    {
        progressImage.color = Color.black;
    }
    public void SetUnlockColor()
    {
        progressImage.color = 
            UIManager.Instance.UICanvas.UpgradePanel.UnlockColor;
    }
    public void SetCurrentProgressColor()
    {
        progressImage.color =
            UIManager.Instance.UICanvas.UpgradePanel.CurrentProgressColor;
    }
    public void SetCostText(float cost)
    {
        if(cost == 0) costText.text = "";
        else costText.text = $"{cost}s";
    }

    public void SetCostState(bool state)
    {
        costText.gameObject.SetActive(state);
    }
}
