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
        if (inAction == false)
        {
            anim.Play("up");
            StartCoroutine(WaitforClear());
        }
    }

    public void GoDown()
    {
        if (inAction == false)
        {
            anim.Play("ner");
            StartCoroutine(WaitforClear());
        }
    }

    bool inAction;
    IEnumerator WaitforClear()
    {
        inAction = true;
        yield return new WaitForSeconds(6f);
        inAction = false;
    }
}
