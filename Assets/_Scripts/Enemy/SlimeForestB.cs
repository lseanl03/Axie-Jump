using DG.Tweening;
using Spine.Unity;
using System.Collections;
using UnityEngine;

public class SlimeForestB : Enemy
{
    [SerializeField] private AnimationReferenceAsset run;
    [SerializeField] private AnimationReferenceAsset die;

    private float runTime = 4f;
    private float waitTime = 1f;

    private Tween runTween;
    private Coroutine runCoroutine;
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rb2d;

    protected override void Awake()
    {
        base.Awake();

        if (!boxCollider2D)
            boxCollider2D = GetComponent<BoxCollider2D>();

        if (!rb2d)
            rb2d = GetComponent<Rigidbody2D>();
    }

    protected override void OnEnable()
    {
        Run();
    }
    private void OnDisable()
    {
        if (runCoroutine != null) StopCoroutine(runCoroutine);
        if (runTween != null) runTween.Kill();
    }
    public void Run()
    {
        if (runCoroutine != null) StopCoroutine(runCoroutine);
        runCoroutine = StartCoroutine(RunCoroutine());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerController>();
            player.Die();

            Die();
        }
    }
    private IEnumerator RunCoroutine()
    {
        while(gameObject.activeSelf)
        {
            anim.AnimationState.SetAnimation(0, run, true);
            MoveToTarget(transform.localScale.x == 1);
            yield return new WaitForSeconds(runTime);
            anim.AnimationState.SetAnimation(0, idleAnim, true);
            SetInitialDir();
            yield return new WaitForSeconds(waitTime);
        }

    }

    private void MoveToTarget(bool isLeft)
    {
        var newPosX = isLeft ? GameConfig.minPos : GameConfig.maxPos;
        runTween = transform.DOMoveX(newPosX, runTime).SetEase(Ease.Linear);
    }

    private void Die()
    {
        var dieAnim = anim.AnimationState.SetAnimation(0, die, false);
        dieAnim.Complete += (e) =>
        {
            PoolManager.Instance.ReturnObjFromTrunk(gameObject, PoolType);
        };
    }
}
