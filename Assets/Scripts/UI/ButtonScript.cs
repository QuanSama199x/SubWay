using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    #region properties
    public static ButtonScript Instance;

    public AudioSource tab;
    
    public GameObject isPlaying;
    public GameObject isMenu;
    public GameObject isComplete;
    public GameObject buttonPause;
    public GameObject pauseTable;
    public GameObject SettingTable;
    public GameObject TableChar;
    public GameObject tableBuyChar;
    public GameObject CDCoutinue;
    public Text textCoutinue;

    public Scrollbar volume;

    public GameObject tableHighScore;
    
    public AudioSource AudioBG;

    public Image selectChar;

    public GameObject Player;
    public CharacterScript character;
    public List<GameObject> CHARACTER;
    public List<Char> Character;

    public List<CharacterData> CharacterData;
    public List<GameObject> CharEnable;


    public int priceShield=100, pricex2Coin =200;
    #endregion
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

        isPlaying.SetActive(false);
        isComplete.SetActive(false);
        isMenu.SetActive(true);
        pauseTable.SetActive(false);
        SettingTable.SetActive(false);
        TableChar.SetActive(false);

        volume.value = 1;

    }

    // Update is called once per frame
    void Update()
    {
        AudioBG.volume=volume.value;
    }
    #region method
    public void ButtonNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SoundTab();
    }

    public void ButtonPause()
    {
        InputScript.Instance.Reset();
        Time.timeScale = 0;
        pauseTable.SetActive(true);
        SoundTab();

    }

    public bool isPlay;
    public void ButtonPlay()
    {
        EventScript.Instance.isPlay.Invoke();
        isPlay = true;
        Time.timeScale = 1;
        isMenu.SetActive(false);
        isPlaying.SetActive(transform);
        AnimationController.Instance.AnimationSetBool("isPlay", true);
        AnimationController.Instance.animatorPlayer.updateMode = AnimatorUpdateMode.Normal;
        SoundTab();
    }
    public void ButtonContinue()
    {
        InputScript.Instance.Reset();
        pauseTable.SetActive(false);
        StartCoroutine(CDTimeContinue());
        SoundTab();
    }
    IEnumerator CDTimeContinue()
    {
        buttonPause.SetActive(false);
        CDCoutinue.SetActive(true);
        textCoutinue.text = 3.ToString();
        yield return new WaitForSecondsRealtime(1);
        textCoutinue.text = 2.ToString();
        yield return new WaitForSecondsRealtime(1);
        textCoutinue.text = 1.ToString();
        yield return new WaitForSecondsRealtime(1);
        CDCoutinue.SetActive(false);
        buttonPause.SetActive(true);
        Time.timeScale = 1;

    }

    public void ButtonMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SoundTab();
    }

    public void ButtonSetting()
    {
        SettingTable.SetActive(true);
        StartCoroutine(RecTransformUI.Instance.OpenTable(RecTransformUI.Instance.recSetting, RecTransformUI.Instance.scaleX));
        SoundTab();
    }

    public void ButtonXSetting()
    {
        StartCoroutine(RecTransformUI.Instance.CloseTable(RecTransformUI.Instance.recSetting, 0));
        SoundTab();
    }

    public void buttonUseShield()
    {
        PlayerScript.Instance.isHaveShield = true;
        PlayerScript.Instance.powerShield = 3;
        RecTransformUI.Instance.buttonShield.SetActive(false);
        SoundTab();
    }

    public void buttonBuyShield()
    {
        if (SaveData.Instance.DataCoin>=priceShield)
        {
            RecTransformUI.Instance.buttonBuyShield.SetActive(false);
            RecTransformUI.Instance.haveShield = true;
            SaveData.Instance.DataCoin -= priceShield;
            RecTransformUI.Instance.TextDataCoin();
            SaveData.Instance.Save();
        }
        SoundTab();
    }

    public void buttonBuyx2Coin()
    {
        if (SaveData.Instance.DataCoin>=pricex2Coin)
        {
            Config.Instance.scaleCoin = 2;
            RecTransformUI.Instance.buttonBuyx2Coin.SetActive(false);
            SaveData.Instance.DataCoin -= pricex2Coin;
            RecTransformUI.Instance.TextDataCoin();
            SaveData.Instance.Save();
        }
        SoundTab();
    }

    #region Char
    public void SelectChar()
    {
        if (TableChar.activeInHierarchy)
        {
            StartCoroutine(RecTransformUI.Instance.CloseTable(RecTransformUI.Instance.recTableChar, 0));
        }
        else
        {
            TableChar.SetActive(true);
            StartCoroutine(RecTransformUI.Instance.OpenTable(RecTransformUI.Instance.recTableChar, 1));
        }
        SoundTab();
    }

    public void ChooseCharacters(int count)
    {
        StartCoroutine(RecTransformUI.Instance.CloseTable(RecTransformUI.Instance.recTableChar, 0));
        selectChar.sprite = Character[count].iconChar.sprite;
        AnimationController.Instance.animatorPlayer.avatar = Character[count].avtChar;
        Custom.Instance.shoeLeft.transform.SetParent(Character[count].left.transform);
        Custom.Instance.shoeRight.transform.SetParent(Character[count].right.transform);
        Custom.Instance.magnet.transform.SetParent(Character[count].hand.transform);
        SaveData.Instance.checkSelectChar = count;
        for (int i = 0; i < Character.Count; i++)
        {
            if (i==count)
            {
                Character[i].setChar.SetActive(true);
            }
            else
            {
                Character[i].setChar.SetActive(false);
            }
        }
        SaveData.Instance.Save();
        SoundTab();
    }

    public void ButtonBlockChar(int count)
    {
        Character[count].buyChar.SetActive(true);

        tableBuyChar.SetActive(true);
        StartCoroutine(RecTransformUI.Instance.OpenTable(RecTransformUI.Instance.rectableBuyChar, RecTransformUI.Instance.scaleX));
        SoundTab();
    }
   
    public void BuyCharacters( int count)
    {
        
       
        if (SaveData.Instance.DataCoin>=Character[count].price)
        {
            SaveData.Instance.DataCoin -= Character[count].price;
            Character[count].checkBuy.SetActive(false);
            SaveData.Instance.boolcheckbuy[count] = true;
            SaveData.Instance.Save();        
        }

            StartCoroutine(RecTransformUI.Instance.CloseTable(RecTransformUI.Instance.rectableBuyChar, 0));
            Character[count].buyChar.SetActive(false);
            SoundTab();
    }
   
    public void buttonXBuyChar()
    {
        StartCoroutine(RecTransformUI.Instance.CloseTable(RecTransformUI.Instance.rectableBuyChar, 0));
        for (int i = 1; i < Character.Count; i++)
        {
            Character[i].buyChar.SetActive(false);        }
        SoundTab();
    }
    #endregion


    public void buttonopenHighScore()
    {
        tableHighScore.SetActive(true);
        StartCoroutine(RecTransformUI.Instance.OpenTable(RecTransformUI.Instance.rectableHighScore, RecTransformUI.Instance.scaleX));
        SoundTab();
    }
    
    public void buttoncloseHighScore()
    {
        StartCoroutine(RecTransformUI.Instance.CloseTable(RecTransformUI.Instance.rectableHighScore, 0));
        SoundTab();
    }

    public void SoundTab()
    {
        tab.Play();
    }

    public void resetData()
    {
        SaveData.Instance.ResetData();
        SoundTab();
    }
    #endregion
}

[System.Serializable]
public class Char
{
    public GameObject left, right, hand,setChar,checkBuy,buyChar;
    public Image iconChar;
    public Avatar avtChar;
    public int price;
}
