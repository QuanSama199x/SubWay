using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    private static Config instance;
    public static Config Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<Config>();
            return instance;
        }
    }
    public int Coin;
    public int scaleCoin = 1;
    public float Score;
    public int basicScore=11;
    public int scaleScore;

    void Start()
    {
        EventScript.Instance.OnGetCoin.AddListener(AddCoin);
        EventScript.Instance.OnGetCoin.AddListener(AddScore);
        EventScript.Instance.LvUp.AddListener(Config.Instance.UpScaleCore);
        scaleScore = 1;
    }

    void Update()
    {
        Score += (float)Time.deltaTime * basicScore * scaleScore;
    }


    public void UpScaleCore()
    {
        scaleScore += 1;
    }
    public void AddCoin()
    {
        Coin += scaleCoin;
        RecTransformUI.Instance.TextCoin();
    }
    public void AddScore()
    {
        Score += basicScore * scaleScore;
    }
}
