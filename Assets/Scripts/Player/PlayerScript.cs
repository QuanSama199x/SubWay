using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerScript : MonoBehaviour
{
    #region properties

    public static PlayerScript Instance;
    public const string DANCE = "Dance";
    public const string IS_STUN = "isStun";
    public const string IS_JUMP = "isJump";
    public const string IS_ROLL = "isRoll";
    public const string BLOCK1 = "Block1";
    public const string BLOCK2 = "Block2";
    public const string BLOCK3 = "Block3";
    public const string BLOCK_TRAIN = "Шапка_1_9";
    public const string BLOCKER = "Blocker";
    public const string NON_BLOCK = "nonBlocker";
    public const string COIN = "Coin";
    public const string MAGNET = "magnet";
    public const string SHOES = "Shoes";
    public const string ROAD = "Road";
    public const string TRAIN = "Train";

    private Vector3 viewPoint;

    public bool isroll;

    public float timeStun;
    public int maxTimeStun=4;
    /*[Header(("Const"))]*/
    public float timeIdle;
    public bool idle, dance;
    public float timeDance;
    public int maxTimeIdle = 2, maxTimeDance = 5;

    public bool isHaveShield;
    public GameObject shield;
    public int powerShield;

    public int rangeSuckCoin = 8;

    public int scaleScoreGetCoin = 10;

    public int setDance;

    public bool isGetRocket;

    #endregion
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        shield.SetActive(false);
        isHaveShield = false;
        idle = true;      
        timeStun = 4;
        viewPoint = new Vector3(0, 5.22f, -4.73f);
    }

    void Update()
    {
        InputScript.Instance.swipe();
        if (Time.timeScale == 1)
        {
            InputScript.Instance.InputController();
        }
        if (!ButtonScript.Instance.isPlay)
        {
            if (idle)
            {
                timeIdle += 0.005f;
                if (timeIdle >= maxTimeIdle)
                {
                    idle = false;
                    dance = true;
                    timeIdle = 0;
                    setDance = Random.Range(1, 4);
                    AnimationController.Instance.AnimationSetBool(DANCE+setDance,true);
                }
            }
            if (dance)
            {
                timeDance += 0.005f;
                if (timeDance >= maxTimeDance)
                {
                    idle = true;
                    dance = false;
                    timeDance = 0;
                    AnimationController.Instance.AnimationSetBool(DANCE+setDance,false);
                }
            }
        }


        if (Mathf.Abs(SwipeManager.Instance.emty.transform.position.x - transform.position.x) <= 1)
        {
            SwipeManager.Instance.nonEmty.x = SwipeManager.Instance.emty.transform.position.x;
        }
        SwipeManager.Instance.emty.transform.position = new Vector3(SwipeManager.Instance.emty.transform.position.x, transform.position.y,transform.position.z);

        transform.position = Vector3.Lerp(transform.position,SwipeManager.Instance.emty.transform.position, 5 * Time.deltaTime);
        if (InputScript.Instance.swipeLeft)
        {
            SwipeManager.Instance.moveLeft();
        }

        if (InputScript.Instance.swipeRight)
        {
            SwipeManager.Instance.moveRight();
        }

        if (InputScript.Instance.swipeUp && SwipeManager.Instance.countJump == true)
        {
            SwipeManager.Instance.timeroll = 1;
            SwipeManager.Instance.countJump = false;
            AnimationController.Instance.AnimationSetBool(IS_JUMP, true);
            SwipeManager.Instance.Jump();
        }

        if (InputScript.Instance.swipeDown && SwipeManager.Instance.timeroll == 0)
        {
            PlayerScript.Instance.isroll = true;
            SwipeManager.Instance.Down();
        }

        if (Mathf.Abs(transform.position.x - SwipeManager.Instance.emty.transform.position.x) <= 0.3 && transform.forward != Vector3.zero)
        {
            transform.forward = Vector3.zero;

        }

        if (SwipeManager.Instance.istimeroll)
        {
            SwipeManager.Instance.colliderPlayer.gameObject.transform.localScale = new Vector3(1, 0.5f, 1);
            SwipeManager.Instance.timeroll += Time.deltaTime;

        }
        if (SwipeManager.Instance.timeroll >= 1)
        {
            SwipeManager.Instance.colliderPlayer.gameObject.transform.localScale = new Vector3(1, 1, 1);
            SwipeManager.Instance.timeroll = 0;
            AnimationController.Instance.AnimationSetBool(IS_ROLL, false);
            SwipeManager.Instance.istimeroll = false;
        }

        if (Custom.Instance.timeGetItem >= Custom.Instance.maxTimeGetItem)
        {
            Custom.Instance.timeGetItem = 0;
            Custom.Instance.isGetMagnet = false;
        }
        if (!followPlayer)
        {
            Camera main;
            (main = Camera.main).transform.position = Vector3.Lerp(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y,
                Camera.main.transform.position.z), new Vector3(transform.position.x, viewPoint.y, Camera.main.transform.position.z), 5 * Time.deltaTime);

            main.transform.position = new Vector3(main.transform.position.x, main.transform.position.y,
                transform.position.z - 4.73f);
            main.transform.position = main.transform.position;
        }
        if (transform.position.y < 0)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
        
        if (Custom.Instance.isGetMagnet)
        {
            Custom.Instance.GetMagnet();
        }
        if (Custom.Instance.isGetShoes)
        {
            Custom.Instance.GetShoes();
        }
        if (isStun)
        {
            timeStun -= Time.deltaTime;
            if (timeStun <= 0)
            {
                timeStun = maxTimeStun;
                isStun = false;
                AnimationController.Instance.AnimationSetBool(IS_STUN, false);
            }
        }
        if (isHaveShield)
        {
            Custom.Instance.GetShield();
        }
        if (!isHaveShield)
        {
            Custom.Instance.UnGetShield();
        }

        if(isGetRocket)
        {

            followPlayer = true;
            Custom.Instance.GetRocket();
        }
        if(followPlayer)
        {
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 5.22f, transform.position.z - 4.73f);
        }
    }
    public bool followPlayer;

    public void OnCollisionEnter(Collision other)
    {
        followPlayer =false;
        if (isStun)
        {
            AnimationController.Instance.AnimationSetBool(IS_STUN, true);
        }
        else
        {
            AnimationController.Instance.AnimationSetBool(IS_JUMP, false);
        }
        if (SwipeManager.Instance.isjump && !isroll)
        {
            SwipeManager.Instance.isjump = false;
            AnimationController.Instance.AnimationSetBool(IS_JUMP, false);
        }


    }

    public bool isStun;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == COIN)
        {
            EventScript.Instance.OnGetCoin.Invoke();
            ObjectPoolVFX.Instance.VFXgetCoin.GetPooledVFX(Resources.Load<ParticleSystem>("VFX/VFXgetCoin"),other.transform.position).Play();
            ObjectPoolSFX.Instance.SFXgetCoin.GetPooledSFX(Resources.Load<AudioSource>("SFX/SoundgetCoin"),other.transform.position).Play(); ;
            other.transform.SetParent(null);
            other.transform.position = new Vector3(1000, 1000, 1000);
            other.gameObject.SetActive(false);
        }
        if (isHaveShield)
        {
            switch (other.gameObject.name)
            {
                case BLOCK1:
                    powerShield -= 1;
                    break;
                case BLOCK2:
                    powerShield -= 1;
                    break;
                case BLOCK3:
                    powerShield -= 1;
                    break;
                case BLOCK_TRAIN:
                    other.gameObject.transform.parent.transform.parent.transform.parent.transform.parent.gameObject.SetActive(false);
                    other.gameObject.transform.parent.transform.parent.transform.parent.transform.parent.SetParent(null);
                    powerShield -= 1;
                    break;
                case NON_BLOCK:
                    other.gameObject.transform.parent.gameObject.SetActive(false);
                    other.gameObject.transform.parent.SetParent(null);
                    powerShield -= 1;
                    break;
            }
            if (powerShield <= 0)
            {
                RecTransformUI.Instance.haveShield = false;
                isHaveShield = false;
            }
        }
        else
        {
            switch (other.gameObject.tag)
            {
                case BLOCKER:
                    GamePlayScript.Instance.Gameover();
                    break;
                case NON_BLOCK:
                    other.name = other.GetHashCode().ToString();
                    if (isStun)
                    {
                        GamePlayScript.Instance.Gameover();
                    }
                    else
                    {
                        AnimationController.Instance.AnimationSetBool(IS_STUN, true);
                        SwipeManager.Instance.emty.transform.position = new Vector3(SwipeManager.Instance.nonEmty.x, SwipeManager.Instance.emty.transform.position.y, SwipeManager.Instance.emty.transform.position.z);
                        isStun = true;
                    }
                    break;
            }
        }


        if (other.gameObject.tag == MAGNET)
        {
            Custom.Instance.isGetMagnet = true;
            other.transform.SetParent(null);
            other.transform.position = new Vector3(1000, 1000, 1000);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == SHOES)
        {
            Custom.Instance.isGetShoes = true;
            other.transform.SetParent(null);
            other.transform.position = new Vector3(1000, 1000, 1000);
            other.gameObject.SetActive(false);
        }

    }

    public void OnCollisionStay(Collision other)
    {
        viewPoint = other.gameObject.tag switch
        {
            ROAD => new Vector3(0, 5.22f, -4.73f),
            TRAIN => new Vector3(0, 8.3789f, -4.73f),
            _ => viewPoint
        };

        if (other.gameObject.tag == TRAIN)
        {
            AnimationController.Instance.AnimationSetBool(IS_JUMP, false);
        }
        if (other.gameObject.tag == ROAD || other.gameObject.tag == TRAIN)
        {
            SwipeManager.Instance.countJump = true;

            if (isroll)
            {
                AnimationController.Instance.AnimationSetBool(IS_ROLL, true);
                SwipeManager.Instance.istimeroll = true;
                isroll = false;

            }

        }
    }
    public void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == ROAD || other.gameObject.tag == TRAIN)
        {

            SwipeManager.Instance.countJump = false;
        }
        if (other.gameObject.tag == TRAIN)
        {
            AnimationController.Instance.AnimationSetBool(IS_JUMP, true);
        }
    }
    
}

