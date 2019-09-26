using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallScript : MonoBehaviour
{
    public float dice;
    bool wallGoes;
    private Animator animator;
    bool checking;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (ScoreCounterScript.instance.playerOneScore != ScoreCounterScript.instance.playerTwoScore)
        {
            animator.SetBool("walls", false);
            checking = false;
        }

        if (ScoreCounterScript.instance.playerOneScore == ScoreCounterScript.instance.playerTwoScore &&
            ScoreCounterScript.instance.playerOneScore != 0 && checking == false)
        {
            checking = true;
            RollTheDie();
            if (wallGoes == true)
            {
                animator.SetBool("walls", true);
            }
        }

    }

    void RollTheDie()
    {
        dice = Random.Range(1, 6);

        if (dice <= 1)
        {
            wallGoes = true;
        }
    }
}
