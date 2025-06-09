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
    protected Collider2D collider2d;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollider(collision);
    }

    private void CheckCollider(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(AudioType.CollectBuff);

            collider2d = collision;
            var player = collision.GetComponent<PlayerController>();
            if (player == null) return;

            ActionBuff();
            ApplyEffect();
            PoolManager.Instance.ReturnObjFromTrunk(gameObject, poolType);
        }
    }

}
