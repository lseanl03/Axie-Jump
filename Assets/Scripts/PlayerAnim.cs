using Spine.Unity;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private AnimationReferenceAsset idleAnim;
    [SerializeField] private AnimationReferenceAsset jumpAnim;

    private SkeletonAnimation anim;

    private void Awake()
    {
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
        anim.state.SetAnimation(0, idleAnim, true);
    }
    public void Jump()
    {
        anim.state.SetAnimation(0, jumpAnim, false);
    }
}
