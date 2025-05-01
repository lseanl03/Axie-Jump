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
    [SerializeField] private Image requestPanel;
    [SerializeField] private TextMeshProUGUI playTimeText;
    [SerializeField] private TextMeshProUGUI updateTimeText;

    private void Start()
    {
        SetPointText(GameManager.Instance.Points);
        SetPrimogemText(GameManager.Instance.Primogems);
    }
    private void OnEnable()
    {
        EventManager.onCollectItem += OnCollectItem;
    }
    private void OnDisable()
    {
        EventManager.onCollectItem -= OnCollectItem;
    }
    private void OnCollectItem(Rate rate)
    {
        switch(rate)
        {
            case Rate.Normal:
                SetUpdateTimeText(1);
                SetPointText(GameManager.Instance.Points);
                break;
            case Rate.Rare:
                SetUpdateTimeText(2);
                SetPointText(GameManager.Instance.Points);
                break;
            case Rate.Special:
                SetPrimogemText(GameManager.Instance.Primogems);
                break;
        }
    }

    private void SetPointText(int point)
    {
        pointText.text = $"{point}";
    }

    private void SetPrimogemText(int primogem)
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
