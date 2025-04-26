using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] private Transform poolContainer;
    
    // Pools for different object types
    private ObjectPool<TrunkController> trunkPool;
    private ObjectPool<Item> itemPool;
    
    private Dictionary<TrunkType, ObjectPool<TrunkController>> trunkTypePools = new Dictionary<TrunkType, ObjectPool<TrunkController>>();
    
    protected override void Awake()
    {
        base.Awake();
        if (!poolContainer)
        {
            poolContainer = new GameObject("Pool Container").transform;
            poolContainer.SetParent(transform);
        }
    }
    
    public void InitializePools(TrunkController[] trunkPrefabs, Item itemPrefab)
    {
        // Create item pool
        Transform itemContainer = new GameObject("Item Pool").transform;
        itemContainer.SetParent(poolContainer);
        itemPool = new ObjectPool<Item>(itemPrefab, 20, itemContainer);
        
        // Create trunk pools by type
        Transform trunkContainer = new GameObject("Trunk Pool").transform;
        trunkContainer.SetParent(poolContainer);
        
        foreach (var trunkPrefab in trunkPrefabs)
        {
            if (!trunkTypePools.ContainsKey(trunkPrefab.TrunkType))
            {
                Transform typeContainer = new GameObject($"Type_{trunkPrefab.TrunkType}").transform;
                typeContainer.SetParent(trunkContainer);
                
                // Create pool for this trunk type with appropriate initial size
                trunkTypePools.Add(trunkPrefab.TrunkType, 
                    new ObjectPool<TrunkController>(trunkPrefab, 3, typeContainer));
            }
        }
    }
    
    public TrunkController GetTrunk(TrunkType trunkType)
    {
        if (trunkTypePools.ContainsKey(trunkType))
        {
            return trunkTypePools[trunkType].Get();
        }
        return null;
    }
    
    public void ReleaseTrunk(TrunkController trunk)
    {
        if (trunkTypePools.ContainsKey(trunk.TrunkType))
        {
            trunkTypePools[trunk.TrunkType].Release(trunk);
        }
    }
    
    public Item GetItem()
    {
        return itemPool.Get();
    }
    
    public void ReleaseItem(Item item)
    {
        itemPool.Release(item);
    }
}
