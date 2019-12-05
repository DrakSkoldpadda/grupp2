using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InTheDarkMeater : MonoBehaviour
{
    [SerializeField] private RespawnScript death;
    [SerializeField] private Slider darkMeater;
    [SerializeField] private float darkMeaterMultiplyer = 1f;

    public bool condition = false;
    float cooldown = 1f;

    // Update is called once per frame
    void Update()
    {
        if (darkMeater.value > 0)
        {
            if (condition)
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