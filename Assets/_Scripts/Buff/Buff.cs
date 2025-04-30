using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    protected int amount;
    protected float fadeTime = 0.5f;
    protected float initialPosY = 1.5f;

    protected Coroutine applyBuffCoroutine;
    protected BuffData buffData;

    [SerializeField] protected PoolType poolType;

    protected GameManager gameManager => GameManager.Instance;
    protected TrunkManager trunkManager => TrunkManager.Instance;
    protected BuffManager buffManager => BuffManager.Instance;

    public int Amount
    {
        get => amount;
        set => amount = value;
    }

    public PoolType PoolType
    {
        get => poolType;
        set => poolType = value;
    }

    public BuffData BuffData
    {
        get => buffData;
        set => buffData = value;
    }

    protected virtual void Awake()
    {
        buffData = Resources.Load<BuffData>($"SOData/Buff/{GetType().Name}Data");
    }

    private void OnDisable()
    {
        if (applyBuffCoroutine != null) StopCoroutine(applyBuffCoroutine);
    }

    public virtual void ActionBuff()
    {
        ApplyBuff();
    }
    public virtual void ApplyBuff()
    {
        EventManager.ApplyBuffAction(this);
    }
    public virtual void RemoveBuff() { }

    public virtual void ApplyEffect() { }
    public virtual void RemoveEffect() { }


    public void SetInitialPosY()
    {
        transform.position = new Vector2(transform.position.x,
            transform.position.y + initialPosY);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActionBuff();
            PoolManager.Instance.ReturnObjFromTrunk(gameObject, poolType);
        }
    }
}
