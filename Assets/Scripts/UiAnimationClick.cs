using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiAnimationClick : MonoBehaviour
{
    public GameObject who;
    public void AnimationClick(string name)
    {
        Animator anim = who.GetComponent<Animator>();
        anim.Play(name);
    }
}
