using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int speedCoin = 20;
    void Start()
    {
        transform.forward = new Vector3(0, 1, 0);
    }

    void FixedUpdate()
    {
        transform.Rotate(0,0,5);

        if (Custom.Instance.isGetMagnet)
        {
            getMagnet();
        }
        if (gameObject.activeInHierarchy&& gameObject.transform.parent ==null)
        {
            Vector3 dir = PlayerScript.Instance.transform.position - transform.position;
            transform.position += dir.normalized * Time.deltaTime * speedCoin;
        }
    }
    
    public void getMagnet()
    {
        if (Vector3.Distance(PlayerScript.Instance.transform.position, transform.position) <= PlayerScript.Instance.rangeSuckCoin)
        {
            transform.SetParent(null);
        }    
    }

    public void OnDisable()
    {
        transform.forward = new Vector3(0,1,0);
    }
}
