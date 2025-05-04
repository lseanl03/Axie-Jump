using UnityEngine;

public class TimeBuff : Buff
{
    public override void ApplyBuff()
    {
        base.ApplyBuff();
        if (buffManager.IsUsingTimeBuff) return;
        else buffManager.IsUsingTimeBuff = true;

        var timeBuff = buffData.buffs.valueEffect;
        gameManager.PlayTime += timeBuff;
        UIManager.Instance.UICanvas.GamePanel.SetUpdateTimeText(timeBuff);

    }

    public override void RemoveBuff()
    {
        base.RemoveBuff();
        buffManager.IsUsingTimeBuff = false;
    }
    public override void ApplyEffect()
    {
        PoolManager.Instance.GetObj(
            PoolType.Effect_TimeBuff,
            collider2d.transform.position,
            collider2d.transform);
    }
}
