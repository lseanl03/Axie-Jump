using UnityEngine;

public class PrimogemBuff : Buff
{
    public override void ApplyBuff()
    {
        base.ApplyBuff();
        if (buffManager.IsUsingPrimogemBuff) return;
        else buffManager.IsUsingPrimogemBuff = true;

        gameManager.PrimogemPoint *= 2;
    }

    public override void RemoveBuff()
    {
        base.RemoveBuff();
        buffManager.IsUsingPrimogemBuff = false;

        gameManager.PrimogemPoint = GameConfig.primogemPoint;
    }
    public override void ApplyEffect()
    {
        PoolManager.Instance.GetObj(
            PoolType.Effect_PrimogemBuff,
            collider2d.transform.position,
            collider2d.transform);
    }
}
