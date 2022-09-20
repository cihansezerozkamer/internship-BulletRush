using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleton<ObjectPool>
{  
    [Serializable]
    public  struct Pool
    {
        public Queue<GameObject> poolObjects;
        public GameObject objectPrefab;
        public int poolSize;
    }
    [SerializeField] private Pool[] pools = null;
    private void Awake()
    {
        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].poolObjects = new Queue<GameObject>();
            for (int i = 0; i < pools[j].poolSize; i++)
            {
                GameObject obj = Instantiate(pools[j].objectPrefab);
                obj.SetActive(false);
                pools[j].poolObjects.Enqueue(obj);
            }
        }
    }
    
    
    public GameObject GetPoolObject(int objectType)
    {
        if(objectType >= pools.Length)
        {
            return null;
        }
        GameObject obj = pools[objectType].poolObjects.Dequeue();
        obj.SetActive(true);
        pools[objectType].poolObjects.Enqueue(obj);
        return obj;
    }
    public int GetPoolSize(int objectType)
    {
        return pools[objectType].poolSize;
    }
    public void DeactivePool()
    {
       foreach (var pool in pools)
        {
           foreach(var obj in pool.poolObjects)
            {
                obj.SetActive(false);
            }
        }
    }
 }
  

