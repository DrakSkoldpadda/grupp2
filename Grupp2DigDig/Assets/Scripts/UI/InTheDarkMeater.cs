using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InTheDarkMeater : MonoBehaviour
{
    [SerializeField] private RespawnScript death;
    [SerializeField] private Slider darkMeater;
    [SerializeField] private float darkMeaterMultiplyer = 1f;

    public int lightRanges = 0;

    float cooldown = 1f;

    // Update is called once per frame
    void Update()
    {
        if (lightRanges < 0)
            lightRanges = 0;



        if (darkMeater.value > 0)
        {
            if (lightRanges <= 0)
            {
                darkMeater.value -= Time.deltaTime * darkMeaterMultiplyer;

                cooldown = 1f;
            }
            else
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0f)
                {
                    darkMeater.value += Time.deltaTime * darkMeaterMultiplyer * 2f;
                }
            }


        }
        if (darkMeater.value <= 0)
        {
            print("You died");

            death.Death();
        }
    }
}