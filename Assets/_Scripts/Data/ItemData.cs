using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public ItemConfig[] normalItems;
    public ItemConfig[] rareItems;
    public ItemConfig specialItem;
}

[Serializable]
public class ItemConfig
{
    public ItemType itemType;
    public Rate rate;
    public Sprite sprite;
}
