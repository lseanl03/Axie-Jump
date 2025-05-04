using DG.Tweening;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI primogemText;
    [SerializeField] private TextMeshProUGUI playTimeText;
    [SerializeField] private TextMeshProUGUI updateTimeText;
    [SerializeField] private GameObject gameMenu;
    private void Start()
    {
        SetPointText(GameManager.Instance.Points);
        SetPrimogemText(GameManager.Instance.Primogems);
    }
    private void OnEnable()
    {
        EventManager.onCollectItem += OnCollectItem;
        EventManager.onSceneChanged += OnChangedScene;
    }
    private void OnDisable()
    {
        EventManager.onCollectItem -= OnCollectItem;
        EventManager.onSceneChanged -= OnChangedScene;
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

    private void OnChangedScene(SceneType sceneType)
    {
        gameMenu.SetActive(sceneType == SceneType.Game);
        pointText.gameObject.SetActive(sceneType == SceneType.Game);
        primogemText.gameObject.SetActive(sceneType == SceneType.Game);
        playTimeText.gameObject.SetActive(sceneType == SceneType.Game);
        if (sceneType == SceneType.Game)
        {
            SetPlayTime(GameManager.Instance.PlayTime);
            SetPointText(0);
            SetPrimogemText(0);
        }
    }
}
