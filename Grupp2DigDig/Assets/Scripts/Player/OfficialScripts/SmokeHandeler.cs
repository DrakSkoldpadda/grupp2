using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeHandeler : MonoBehaviour
{
    public ParticleSystem rightLegSmoke;
    public ParticleSystem leftLegSmoke;

    public AudioSource stepljud;

    public void RightSmoke()
    {
        rightLegSmoke.Play();
        //Debug.Log("Right SMOOK");


    }

    public void LeftSmoke()
    {
        leftLegSmoke.Play();
        //Debug.Log("Left SMOOK");
    }
}
