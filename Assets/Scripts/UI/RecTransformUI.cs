using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class RecTransformUI : MonoBehaviour
{
    public static RecTransformUI Instance;

    public float scaleX;
    public float scaleY;
    private void Awake()
    {
        Instance = this;
    }

    #region isPlaying
    public int Score0,Score1, Score2, Score3, Score4, Score5,scaleScore;
    public Text textScore;
    public Text textScaleScore;
    public RectTransform rectextScore;

    public RectTransform recCompleteTable;

    public Text textCoin;
    public Text textCoinGameOver;
    public RectTransform rectextCoin;

    public RectTransform reccountdown;
    public Image countdownItem;
    public GameObject magnet;
    public GameObject shoes;

    public RectTransform recPause;
    public RectTransform recPauseTable;
    public RectTransform recSetting;

    public RectTransform recIconShield;
    public GameObject iconShield;
    public bool haveShield;
    public GameObject buttonBuyShield;
    public RectTransform recbuttonBuyShield;
    public float timebuttonBuyShield;
    public Image timeUseShield;
    public GameObject buttonShield;

    public RectTransform recbuttonBuyx2Coin;
    public GameObject buttonBuyx2Coin;
    #endregion

    #region isMenu

    public RectTransform recSettingM;
    public RectTransform rectabtoPlay;
    public RectTransform recSelectChar;
    public RectTransform recTableChar;
    public RectTransform recDataCoin;
    public RectTransform rectableHighScore;
    public RectTransform recbuttonHighScore;
    public RectTransform rectableBuyChar;
    public RectTransform buttonReset;

    public Text textDataCoin;
    public RectTransform text;
    private float  y,x;
    
    public List<Text> highscore;
    

    #endregion
    
    public Text textScoreGameOver;
    public int ScoreGameOver;
    public int CoinGameOver;
    public GameObject NewRecord;
    public GameObject medal;
    public Text countMedal;

    public float abc =0.1f;



    void Start()
    {
        EventScript.Instance.OnGetCoin.AddListener(TextCoin);
        EventScript.Instance.isPlay.AddListener(IsPlay);

        buttonBuyShield.SetActive(true);
        haveShield = false;
        scaleX = (float)Screen.width / 480;
        scaleY = (float)Screen.height / 800;

        y = 0;
        x = 0;
        scaleScore = 1;
        
        magnet.SetActive(false);
        shoes.SetActive(false);
        
        RecTransform(rectextScore);
        RecTransform(recCompleteTable);
        RecTransform(rectextCoin);
        RecTransform(reccountdown);
        RecTransform(recPause);
        RecTransform(recPauseTable);
        RecTransform(recSetting);
        RecTransform(recSettingM);
        RecTransform(rectabtoPlay);
        RecTransform(recSelectChar);
        RecTransform(recDataCoin);
        RecTransform(rectableHighScore);
        RecTransform(recbuttonHighScore);
        RecTransform(buttonReset);
        RecTransform(rectableBuyChar);
        RecTransform(recIconShield);
        RecTransform(recbuttonBuyShield);
        RecTransform(recbuttonBuyx2Coin);
        StartCoroutine(CD());
        StartCoroutine(Order());

        rectableHighScore.localScale = new Vector2(0, 0);
        rectableBuyChar.localScale = new Vector2(0, 0);
        recSetting.localScale = new Vector2(0, 0);
        recCompleteTable.localScale = new Vector2(0, 0);
        TextDataCoin();
    }
    IEnumerator CD()
    {
        yield return new WaitForSecondsRealtime(4);
        StartCoroutine(CD());
        abc = -abc;
    }
    private bool up1,up2;
    void Update()
    {
        text.Rotate(0,0,0.3f);
        text.localPosition = new Vector2(x, y);        
            y += abc*2;     
            x += abc;
        countdownItem.fillAmount = (float) Custom.Instance.timeGetItem / Custom.Instance.maxTimeGetItem;
        if (haveShield)
        {
            iconShield.SetActive(true);
        }
        else
        {
            iconShield.SetActive(false);
        }
        TextScore();
    }
    
    public void TextScore()
    {
        float Score = Config.Instance.Score;
        textScaleScore.text = scaleScore.ToString();
        textScore.text = String.Format("{0,0 :D6}", (int)Score).ToString();
    }
    public void TextCoin()
    {
        textCoin.text = Config.Instance.Coin.ToString();
    }   
    public void TextDataCoin()
    {
        textDataCoin.text = SaveData.Instance.DataCoin.ToString();
    }

    public void RecTransform(RectTransform getTransform)
    {
        getTransform.transform.localPosition = new Vector2(getTransform.transform.localPosition.x*scaleX,getTransform.transform.localPosition.y*scaleY);
        getTransform.localScale = new Vector2(scaleX,scaleX);
    }

    public void IsPlay()
    {
        StartCoroutine(Order2());
    }
    IEnumerator Translate(RectTransform rectransform, int position)
    {
        while(rectransform.localPosition.x > position * scaleX)
        {
            yield return new WaitForSecondsRealtime(0.001f);
            rectransform.localPosition = new Vector2(rectransform.localPosition.x - scaleX * 30, rectransform.localPosition.y);
        }
        rectransform.localPosition = new Vector2(position * scaleX, rectransform.localPosition.y);
    }
    IEnumerator Order()
    {
        yield return new WaitForSecondsRealtime(1);
        yield return Translate(recSettingM, 190);
        yield return Translate(recbuttonHighScore, 190);
        yield return Translate(buttonReset, 190);
        yield return Translate(recSelectChar, 190);
        yield return Translate(recDataCoin, -130);
    }
    IEnumerator Order2()
    {
        StartCoroutine(Translate(recbuttonBuyx2Coin, -150));
        yield return Translate(recbuttonBuyShield, -150);
        yield return Translate(recPause, -200);
        yield return Translate(rectextScore, (int)115.62);
        yield return Translate(rectextCoin, (int)147.37);
        yield return new WaitForSecondsRealtime(3);
        StartCoroutine(Translate(recbuttonBuyShield, -350));
        StartCoroutine(Translate(recbuttonBuyx2Coin, -350));
        yield return new WaitForSecondsRealtime(1);
        buttonBuyx2Coin.SetActive(false);
        buttonBuyShield.SetActive(false);
    }
    public IEnumerator OpenTable(RectTransform Table,float Scale)
    {
        while(Table.localScale.x < Scale)
        {
            yield return new WaitForSecondsRealtime(0.001f);
            Table.localScale = new Vector2(Table.localScale.x + 0.2f, Table.localScale.y + 0.2f);
        }  
    }

    public IEnumerator CloseTable(RectTransform Table, float Scale)
    {
        while(Table.localScale.x > Scale)
        {
            yield return new WaitForSecondsRealtime(0.001f);
            Table.localScale = new Vector2(Table.localScale.x - 0.2f, Table.localScale.y - 0.2f);
        }
        Table.gameObject.SetActive(false);
    }
}


