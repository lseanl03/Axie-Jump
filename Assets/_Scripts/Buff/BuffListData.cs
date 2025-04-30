using UnityEngine;

[CreateAssetMenu(fileName = "BuffListData", menuName = "ScriptableObjects/BuffListData", order = 1)]
public class BuffListData : ScriptableObject
{
    public Buff[] buffs;
}
