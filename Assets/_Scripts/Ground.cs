using DG.Tweening;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.onGameStart += TransitionGround;
    }

    private void OnDisable()
    {
        EventManager.onGameStart -= TransitionGround;
    }

    private void TransitionGround()
    {
        transform.DOMoveY(
            transform.position.y - GameConfig.groundTransitionDistance, 
            GameConfig.groundTransitionTime)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}
