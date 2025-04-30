using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffData", menuName = "ScriptableObjects/BuffData", order = 1)]
public class BuffData : ScriptableObject
{
    public BuffConfig buffs;
}

[Serializable]
public class BuffConfig
{
    public string buffName;
    public string description;
    public float valueEffect;
    public float duration;
    public BuffType buffType;
    public Sprite sprite;
}
