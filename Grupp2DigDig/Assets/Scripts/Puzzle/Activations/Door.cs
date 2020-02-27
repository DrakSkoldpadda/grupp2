using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Activated
{
    private Animator anim;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    override public void Activate()
    {
        anim.SetBool("Open", true);
    }

    public override void DeActivate()
    {
        anim.SetBool("Open", false);
    }
}