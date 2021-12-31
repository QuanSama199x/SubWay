using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController Instance;
    public Animator animatorPlayer;
    public const string DANCE = "Dance";
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }

    public void AnimationSetBool(string animation,bool set)
    {
        animatorPlayer.SetBool(animation, set);
    }
}
