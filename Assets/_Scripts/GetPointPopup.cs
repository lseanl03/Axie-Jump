using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GetPointPopup : MonoBehaviour
{
    private TextMeshPro text;
    private Coroutine hideCoroutine;
    private DG.Tweening.Sequence hideSequence;
    private void Awake()
    {
        if (text == null)
            text = GetComponent<TextMeshPro>();
    }
    private void OnEnable()
    {
        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        hideCoroutine = StartCoroutine(HidePopup());

    }

    private void OnDisable()
    {
        if (hideSequence != null) hideSequence.Kill();
        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
    }
    public void SetText(int point)
    {
        this.text.text = point.ToString();
    }

    private IEnumerator HidePopup()
    {
        text.alpha = 0f;
        yield return new WaitForSeconds(0.1f);
        hideSequence = DOTween.Sequence();
        hideSequence.Append(text.DOFade(1f, 0.5f));
        hideSequence.Join(transform.DOMoveY(transform.position.y + 3f, 0.5f));
        hideSequence.Append(text.DOFade(0f, 0.5f));
        hideSequence.OnComplete(() =>
        {
            PoolManager.Instance.ReturnObj(gameObject, PoolType.GetPointPopup);
        });
    }
}
