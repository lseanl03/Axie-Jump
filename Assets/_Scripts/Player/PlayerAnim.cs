using DG.Tweening.Core.Easing;
using Spine.Unity;
using System.Collections;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private AnimationReferenceAsset idle;
    [SerializeField] private AnimationReferenceAsset jump;
    [SerializeField] private AnimationReferenceAsset die;

    private SkeletonAnimation anim;
    private Coroutine dieCoroutine;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        anim = GetComponentInChildren<SkeletonAnimation>();
    }

    private void Start()
    {
        if (anim == null) return;
        Idle();
    }

    public SkeletonAnimation GetSkeletionAnim()
    {
        return anim;
    }

    public void Idle()
    {
        anim.state.SetAnimation(0, idle, true);
    }
    public void Jump()
    {
        anim.state.SetAnimation(0, jump, false);
    }
    public void Die()
    {
        anim.state.SetAnimation(0, die, false);

        if (dieCoroutine != null) StopCoroutine(dieCoroutine);
        dieCoroutine = StartCoroutine(DieCoroutine());
    }

    #region Coroutine
    private IEnumerator DieCoroutine()
    {
        animator.SetTrigger("isDie");
        yield return new WaitForSeconds(0.5f);
        anim.timeScale = 0;
    }
    #endregion
}
