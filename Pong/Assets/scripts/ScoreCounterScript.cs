using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreCounterScript : MonoBehaviour
{
    public static ScoreCounterScript instance;

    public int playerOneScore;
    public int playerTwoScore;
    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI scoreText2;
    public GameObject ball;
    public GameObject resetLocation;

    public Animator winAnimator;

    private SoundScript audioManager;
    public string winSound;




    private void Start()
    {
        if (instance == null)
            instance = this;

        StartCoroutine(ResetBall());

        audioManager = SoundScript.instance;
    }


    public void ScoreUpdateOne()
    {
        playerOneScore++;
        scoreText1.text = "" + playerOneScore + "";
        if (playerOneScore >= 7)
        {
            audioManager.PlaySound(winSound);
            winAnimator.SetTrigger("playerOneWins");
            return;
        }

        else StartCoroutine(ResetBall());

    }

    public void ScoreUpdateTwo()
    {
        playerTwoScore++;
        scoreText2.text = "" + playerTwoScore + "";
        if (playerTwoScore >= 7)
        {
            audioManager.PlaySound(winSound);
            winAnimator.SetTrigger("playerTwoWins");
            return;
        }

        else StartCoroutine(ResetBall());
    }

    IEnumerator ResetBall()
    {
        yield return new WaitForSeconds(2);

        ball.SetActive(true);
    }
}
