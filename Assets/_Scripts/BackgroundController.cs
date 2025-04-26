using DG.Tweening;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float distance = GameConfig.bgTransitionDistance;
    private Material material;

    private void Start()
    {
        if (material == null)
        {
            material = GetComponentInChildren<Renderer>().material;
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
}
