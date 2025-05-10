using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UsingBuff : MonoBehaviour
{
    [SerializeField] private Image buffImage;
    [SerializeField] private TextMeshProUGUI durationText;
    [SerializeField] private BuffType buffType;
    private Coroutine applyBuffCoroutine;
    private float durationTime;

    public BuffType BuffType
    {
        get { return buffType; }
        set { buffType = value; }
    }

    private void OnEnable()
    {
        EventManager.onApplyBuff += OnApplyBuff;
        EventManager.onGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventManager.onApplyBuff -= OnApplyBuff;
        EventManager.onGameOver -= OnGameOver;
        if (applyBuffCoroutine != null) StopCoroutine(applyBuffCoroutine);
    }
    private void OnGameOver()
    {
        buffImage.gameObject.SetActive(false);
        durationText.gameObject.SetActive(false);
    }
    private void OnApplyBuff(Buff buff)
    {
        if (buff.BuffData.buffs.buffType == buffType)
        {
            if (applyBuffCoroutine != null) StopCoroutine(applyBuffCoroutine);
            applyBuffCoroutine = StartCoroutine(ApplyBuffCoroutine(buff));
        }
    }

    public void SetBuffImage(Sprite sprite)
    {
        buffImage.sprite = sprite;
    }

    public void SetDurationText(float duration)
    {
        durationText.text = $"{Mathf.Ceil(duration)}s";
    }

    private IEnumerator ApplyBuffCoroutine(Buff buff)
    {
        transform.SetSiblingIndex(0);
        buffImage.gameObject.SetActive(true);
        durationText.gameObject.SetActive(true);
        durationTime = BuffManager.Instance
            .GetBuffTimeWithType(buff.BuffData.buffs.buffType);
        SetBuffImage(buff.BuffData.buffs.sprite);
        while (durationTime > 0)
        {
            durationTime -= Time.deltaTime;
            if (durationTime <= 0) durationTime = 0;

            SetDurationText(durationTime);
            yield return null;
        }
        buffImage.gameObject.SetActive(false);
        durationText.gameObject.SetActive(false);
        buff.RemoveBuff();
    }
}
