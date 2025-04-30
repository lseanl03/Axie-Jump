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
    [SerializeField] private LevelTransiton levelTransiton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Image requestPanel;
    [SerializeField] private TextMeshProUGUI playTimeText;
    [SerializeField] private TextMeshProUGUI updateTimeText;
    private Coroutine transitionCoroutine;

    private void Start()
    {
        SetPointText(GameManager.Instance.Points);
        SetPrimogemText(GameManager.Instance.Primogems);
    }
    private void OnEnable()
    {
        EventManager.onGameOver += OnGameOver;
        EventManager.onCollectItem += OnCollectItem;
    }
    private void OnDisable()
    {
        EventManager.onGameOver -= OnGameOver;
        EventManager.onCollectItem -= OnCollectItem;
    }
    private void OnGameOver()
    {
        restartButton.gameObject.SetActive(true);
    }

    private void OnCollectItem(Rate rate)
    {
        switch(rate)
        {
            case Rate.Normal:
                SetPointText(GameManager.Instance.Points);
                break;
            case Rate.Rare:
                SetPointText(GameManager.Instance.Points);
                break;
            case Rate.Special:
                SetPrimogemText(GameManager.Instance.Primogems);
                break;
        }
    }

    public void TransitionLevel()
    {
        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
        transitionCoroutine = StartCoroutine(TransitionCoroutine());
    }

    private IEnumerator TransitionCoroutine()
    {
        levelTransiton.gameObject.SetActive(true);
        yield return new WaitForSeconds(GameConfig.closeOverlay);

        restartButton.gameObject.SetActive(false);
        levelTransiton.TransitionState(true);
        DOTween.KillAll();
        var loadScene = SceneManager.LoadSceneAsync("Main");

        yield return new WaitForSeconds(GameConfig.openOverlay);
        
        levelTransiton.TransitionState(false);
        levelTransiton.gameObject.SetActive(false);
    }

    private void SetPointText(int point)
    {
        pointText.text = $"{point}";
    }

    private void SetPrimogemText(int primogem)
    {
        primogemText.text = $"{primogem}";
    }

    public void SetPlayTime(float time)
    {
        playTimeText.text = $"{Mathf.Ceil(time)}s";
    }

    public void SetUpdateTimeText(float time)
    {
        updateTimeText.gameObject.SetActive(true);
        updateTimeText.text = $"+{Mathf.Ceil(time)}s";
        updateTimeText.rectTransform.position = new Vector2(0, 0);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(updateTimeText.rectTransform.DOLocalMoveX(170, 0.5f));
        sequence.Join(updateTimeText.DOFade(1, 0.5f));
        sequence.AppendInterval(0.5f);
        sequence.OnComplete(() =>
        {
            updateTimeText.gameObject.SetActive(false);
        });
    }
}
