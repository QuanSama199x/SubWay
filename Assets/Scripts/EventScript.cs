using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EventScript : MonoBehaviour
{
    private static EventScript instance;
    public static EventScript Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<EventScript>();
            return instance;
        }
    }
    public UnityEvent OnGetCoin;
    public UnityEvent LvUp;
    public UnityEvent isPlay;
    public UnityEvent GameOver;

    private void Awake()
    {
        LvUp = new UnityEvent();
        OnGetCoin = new UnityEvent();
        isPlay = new UnityEvent();
        GameOver = new UnityEvent();
    }
}
