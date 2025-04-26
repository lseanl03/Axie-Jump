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
    private Coroutine transitionCoroutine;

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
        pointText.text = $"Điểm: {point}";
    }

    private void SetPrimogemText(int primogem)
    {
        primogemText.text = $"Nguyên thạch: {primogem}";
    }
}
