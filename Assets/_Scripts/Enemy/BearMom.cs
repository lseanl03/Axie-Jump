using Spine.Unity;
using UnityEngine;

public class BearMom : Enemy
{
    [SerializeField] private AnimationReferenceAsset getBuff;
    [SerializeField] private AnimationReferenceAsset debuff;

    protected override void Awake()
    {
        base.Awake();
        initialPosY = 0.3f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OpenRequest();
        }
    }

    private void OpenRequest()
    {
        EventManager.OpenRequestAction(
            GameManager.Instance.RequestData.GetBearMomRequest());
        GameManager.Instance.CurrentRequestEnemy = this;
    }

    public override void StartAction()
    {
        var getBuffAnim = anim.AnimationState.SetAnimation(0, getBuff, false);
        anim.AnimationState.AddAnimation(0, idleAnim, true, 0);
        getBuffAnim.Complete += (trackEntry) =>
        {
            PoolManager.Instance.ReturnObjFromTrunk(gameObject, PoolType.Enemy_Bear_Mom);
        };
    }

    public override void EndAction()
    {
        var debuffAnim = anim.AnimationState.SetAnimation(0, debuff, false);
        anim.AnimationState.AddAnimation(0, idleAnim, true, 0);
    }
}
