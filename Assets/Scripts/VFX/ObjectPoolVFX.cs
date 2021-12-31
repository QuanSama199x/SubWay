using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPoolVFX : MonoBehaviour
{
    public static ObjectPoolVFX Instance;

    private void Awake()
    {
        Instance = this;
    }

    public PoolVFXElement VFXgetCoin;
    void Start()
    {
        VFXgetCoin.startVFX(Resources.Load<ParticleSystem>("VFX/VFXgetCoin"));
    }

}

[System.Serializable]
public class PoolVFXElement
{
    public List<ParticleSystem> PooledVFX;
    public int amountToPoolVFX;

    public void startVFX(ParticleSystem x)
    {
        for (int i = 0; i < amountToPoolVFX; i++)
        {
            ParticleSystem obj = x;
            var obj1 =ParticleSystem.Instantiate(obj);
            obj1.Stop();
            PooledVFX.Add(obj1);
        }
    }

    public ParticleSystem GetPooledVFX(ParticleSystem x, Vector3 position)
    {
        for (int i = 0; i < PooledVFX.Count; i++)
        {
            if (!PooledVFX[i].isStopped)
            {
                PooledVFX[i].transform.position = position;
                return PooledVFX[i];
            }
        }
        ParticleSystem obj = x;
        var obj1 = ParticleSystem.Instantiate(obj);
        obj1.Stop();
        PooledVFX.Add(obj1);
        obj.transform.position = position;
        return obj1;
    }
}