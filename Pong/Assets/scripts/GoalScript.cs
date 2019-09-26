using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public static GoalScript instance;

    [SerializeField]
    private bool thisIsPlayerOne = false;


    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ball" && thisIsPlayerOne == true)
        {
            ScoreCounterScript.instance.ScoreUpdateTwo();
        }

        else if (collision.tag == "ball" && thisIsPlayerOne == false)
        {
            ScoreCounterScript.instance.ScoreUpdateOne();
        }
    }
}