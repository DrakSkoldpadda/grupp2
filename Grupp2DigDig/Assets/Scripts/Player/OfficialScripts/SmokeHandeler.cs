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
        PlayStep();
    }

    public void LeftSmoke()
    {
        leftLegSmoke.Play();
        //Debug.Log("Left SMOOK");
        PlayStep();
    }

    void PlayStep()
    {

        stepljud.pitch = 1 * Random.Range(0.7f, 1.2f);
        stepljud.volume = 0.5f * Random.Range(0.1f, 1.2f);
        stepljud.Play();
    }
}
