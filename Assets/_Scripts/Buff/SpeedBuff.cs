using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBuff : Buff
{

    public override void ApplyBuff()
    {
        base.ApplyBuff();
        if (buffManager.IsUsingSpeedBuff) return;
        else buffManager.IsUsingSpeedBuff = true;
        
        gameManager.Player.JumpTime = gameManager.Player.JumpTime / 2;
        trunkManager.TransitionTime = trunkManager.TransitionTime / 2;
    }
    public override void RemoveBuff()
    {
        base.RemoveBuff();
        buffManager.IsUsingSpeedBuff = false;

        gameManager.Player.JumpTime = GameConfig.jumpTime;
        trunkManager.TransitionTime = GameConfig.trunkTransitionTime;
    }

    public override void ApplyEffect()
    {
        PoolManager.Instance.GetObj(
            PoolType.Effect_SpeedBuff, 
            collider2d.transform.position, 
            collider2d.transform);
    }
}
