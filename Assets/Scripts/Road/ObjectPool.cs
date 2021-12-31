using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;


public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    void Awake() {
        Instance = this;
    }

    public PoolElement poolMagnet, poolShoes;
    public PoolElement poolCoin;
    public List<PoolElement> PoolBlocker;
    public List<PoolElement> poolTrain;

    public PoolElement PoolRoadDemo;
    
        
    private void Start()
    {
        PoolRoadDemo.Start(Resources.Load<GameObject>("Road/RoadDemo"));
        poolMagnet.Start(Resources.Load<GameObject>("Item/magnet")); 
        poolShoes.Start(Resources.Load<GameObject>("Item/Shoes"));
        poolCoin.Start(Resources.Load<GameObject>("Item/Coin"));
        for(int i=0;i<PoolBlocker.Count;i++)
        {
            PoolBlocker[i].Start(Resources.Load<GameObject>("Blocker/Block"+(i+1)));
        }
        for (int i = 0; i < poolTrain.Count; i++)
        {
            poolTrain[i].Start(Resources.Load<GameObject>("Train/Train"+(i+1)));
        }
        SpawnRoadScript.Instance.isStart();
        
    }
}

[System.Serializable]
public class PoolElement
{
    public List<GameObject> PooledObjects; 
    public int amountToPool;

    public void Start(GameObject x)
    {
        for (int i = 0; i < amountToPool; i++) {
           
            GameObject obj = x;
            var obj1 = GameObject.Instantiate(obj); 
            obj1.SetActive(false); 
            PooledObjects.Add(obj1);
        }
    }
 

    public GameObject GetPooledObject(GameObject x)
    {
        
        for (int i = 0; i < PooledObjects.Count; i++) {
            if (!PooledObjects[i].activeInHierarchy) {
                return PooledObjects[i];
                
            }
        }
        GameObject obj = x;
        var obj1 = GameObject.Instantiate(obj);
        obj1.SetActive(false);
        PooledObjects.Add(obj1);
        return obj1;
    }
}