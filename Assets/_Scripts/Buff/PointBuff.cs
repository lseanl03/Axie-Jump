using UnityEngine;

public class PointBuff : Buff
{
    public override void ApplyBuff()
    {
        base.ApplyBuff();
        if (buffManager.IsUsingPointBuff) return;
        else buffManager.IsUsingPointBuff = true;

        gameManager.NormalItemPoint *= 2;
        gameManager.RareItemPoint *= 2;
    }

    public override void RemoveBuff()
    {
        base.RemoveBuff();
        buffManager.IsUsingPointBuff = false;

        gameManager.NormalItemPoint = GameConfig.normalItemPoint;
        gameManager.RareItemPoint = GameConfig.rareItemPoint;
    }
    public override void ApplyEffect()
    {
        PoolManager.Instance.GetObj(
            PoolType.Effect_PointBuff,
            collider2d.transform.position,
            collider2d.transform);
    }
}
