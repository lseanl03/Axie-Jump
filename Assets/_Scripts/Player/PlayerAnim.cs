using DG.Tweening.Core.Easing;
using Spine.Unity;
using System.Collections;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private AnimationReferenceAsset idle;
    [SerializeField] private AnimationReferenceAsset jump;
    [SerializeField] private AnimationReferenceAsset die;
    [SerializeField] private AnimationReferenceAsset collectItem;
    [SerializeField] private AnimationReferenceAsset[] randomIdles;

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
        RandomIdles();
    }
    public SkeletonAnimation Anim
    {
        get { return anim; }
        set { anim = value; }
    }
    private void OnEnable()
    {
        if(dieCoroutine != null) StopCoroutine(dieCoroutine);
    }
    public AnimationReferenceAsset[] RandomIdlesAnim
    {
        get { return randomIdles; }
        set { randomIdles = value; }
    }
    public AnimationReferenceAsset IdleAnim
    {
        get { return idle; }
        set { idle = value; }
    }
    public AnimationReferenceAsset JumpAnim
    {
        get { return jump; }
        set { jump = value; }
    }
    public AnimationReferenceAsset DieAnim
    {
        get { return die; }
        set { die = value; }
    }
    public AnimationReferenceAsset CollectItemAnim
    {
        get { return collectItem; }
        set { collectItem = value; }
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
    public void CollectItem()
    {
        anim.state.SetAnimation(0, collectItem, false);
        anim.state.AddAnimation(0, idle, true, 0);
    }

    public void RandomIdles()
    {
        int randomIndex = Random.Range(0, randomIdles.Length);
        anim.state.SetAnimation(0, randomIdles[randomIndex], true);
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
