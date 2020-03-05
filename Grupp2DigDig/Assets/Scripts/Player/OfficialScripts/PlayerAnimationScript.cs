using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{

    private Animator playerAnim;    
    private OfficialPlayerMovement playerMov;

    public float YVelocity;

    enum PlayerState { Jumping, Falling, Idle, Running}
    PlayerState currentPlayerState;

    private float moveVelocity;

    private void Start()
    {
        currentPlayerState = PlayerState.Idle;
        playerAnim = GetComponentInChildren<Animator>();
        playerMov = GetComponentInParent<OfficialPlayerMovement>();
    }

    private void Update()
    {
        SetAnimationState();
        YVelocity = playerMov.playerVelocity.y;

    }

    void SetAnimationState()
    {
        // If I am not moving forward or backwards or is not grounded. Be idle
        if (Input.GetAxis("Horizontal") == 0.0f && Input.GetAxis("Vertical") == 0.0f && playerMov.controller.isGrounded)
        {
            currentPlayerState = PlayerState.Idle;

            //print("Idle");
        }
        // Else if either horizontal or Vertical is not 0 while grounded. Go Running
        else if ((Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f) && playerMov.controller.isGrounded)
        {
            currentPlayerState = PlayerState.Running;
        }
        // Else if you are not grounded and is flying upwards. Jump Up
        else if (!playerMov.controller.isGrounded && playerMov.playerVelocity.y > 0)
        {
            currentPlayerState = PlayerState.Jumping;
        }
        // Else if you are also not grounded and is falling down. The FAAAAAAaAaaa.a....l (Also don't fall while standing still)
        else if (!playerMov.controller.isGrounded && playerMov.playerVelocity.y < -1) 
        {
            currentPlayerState = PlayerState.Falling;
            //print("Falling");
        }

        StartAnimation();

    }
    void StartAnimation()
    {

        if(currentPlayerState == PlayerState.Idle)
        {
            playerAnim.SetInteger("CurrentState", 0);
        }
        else if (currentPlayerState == PlayerState.Running)
        {
            playerAnim.SetInteger("CurrentState", 1);
        }
        else if (currentPlayerState == PlayerState.Jumping)
        {
            playerAnim.SetInteger("CurrentState", 2);
        }
        else if (currentPlayerState == PlayerState.Falling)
        {
            playerAnim.SetInteger("CurrentState", 3);
        }
    }

}
