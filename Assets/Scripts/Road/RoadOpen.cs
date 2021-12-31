using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadOpen : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position = new Vector3(0, 0, transform.position.z);
        transform.Translate(new Vector3(0,0,-1)*GamePlayScript.Instance.MovingSpeed*Time.deltaTime);
    }
}
