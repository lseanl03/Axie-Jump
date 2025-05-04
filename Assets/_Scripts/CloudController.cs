using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    private float scrollSpeed = 0.01f;
    private Material material;
    private Coroutine autoScroll;

    private void Start()
    {
        if (material == null)
            material = GetComponentInChildren<Renderer>().material;

        if (autoScroll != null) StopCoroutine(autoScroll);
        autoScroll = StartCoroutine(AutoScrollCoroutine(scrollSpeed));
    }
    private IEnumerator AutoScrollCoroutine(float speed)
    {
        while (true)
        {
            Vector2 currentOffset = material.GetTextureOffset("_MainTex");
            currentOffset += Vector2.right * speed * Time.deltaTime;
            material.SetTextureOffset("_MainTex", currentOffset);
            yield return null;
        }
    }
}
