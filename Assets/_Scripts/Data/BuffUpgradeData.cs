using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffUpgradeData", menuName = "ScriptableObjects/BuffUpgradeData", order = 1)]
public class BuffUpgradeData : ScriptableObject
{
    public BuffUpgradeConfig[] buffUpgradeConfigs;
}

[Serializable]
public class BuffUpgradeConfig
{
    public string buffName;
    public BuffType buffType;
    public Sprite buffImage;
    public int startPrice;
}
