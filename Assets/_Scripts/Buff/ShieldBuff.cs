using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ShieldBuff : Buff
{
    private float shieldNewPosY = 0.5f;
    public override void ApplyBuff()
    {
        base.ApplyBuff();
        if (buffManager.IsUsingShieldBuff) return;
        else
        {
            buffManager.IsUsingShieldBuff = true;
            gameManager.Player.CanDie = false;
        }
    }

    public override void RemoveBuff()
    {
        base.RemoveBuff();
        buffManager.IsUsingShieldBuff = false;
        gameManager.Player.CanDie = true;

        if (gameManager.Player.Shield)
        {
            PoolManager.Instance.ReturnObj(
                gameManager.Player.Shield.gameObject, PoolType.Shield);
            gameManager.Player.Shield = null;
        }
    }
    public override void ApplyEffect()
    {
        PoolManager.Instance.GetObj(PoolType.Effect_ShieldBuff,
            collider2d.transform.position,
            collider2d.transform);

        if (gameManager.Player.Shield)
        {
            PoolManager.Instance.ReturnObj(
                gameManager.Player.Shield.gameObject, PoolType.Shield);
            gameManager.Player.Shield = null;
        }

        var obj = PoolManager.Instance.GetObj(PoolType.Shield,
            collider2d.transform.position, collider2d.transform);
        if (obj.GetComponent<Shield>())
        {
            var shield = obj.GetComponent<Shield>();
            shield.transform.position =
                new Vector2(shield.transform.position.x,
                shield.transform.position.y + shieldNewPosY);

            gameManager.Player.Shield = shield;
        }
    }
}
