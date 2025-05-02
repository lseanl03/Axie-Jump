using DG.Tweening;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI primogemText;
    [SerializeField] private TextMeshProUGUI playTimeText;
    [SerializeField] private TextMeshProUGUI updateTimeText;
    [SerializeField] private RequestPanel requestPanel;

    private void Start()
    {
        SetPointText(GameManager.Instance.Points);
        SetPrimogemText(GameManager.Instance.Primogems);
    }

    public RequestPanel RequestPanel
    {
        get { return requestPanel; }
    }
    private void OnEnable()
    {
        EventManager.onCollectItem += OnCollectItem;
    }
    private void OnDisable()
    {
        EventManager.onCollectItem -= OnCollectItem;
    }
    private void OnCollectItem(Item item)
    {
        var gameManager = GameManager.Instance;
        switch (item.Rate)
        {
            case Rate.Normal:
                SetUpdateTimeText(GameConfig.normalItemTime);
                break;
            case Rate.Rare:
                SetUpdateTimeText(GameConfig.rareItemTime);
                break;
        }
    }

    public void SetPointText(int point)
    {
        pointText.text = $"{point}";
    }

    public void SetPrimogemText(int primogem)
    {
        primogemText.text = $"{primogem.ToString("D4")}";
    }

    public void SetPlayTime(float time)
    {
        playTimeText.text = $"{Mathf.Ceil(time)}s";
    }

    public void SetUpdateTimeText(float time)
    {
        updateTimeText.GetComponent<Animator>().SetTrigger("Show");
        updateTimeText.text = $"+{Mathf.Ceil(time)}s";
    }
}
