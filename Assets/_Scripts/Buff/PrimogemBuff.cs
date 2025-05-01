using UnityEngine;

public class PrimogemBuff : Buff
{
    public override void ApplyBuff()
    {
        base.ApplyBuff();
        if (buffManager.IsUsingPrimogemBuff) return;
        else buffManager.IsUsingPrimogemBuff = true;

    }

    public override void RemoveBuff()
    {
        base.RemoveBuff();
        buffManager.IsUsingPrimogemBuff = false;
    }
    public override void ApplyEffect()
    {
        PoolManager.Instance.GetObj(
            PoolType.Effect_PrimogemBuff,
            collider2d.transform.position,
            collider2d.transform);
    }
}
