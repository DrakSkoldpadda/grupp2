using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{

    private Animator playerAnimator;
    private Rigidbody rigBody;
    private TestMovement playMov;
    private float moveVelocity;

    private void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        rigBody = GetComponent<Rigidbody>();
        playMov = GetComponent<TestMovement>();
    }

    private void Update()
    {
        moveVelocity = rigBody.velocity.magnitude;

        if (!playMov.isGrounded)
            playerAnimator.SetBool("Falling", true);
        else
            playerAnimator.SetBool("Falling", false);

    }

    private void FixedUpdate()
    {
        if(playMov.isGrounded)
        playerAnimator.SetFloat("Walk", moveVelocity);



        if(Input.GetButtonDown(playMov.jumpButton))
            playerAnimator.SetBool("Jumping", true);


        if (rigBody.velocity.y > 0.05f)
            playerAnimator.SetBool("Jumping", true);

        else
            playerAnimator.SetBool("Jumping", false);

    }
}
