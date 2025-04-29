using UnityEngine;

public class SpeedBuff : Buff
{
    public override void ApplyBuff()
    {

    }
    public override void RemoveBuff()
    {
        base.RemoveBuff();
        // Remove speed buff logic here
        Debug.Log("Speed buff removed");
    }
}
