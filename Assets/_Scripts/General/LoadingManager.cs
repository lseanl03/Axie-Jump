using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : Singleton<LoadingManager>
{
    private Coroutine transitionCoroutine;
    public void TransitionLevel(SceneType sceneType)
    {
        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
        transitionCoroutine = StartCoroutine(TransitionCoroutine(sceneType));
    }

    private IEnumerator TransitionCoroutine(SceneType sceneType)
    {
        var uiCanvas = UIManager.Instance.UICanvas;
        uiCanvas.LevelTransiton.gameObject.SetActive(true);

        yield return new WaitForSeconds(GameConfig.closeOverlay);

        uiCanvas.RestartButton.gameObject.SetActive(false);

        DOTween.KillAll();

        var loadScene = SceneManager.LoadSceneAsync(sceneType.ToString());
        loadScene.allowSceneActivation = false;
        while (!loadScene.isDone)
        {
            if (loadScene.progress >= 0.9f)
            {
                loadScene.allowSceneActivation = true;
                GameManager.Instance.SceneType = sceneType;
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        
        uiCanvas.LevelTransiton.TransitionState(true);
        EventManager.SceneChangedAction(sceneType);
        
        yield return new WaitForSeconds(GameConfig.openOverlay);

        uiCanvas.LevelTransiton.TransitionState(false);
        uiCanvas.LevelTransiton.gameObject.SetActive(false);
    }
}
