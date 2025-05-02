using System.Collections.Generic;
using System;
using UnityEngine;
using Spine;

[Serializable]
public class Pool
{
    public PoolType poolType;
    public GameObject prefab;
    public int initialSize;
}

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] private List<Pool> trunkPools;
    [SerializeField] private List<Pool> objFromTrunkPools;
    [SerializeField] private List<Pool> objPools;
    private Dictionary<PoolType, List<GameObject>> poolDictionary;
    private Dictionary<PoolType, GameObject> prefabDictionary;
    private Dictionary<PoolType, Transform> poolParents;

    protected override void Awake()
    {
        base.Awake();
        InitializePools();

    }
    private void InitializePools()
    {
        poolDictionary = new Dictionary<PoolType, List<GameObject>>();
        prefabDictionary = new Dictionary<PoolType, GameObject>();
        poolParents = new Dictionary<PoolType, Transform>();

        InitialTrunkPools();
        InitialObjFromTrunkPools();
        InitialObjPools();
    }

    public Transform PoolParent(PoolType poolType)
    {
        if (poolParents.ContainsKey(poolType))
            return poolParents[poolType];
        else
            return null;
    }


    #region Trunk
    private void InitialTrunkPools()
    {
        foreach (Pool trunkPool in trunkPools)
        {
            prefabDictionary[trunkPool.poolType] = trunkPool.prefab;

            List<GameObject> objectPool = new List<GameObject>();
            poolDictionary[trunkPool.poolType] = objectPool;

            for (int i = 0; i < trunkPool.initialSize; i++)
            {
                CreateNewTrunkInPool(trunkPool.poolType);
            }
        }
    }
    public TrunkController GetTrunk(PoolType poolType, Vector2 pos)
    {
        List<GameObject> pool = poolDictionary[poolType];
        GameObject obj = pool.Find(o => !o.activeInHierarchy);

        if (obj != null) pool.Remove(obj);
        else obj = CreateNewTrunkInPool(poolType);
        if (obj == null) obj = CreateNewTrunkInPool(poolType);

        obj.transform.position = pos;
        obj.SetActive(true);

        return obj.GetComponent<TrunkController>();
    }

    private GameObject CreateNewTrunkInPool(PoolType type)
    {
        GameObject obj = Instantiate(prefabDictionary[type], 
            TrunkManager.Instance.TrunkHolder.transform);
        obj.SetActive(false);
        poolDictionary[type].Add(obj);
        return obj;
    }

    public void ReturnTrunkObj(GameObject obj, PoolType type)
    {
        obj.SetActive(false);
        obj.transform.SetParent(
            TrunkManager.Instance.TrunkHolder.transform);
        poolDictionary[type].Add(obj);
    }
    #endregion

    #region Obj From Trunk

    private void InitialObjFromTrunkPools()
    {
        foreach (Pool pool in objFromTrunkPools)
        {
            GameObject poolParent = new GameObject($"Pool_{pool.poolType}");
            poolParent.transform.SetParent(transform);
            poolParents[pool.poolType] = poolParent.transform;
            prefabDictionary[pool.poolType] = pool.prefab;

            List<GameObject> objectPool = new List<GameObject>();
            poolDictionary[pool.poolType] = objectPool;

            for (int i = 0; i < pool.initialSize; i++)
            {
                CreateNewObjFromTrunkInPool(pool.poolType);
            }
        }
    }
    public GameObject GetObjFromTrunk(PoolType poolType, Vector2 pos, Transform parent)
    {
        List<GameObject> pool = poolDictionary[poolType];
        GameObject obj = pool.Find(o => !o.activeInHierarchy);

        if (obj != null) pool.Remove(obj);
        else obj = CreateNewObjFromTrunkInPool(poolType);
        if (obj == null) obj = CreateNewObjFromTrunkInPool(poolType);

        obj.SetActive(true);
        obj.transform.SetParent(parent);
        obj.transform.position = pos;

        return obj;
    }

    private GameObject CreateNewObjFromTrunkInPool(PoolType type)
    {
        GameObject obj = Instantiate(prefabDictionary[type], poolParents[type]);
        obj.SetActive(false);
        poolDictionary[type].Add(obj);
        return obj;
    }

    public void ReturnObjFromTrunk(GameObject obj, PoolType type)
    {
        obj.SetActive(false);
        obj.transform.SetParent(poolParents[type]);
        poolDictionary[type].Add(obj);
    }
    #endregion

    #region Obj
    private void InitialObjPools()
    {
        foreach (Pool pool in objPools)
        {
            GameObject poolParent = new GameObject($"Pool_{pool.poolType}");
            poolParent.transform.SetParent(transform);
            poolParents[pool.poolType] = poolParent.transform;
            prefabDictionary[pool.poolType] = pool.prefab;

            List<GameObject> objectPool = new List<GameObject>();
            poolDictionary[pool.poolType] = objectPool;

            for (int i = 0; i < pool.initialSize; i++)
            {
                CreateNewObj(pool.poolType);
            }
        }
    }
    public GameObject GetObj(PoolType poolType, Vector2 pos, Transform transform = null)
    {
        List<GameObject> pool = poolDictionary[poolType];
        GameObject obj = pool.Find(o => !o.activeInHierarchy);

        if (obj != null) pool.Remove(obj);
        else obj = CreateNewObj(poolType);
        if (obj == null) obj = CreateNewObj(poolType);

        obj.SetActive(true);
        
        if(!transform) obj.transform.SetParent(poolParents[poolType]);
        else obj.transform.SetParent(transform);

        obj.transform.position = pos;

        return obj;
    }

    private GameObject CreateNewObj(PoolType type)
    {
        GameObject obj = Instantiate(prefabDictionary[type], poolParents[type]);
        obj.SetActive(false);
        poolDictionary[type].Add(obj);
        return obj;
    }

    public void ReturnObj(GameObject obj, PoolType type)
    {
        obj.SetActive(false);
        obj.transform.SetParent(poolParents[type]);
        poolDictionary[type].Add(obj);
    }
    #endregion
}
