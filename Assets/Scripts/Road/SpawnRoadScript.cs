using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnRoadScript : MonoBehaviour
{
    public static int key;
    private int LVSpawnTrain;
    public static SpawnRoadScript Instance;

    public string ROAD = "Road/RoadDemo";

    private void Awake()
    {
        Instance = this;
    }

    public List<GameObject> listRoad;

    public void isStart()
    {
        EventScript.Instance.LvUp.AddListener(UpLVSpawnRoad);
        key = Random.Range(1, 5);
        spawnroad(ROAD, new Vector3(0, -0.5f, 100));
        key = Random.Range(1, 5);
        spawnroad(ROAD, new Vector3(0, -0.5f, 200));
        key = Random.Range(1, 5);
        spawnroad(ROAD, new Vector3(0, -0.5f, 300));

    }

    void Update()
    {
        if (listRoad.Count < 4)
        {
            SpawnRoad(new Vector3(listRoad[2].transform.position.x, listRoad[2].transform.position.y, listRoad[2].transform.position.z + 100));
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Road")
        {
            listRoad.Remove(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin" || other.gameObject.tag == "magnet" || other.gameObject.tag == "Shoes")
        {
            other.transform.SetParent(null);
            other.transform.position = new Vector3(1000, 1000, 1000);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Train")
        {
            other.transform.parent.gameObject.SetActive(false);
            other.transform.parent.gameObject.transform.SetParent(null);
            other.transform.parent.gameObject.transform.position = new Vector3(1000, 1000, 1000);
        }
        if (other.gameObject.name == "Block1(Clone)" || other.gameObject.name == "Block2(Clone)" || other.gameObject.name == "Block3(Clone)")
        {
            other.transform.SetParent(null);
            other.transform.position = new Vector3(1000, 1000, 1000);
            other.gameObject.SetActive(false);
        }
    }

    public void UpLVSpawnRoad()
    {
        LVSpawnTrain += 2;
        if (LVSpawnTrain > 6)
        {
            LVSpawnTrain = 6;
        }
    }

    public void SpawnRoad(Vector3 transformnewroad)
    {
        key = LVSpawnTrain + Random.Range(1, 5);
        spawnroad(ROAD, transformnewroad);
    }

    public void spawnroad(string ROAD, Vector3 transformnewroad)
    {
        GameObject road = ObjectPool.Instance.PoolRoadDemo.GetPooledObject(Resources.Load<GameObject>(ROAD));
        if (road != null)
        {
            road.SetActive(true);
            road.transform.position = transformnewroad;
            listRoad.Add(road);
        }
    }


    
}
