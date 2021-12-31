using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPoolSFX : MonoBehaviour
{
    public static ObjectPoolSFX Instance;

    private void Awake()
    {
        Instance = this;
    }

    public PoolSFXElement SFXgetCoin;
    void Start()
    {
        SFXgetCoin.startSFX(Resources.Load<AudioSource>("SFX/SoundgetCoin"));
    } 
}

[System.Serializable]
public class PoolSFXElement
{
    public List<AudioSource> PooledSFX;
    public int amountToPoolSFX;

    public void startSFX(AudioSource x)
    {
        for (int i = 0; i < amountToPoolSFX; i++)
        {
            AudioSource obj = x;
            var obj1 =AudioSource.Instantiate(obj);
            obj1.Stop();
            PooledSFX.Add(obj1);
        }
    }

    public AudioSource GetPooledSFX(AudioSource x, Vector3 position)
    {
        for (int i = 0; i < PooledSFX.Count; i++)
        {
            if (!PooledSFX[i].isPlaying)
            {
                PooledSFX[i].transform.position = position;
                return PooledSFX[i];
            }
        }
        AudioSource obj = x;
        var obj1 = AudioSource.Instantiate(obj);
        obj1.Stop();
        PooledSFX.Add(obj1);
        obj1.transform.position = position;      
        return obj1;
    }
}