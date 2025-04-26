using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private T prefab;
    private Transform parent;
    private Queue<T> pool = new Queue<T>();
    private List<T> activeObjects = new List<T>();
    
    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
        
        // Initialize pool with objects
        for (int i = 0; i < initialSize; i++)
        {
            AddObjectToPool();
        }
    }
    
    private T AddObjectToPool()
    {
        T obj = UnityEngine.Object.Instantiate(prefab, parent);
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
        return obj;
    }
    
    public T Get()
    {
        T obj;
        if (pool.Count == 0)
        {
            obj = AddObjectToPool();
        }
        else
        {
            obj = pool.Dequeue();
        }
        
        obj.gameObject.SetActive(true);
        activeObjects.Add(obj);
        return obj;
    }
    
    public void Release(T obj)
    {
        if (activeObjects.Contains(obj))
        {
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
            activeObjects.Remove(obj);
        }
    }
    
    public void ReleaseAll()
    {
        while (activeObjects.Count > 0)
        {
            Release(activeObjects[0]);
        }
    }
}
