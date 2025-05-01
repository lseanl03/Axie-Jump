using Spine.Unity;
using System.Collections;
using UnityEditorInternal;
using UnityEngine;

public class TreantFlowering : Enemy
{
    [SerializeField] private AnimationReferenceAsset hurt;
    private Coroutine hurtCoroutine;
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        if (!animator)
            animator = GetComponentInChildren<Animator>();

        initialPosY = 0.4f;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if(hurtCoroutine != null) StopCoroutine(hurtCoroutine);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Hurt();
            var player = collision.GetComponent<PlayerController>();
            player.Hurt();
        }
    }
    private void Hurt()
    {
        if (hurtCoroutine != null) StopCoroutine(hurtCoroutine);
        hurtCoroutine = StartCoroutine(HurtCoroutine());
    }

    private IEnumerator HurtCoroutine()
    {
        anim.AnimationState.SetAnimation(0, hurt, false);
        yield return new WaitForSeconds(0.3f);
        anim.timeScale = 0;
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(1f);
        PoolManager.Instance.ReturnObjFromTrunk(
            gameObject, poolType);
    }
}
