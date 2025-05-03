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
            SetColorProgressImage(uiManager.UICanvas.UpgradePanel.CurrentProgressColor);
            SetCostState(true);
        }
        else
        {
            SetColorProgressImage(Color.black);
            SetCostState(false);
        }
    }
    public bool IsUnlock
    {
        get { return isUnlock; }
        set { isUnlock = value; }
    }
    public void SetColorProgressImage(Color color)
    {
        progressImage.color = color;
    }
    public void SetCostText(float cost = 0)
    {
        if(cost == 0) costText.text = "";
        else costText.text = $"{cost}s";
    }

    private void SetCostState(bool state)
    {
        costText.gameObject.SetActive(state);
    }
}
