using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class PusselFlygandeStone : MonoBehaviour
{

    private Animator anim;
    private AudioSource ljud;

    private void Start()
    {
        anim = GetComponent<Animator>();
        ljud = GetComponent<AudioSource>();
    }

    public void GoUp()
    {
        ljud.Play();
        anim.Play("up");
    }

    public void GoDown()
    {
        ljud.Play();
        anim.Play("ner");
    }

}
