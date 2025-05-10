using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : Singleton<BuffManager>
{
    private bool isUsingSpeedBuff = false;
    private bool isUsingPointBuff = false;
    private bool isUsingPrimogemBuff = false;
    private bool isUsingShieldBuff = false;
    private bool isUsingTimeBuff = false;

    private float speedTimeBuff;
    private float pointTimeBuff;
    private float primogemTimeBuff;
    private float shieldTimeBuff;
    private float timeTimeBuff;

    private float valueTimeBuff;

    private BuffListData buffListData;
    private BuffUpgradeData buffUpgradeData;

    protected override void Awake()
    {
        base.Awake();
        buffListData = Resources.Load<BuffListData>("SOData/Buff/BuffListData");
        buffUpgradeData = Resources.Load<BuffUpgradeData>("SOData/BuffUpgradeData");
    }
    private void Start()
    {
        SetTimeBuffInit();
    }
    #region Get Set
    public float ValueTimeBuff
    {
        get => valueTimeBuff;
        set => valueTimeBuff = value;
    }
    public float SpeedBuffTime
    {
        get => speedTimeBuff;
        set => speedTimeBuff = value;
    }
    public float PointBuffTime
    {
        get => pointTimeBuff;
        set => pointTimeBuff = value;
    }
    public float PrimogemBuffTime
    {
        get => primogemTimeBuff;
        set => primogemTimeBuff = value;
    }
    public float ShieldBuffTime
    {
        get => shieldTimeBuff;
        set => shieldTimeBuff = value;
    }
    public float TimeBuffTime
    {
        get => timeTimeBuff;
        set => timeTimeBuff = value;
    }
    public BuffListData BuffListData
    {
        get => buffListData;
        set => buffListData = value;
    }
    public BuffUpgradeData BuffUpgradeData
    {
        get => buffUpgradeData;
        set => buffUpgradeData = value;
    }
    public bool IsUsingSpeedBuff
    {
        get => isUsingSpeedBuff;
        set => isUsingSpeedBuff = value;
    }

    public bool IsUsingPointBuff
    {
        get => isUsingPointBuff;
        set => isUsingPointBuff = value;
    }

    public bool IsUsingPrimogemBuff
    {
        get => isUsingPrimogemBuff;
        set => isUsingPrimogemBuff = value;
    }

    public bool IsUsingShieldBuff
    {
        get => isUsingShieldBuff;
        set => isUsingShieldBuff = value;
    }

    public bool IsUsingTimeBuff
    {
        get => isUsingTimeBuff;
        set => isUsingTimeBuff = value;
    }
    #endregion

    public void SetTimeBuffInit()
    {
        if (!PlayerPrefs.HasKey(BuffType.Speed.ToString()))
        {
            speedTimeBuff 
                = pointTimeBuff 
                = primogemTimeBuff 
                = shieldTimeBuff
                = valueTimeBuff
                = 10;
            timeTimeBuff = 0.5f;

            GameManager.SaveBuffData(BuffType.Speed.ToString(), speedTimeBuff);
            GameManager.SaveBuffData(BuffType.Point.ToString(), pointTimeBuff);
            GameManager.SaveBuffData(BuffType.Primogem.ToString(), primogemTimeBuff);
            GameManager.SaveBuffData(BuffType.Shield.ToString(), shieldTimeBuff);
            GameManager.SaveBuffData(BuffType.Time.ToString(), valueTimeBuff);
        }
        else
        {
            timeTimeBuff = 0.5f;

            speedTimeBuff = GameManager.LoadBuffData(BuffType.Speed.ToString());
            pointTimeBuff = GameManager.LoadBuffData(BuffType.Point.ToString());
            primogemTimeBuff = GameManager.LoadBuffData(BuffType.Primogem.ToString());
            shieldTimeBuff = GameManager.LoadBuffData(BuffType.Shield.ToString());
            valueTimeBuff = GameManager.LoadBuffData(BuffType.Time.ToString());
        }
    }
    public void SpawnBuff(Transform transform)
    {
        var prefab = GetBuffRandom();
        var buff = PoolManager.Instance.GetObjFromTrunk(
            prefab.PoolType, transform.position, transform)
            .GetComponent<Buff>();

        buff.SetInitialPosY();
    }

    public void SpawnBuffFromTrunk(TrunkController trunkController)
    {
        Trunk[] trunks = trunkController.GetTrunkRandom();
        var randomIndex = Random.Range(0, trunks.Length);

        SpawnBuff(trunks[randomIndex].transform);
    }

    private Buff GetBuffRandom()
    {
        int randomIndex = Random.Range(0, buffListData.buffs.Length);
        return buffListData.buffs[randomIndex];
    }
    public float GetBuffTimeWithType(BuffType buffType)
    {
        switch (buffType)
        {
            case BuffType.Speed:
                return speedTimeBuff;
            case BuffType.Point:
                return pointTimeBuff;
            case BuffType.Primogem:
                return primogemTimeBuff;
            case BuffType.Shield:
                return shieldTimeBuff;
            case BuffType.Time:
                return timeTimeBuff;
            default:
                return 0f;
        }
    }

    public float GetBuffValueWithType(BuffType buffType)
    {
        switch (buffType)
        {
            case BuffType.Time:
                return valueTimeBuff;
            default:
                return 0f;
        }
    }
}
