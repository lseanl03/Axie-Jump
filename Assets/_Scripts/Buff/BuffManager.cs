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

    private BuffListData buffListData;

    protected override void Awake()
    {
        base.Awake();
        buffListData = Resources.Load<BuffListData>("SOData/Buff/BuffListData");
    }

    #region Get Set
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
}
