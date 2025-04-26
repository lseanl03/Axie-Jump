using DG.Tweening;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static EventManager;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private LevelTransiton levelTransiton;
    [SerializeField] private Button restartButton;
    private Coroutine transitionCoroutine;

    private void OnEnable()
    {
        EventManager.onGameOver += OnGameOver;
    }
    private void OnDisable()
    {
        EventManager.onGameOver -= OnGameOver;
    }
    private void OnGameOver()
    {
        restartButton.gameObject.SetActive(true);
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
}
