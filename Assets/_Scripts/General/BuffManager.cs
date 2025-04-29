using UnityEngine;

public class BuffManager : Singleton<BuffManager>
{
    [SerializeField] private bool isUsingBuff;
    private BuffData buffData;

    protected override void Awake()
    {
        buffData = Resources.Load<BuffData>("SOData/BuffData");
    }

    public void UseBuff(BuffType buffType)
    {
        if (isUsingBuff) return;
        isUsingBuff = true;

        var buff = GetBuffConfig(buffType);
        ApplyEffect(buff);   
    }

    private BuffConfig GetBuffConfig(BuffType buffType)
    {
        foreach (var buff in buffData.buffs)
        {
            if (buff.buffType == buffType) 
                return buff;
        }
        return null;
    }

    private void ApplyEffect(BuffConfig buff)
    {
        switch(buff.buffType)
        {
            case BuffType.Time:
                
                break;
            case BuffType.Primogem:
                // Apply defense buff effect
                break;
            case BuffType.Point:
                // Apply speed buff effect
                break;
            case BuffType.Shield:
                // Apply shield buff effect
                break;
            case BuffType.Speed:
                // Apply speed buff effect
                break;
        }
    }


}
