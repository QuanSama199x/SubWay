using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance;
    public List<bool> boolcheckbuy;
    public List<int> highscore;

    public bool checkplay;
    public int DataCoin = 0;
    public int checkSelectChar;

    public const string CHECK_PLAY = "checkplay";
    public const string CHECK_BUY = "checkbuy";
    public const string CHECK_SELECT_CHAR = "checkselectchar";
    public const string HIGH_SCORE = "highscore";
    public const string COIN = "Coin";

    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        LoadData();
        if (!checkplay)
        {
            DataCoin = 2000;
            checkplay = true;
            PlayerPrefs.SetInt(CHECK_PLAY,checkplay?1:0);
        }
        LoadHighScore();
        Save();
        for (int i = 0; i < highscore.Count; i++)
        {
            RecTransformUI.Instance.highscore[i].text ="No."+(i+1).ToString()+": "+ highscore[i].ToString();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt(COIN,DataCoin);
        for (int i = 1; i < boolcheckbuy.Count; i++)
        {
            PlayerPrefs.SetInt(CHECK_BUY+i,boolcheckbuy[i]?1:0);
        }
        PlayerPrefs.SetInt(CHECK_SELECT_CHAR, checkSelectChar);
        PlayerPrefs.Save();
    }

    public void SaveHighScore()
    {
        for (int i = 0; i < highscore.Count; i++)
        {
            PlayerPrefs.SetInt(HIGH_SCORE+i,highscore[i]);
        }
    }

    public void LoadHighScore()
    {
        for (int i = 0; i < highscore.Count; i++)
        {
            highscore[i] =PlayerPrefs.GetInt(HIGH_SCORE+i);
        }
    }
    public void LoadData()
    {
        if (PlayerPrefs.GetInt(CHECK_PLAY)==1)
        {
            checkplay = true;
        }
        DataCoin = PlayerPrefs.GetInt(COIN);
        PlayerPrefs.SetInt(COIN,DataCoin);
        for (int i = 1; i < boolcheckbuy.Count; i++)
        {
            if (PlayerPrefs.GetInt(CHECK_BUY+i)==1)
            {
                boolcheckbuy[i] = true;
                ButtonScript.Instance.Character[i].checkBuy.SetActive(false);
            }
            else
            {
                ButtonScript.Instance.Character[i].checkBuy.SetActive(true);
                boolcheckbuy[i] = false;
            }                        
        }
        checkSelectChar = PlayerPrefs.GetInt(CHECK_SELECT_CHAR);
        ButtonScript.Instance.ChooseCharacters(checkSelectChar);
                
    }

    public void ResetData()
    {
        checkplay = false;
        PlayerPrefs.SetInt(CHECK_PLAY,checkplay?1:0);
        DataCoin = 0;
        checkSelectChar = 0;
        for (int i = 0; i < boolcheckbuy.Count; i++)
        {
            boolcheckbuy[i] = false;
            PlayerPrefs.SetInt(CHECK_BUY+i,boolcheckbuy[i]?1:0);
        }

        for (int i = 0; i < highscore.Count; i++)
        {
            highscore[i] = 0;
        }
        Save();
        SaveHighScore();
    }
}
