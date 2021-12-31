using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom : MonoBehaviour
{
    public static Custom Instance;

    public GameObject shoeLeft, shoeRight, magnet, Shield;
    public float timeGetItem;
    public int maxTimeGetItem = 15;

    public float timeUseShield;
    public int maxTimeUseShield = 20;

    public bool isGetShoes, isGetMagnet;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        magnet.SetActive(false);
        shoeLeft.SetActive(false);
        shoeRight.SetActive(false);
    }

    public void GetShoes()
    {
        shoeLeft.SetActive(true);
        shoeRight.SetActive(true);
        SwipeManager.Instance.jumpPower = SwipeManager.Instance.maxJumpPower;
        timeGetItem += Time.deltaTime;
        RecTransformUI.Instance.shoes.SetActive(true);
        if (timeGetItem >= maxTimeGetItem)
        {
            RecTransformUI.Instance.shoes.SetActive(false);
            SwipeManager.Instance.jumpPower = SwipeManager.Instance.minJumpPower;
            isGetShoes = false;
            timeGetItem = 0;
            shoeLeft.SetActive(false);
            shoeRight.SetActive(false);
        }
    }
    public void GetMagnet()
    {
        magnet.SetActive(true);
        timeGetItem += Time.deltaTime;
        RecTransformUI.Instance.magnet.SetActive(true);
        if (timeGetItem >= maxTimeGetItem)
        {
            RecTransformUI.Instance.magnet.SetActive(false);
            isGetMagnet = false;
            timeGetItem = 0;
            magnet.SetActive(false);
        }
    }
    public void GetShield()
    {
        timeUseShield += Time.deltaTime;
        RecTransformUI.Instance.timeUseShield.fillAmount = (float)timeUseShield / maxTimeUseShield;
        Shield.SetActive(true);
        if (timeUseShield >= maxTimeUseShield)
        {
            RecTransformUI.Instance.haveShield = false;
            PlayerScript.Instance.isHaveShield = false;
        }

    }
    public void GetRocket()
    {
        GetComponent<Rigidbody>().useGravity = false;
        timeGetItem += Time.deltaTime;              
        if (timeGetItem >= maxTimeGetItem)
        {
            GetComponent<Rigidbody>().useGravity = true;
            PlayerScript.Instance.isGetRocket = false;
            timeGetItem = 0;
        }
    }
    public IEnumerator up()
    {
        while(transform.position.y < 20)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            transform.position += new Vector3(0, 0.1f, 0);
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 5.22f, transform.position.z - 4.73f);
        }
    }
    public void UnGetShield()
    {
        Shield.SetActive(false);
        timeUseShield = 0;
    }
    
}
