using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class GetPrimogemPopup : MonoBehaviour
{
    [SerializeField] private Vector2 lastPos;
    private TextMeshPro text;
    private Coroutine hideCoroutine;
    private Sequence hideSequence;
    private void Awake()
    {
        if (text == null)
            text = GetComponentInChildren<TextMeshPro>();
    }
    private void OnEnable()
    {
        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        hideCoroutine = StartCoroutine(HidePopup());
    }

    private void OnDisable()
    {
        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
    }
    public void SetText(int point)
    {
        this.text.text = point.ToString();
    }

    private IEnumerator HidePopup()
    {
        text.alpha = 0;
        yield return new WaitForSeconds(0.1f);
        hideSequence = DOTween.Sequence();
        hideSequence.Append(text.DOFade(1f, 0.5f));
        hideSequence.Join(transform.DOMoveY(transform.position.y + 1f, 0.5f));
        hideSequence.Append(transform.DOMove(lastPos, 0.5f));
        hideSequence.Join(text.DOFade(0f, 0.5f));
        hideSequence.OnComplete(() =>
        {
            PoolManager.Instance.ReturnObj(gameObject, PoolType.GetPrimogemPopup);
        });
    }
}
