
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GamePlayScript : MonoBehaviour
{
    public static GamePlayScript Instance;
    public GameObject Player;
    public float timeDeath;

    public float timePlay;
    public int  frameRate =60;

    public void Awake()
    {
        Application.targetFrameRate = frameRate;
        Instance = this;

    }

    public float MovingSpeed;
    public float timeSpawnItem;
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        timeSpawnItem = 0;
        Screen.orientation = ScreenOrientation.Portrait;
        MovingSpeed = 10;
        timePlay = 0;
        Time.timeScale = 0;
        timeDeath = 0;
        level = 1;
    }

    void Update()
    {
        timeSpawnItem += Time.deltaTime;
        timePlay += Time.deltaTime;
        if (timePlay >= 50)
        {
            timePlay = 0;
            MovingSpeed += 2;
            EventScript.Instance.LvUp.Invoke();
            level++;
            if (level > 4)
            {
                level = 4;
            }
        }

        if (isDeath)
        {
            if (Player.transform.position.y > 0)
            {
                Player.transform.position += new Vector3(0, -0.05f, -0);
            }

            if (Player.transform.position.z >= -3)
            {
                Player.transform.position += new Vector3(0, 0, -0.5f);
            }
        }

    }

    private bool isDeath;
    public void Gameover()
    {
        Time.timeScale = 0f;
        AnimationController.Instance.animatorPlayer.updateMode = AnimatorUpdateMode.UnscaledTime;
        Player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 5);
        ButtonScript.Instance.isPlaying.SetActive(false);
        if (Random.Range(0, 2) == 1)
        {
            AnimationController.Instance.AnimationSetBool("isDeath1", true);
        }
        else
        {
            AnimationController.Instance.AnimationSetBool("isDeath2", true);
        }
        isDeath = true;
        StartCoroutine(CDDeath());
        SaveData.Instance.DataCoin += Config.Instance.Coin;
        getHighScore();
        SaveData.Instance.Save();

    
    }
    IEnumerator CDDeath()
    {
        yield return new WaitForSecondsRealtime(3);
        ButtonScript.Instance.isComplete.SetActive(true);
        yield return RecTransformUI.Instance.OpenTable(RecTransformUI.Instance.recCompleteTable, RecTransformUI.Instance.scaleX*0.8f);
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(CDupScore());
    }
    IEnumerator CDupScore()
    {while(true)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            if (RecTransformUI.Instance.ScoreGameOver < (int)Config.Instance.Score)
            {
                RecTransformUI.Instance.ScoreGameOver += (int)((int)Config.Instance.Score - RecTransformUI.Instance.ScoreGameOver) * 3 / 100 + 1;
                RecTransformUI.Instance.textScoreGameOver.text = RecTransformUI.Instance.ScoreGameOver.ToString();
            }
            else
            {
                yield return new WaitForSecondsRealtime(0.5f);
                yield return CDupCoin();
                Debug.LogError("Vjpp");
            }
        }   
    }
    IEnumerator CDupCoin()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            if (RecTransformUI.Instance.CoinGameOver < Config.Instance.Coin)
            {
                RecTransformUI.Instance.CoinGameOver += (int)((int)Config.Instance.Coin - RecTransformUI.Instance.CoinGameOver) * 3 / 100 + 1;
                RecTransformUI.Instance.textCoinGameOver.text = RecTransformUI.Instance.CoinGameOver.ToString();
                yield return (CDupCoin());
            }
            else
            {
                EventScript.Instance.GameOver.Invoke();
            }   
        }
    }
    public void getHighScore()
    {
        for (int i = 0; i < SaveData.Instance.highscore.Count; i++)
        {
            if ((int)Config.Instance.Score > SaveData.Instance.highscore[i])
            {
                RecTransformUI.Instance.NewRecord.SetActive(true);
                RecTransformUI.Instance.medal.SetActive(true);
                RecTransformUI.Instance.countMedal.text = (i + 1).ToString();
                for (int j = 4; j > i; j--)
                {
                    SaveData.Instance.highscore[j] = SaveData.Instance.highscore[j - 1];
                }
                SaveData.Instance.highscore[i] = (int)Config.Instance.Score;
                SaveData.Instance.SaveHighScore();
                return;
            }
        }
    }

}
