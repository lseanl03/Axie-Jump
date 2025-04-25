using UnityEngine;

public class UFO : Enemy
{

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        anim.AnimationState.SetAnimation(0, idleAnim, true);
    }
}
