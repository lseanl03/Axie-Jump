using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float scrollSpeed = 0.01f;
    private float distance = GameConfig.bgTransitionDistance;
    private Material material;
    private Coroutine autoScroll;

    private void Start()
    {
        if (material == null)
            material = GetComponentInChildren<Renderer>().material;

        if(GameManager.Instance.SceneType == SceneType.MainMenu)
        {
            if (autoScroll != null) StopCoroutine(autoScroll);
            autoScroll = StartCoroutine(AutoScrollCoroutine(scrollSpeed));
        }
    }

    private void OnEnable()
    {
        EventManager.onNormalJump += Scroll;
    }

    private void OnDisable()
    {
        EventManager.onNormalJump -= Scroll;
    }

    /// <summary>
    /// Cuộn background
    /// </summary>
    public void Scroll()
    {
        Vector2 currentOffset = material.GetTextureOffset("_MainTex");
        Vector2 targetOffset = currentOffset + Vector2.up * distance;

        DOTween.To(
            () => currentOffset, value => { 
                currentOffset = value; 
                material.SetTextureOffset("_MainTex", currentOffset); },
            targetOffset, GameConfig.bgTransitionTime );
    }

    private IEnumerator AutoScrollCoroutine(float speed)
    {
        while (true)
        {
            Vector2 currentOffset = material.GetTextureOffset("_MainTex");
            currentOffset += Vector2.up * speed * Time.deltaTime;
            material.SetTextureOffset("_MainTex", currentOffset);
            yield return null;
        }
    }
}
