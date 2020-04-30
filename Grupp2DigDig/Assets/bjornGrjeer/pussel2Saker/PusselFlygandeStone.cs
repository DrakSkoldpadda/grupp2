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
        ljud = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        anim.SetBool("ner", true);
    }

    public void GoUp()
    {
        anim.SetBool("ner", false);
    }

    public void GoDown()
    {
        anim.SetBool("ner", true);

    }

    public bool spelaLjud;

    private void FixedUpdate()
    {
        if (spelaLjud)
            ljud.Play();
    }

}
