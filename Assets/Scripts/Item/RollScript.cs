using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollScript : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(0 ,3,0);
    }
}
