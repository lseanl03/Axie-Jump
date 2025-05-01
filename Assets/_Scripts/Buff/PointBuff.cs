using UnityEngine;

public class PointBuff : Buff
{
    public override void ApplyBuff()
    {
        base.ApplyBuff();
        if (buffManager.IsUsingPointBuff) return;
        else buffManager.IsUsingPointBuff = true;

    }

    public override void RemoveBuff()
    {
        base.RemoveBuff();
        buffManager.IsUsingPointBuff = false;
    }
    public override void ApplyEffect()
    {
        PoolManager.Instance.GetObj(
            PoolType.Effect_PointBuff,
            collider2d.transform.position,
            collider2d.transform);
    }
}
