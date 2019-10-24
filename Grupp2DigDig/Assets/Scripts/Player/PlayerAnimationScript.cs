using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody playerBody;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        playerAnimator.SetFloat("conditionToRun", playerBody.velocity.z);
    }
}
