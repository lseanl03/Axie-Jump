using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBuff : Buff
{
    private float initialJumpTime;
    private float initialTrasitionTime;

    public override void ApplyBuff()
    {
        base.ApplyBuff();
        if (buffManager.IsUsingSpeedBuff) return;
        else buffManager.IsUsingSpeedBuff = true;

        initialJumpTime = gameManager.Player.JumpTime;
        initialTrasitionTime = trunkManager.TransitionTime;

        gameManager.Player.JumpTime = initialJumpTime / 2;
        trunkManager.TransitionTime = initialTrasitionTime / 2;
    }
    public override void RemoveBuff()
    {
        base.RemoveBuff();
        buffManager.IsUsingSpeedBuff = false;

        gameManager.Player.JumpTime = initialJumpTime;
        trunkManager.TransitionTime = initialTrasitionTime;
    }


}
