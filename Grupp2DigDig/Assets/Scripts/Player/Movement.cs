using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Character Stats")]
    [SerializeField] float walkingSpeed = 6f;
    [SerializeField] float sprintingSpeedMultiplaier = 12f;

    [SerializeField] float jumpForce = 7f;
    [SerializeField] float jumpCooldown = 0.5f;

    [SerializeField] float normaljumpAditinalSpeed;

    [SerializeField] float minRotationSpeed = 5f;
    [SerializeField] float maxRotationSpeed = 10f;

    [Header("Inputs")]
    [SerializeField] string verticalAxis = "Vertical"; // Forward and backwards
    [SerializeField] string horisontalAxis = "Horizontal"; // Left and Right 


    [SerializeField] string jumpButton = "Jump";
    [SerializeField] string sprintButton = "Sprint";


    [Header("Mechanical Stuff")]
    [SerializeField] float neededSpeedToTurn = 3f;
    [SerializeField] float graivityScale = 0.07f;

    [SerializeField] float turnAmmountBeforeLeaning = 0.2f;

    [SerializeField] float currentRotation;
    [SerializeField] float destenatedRotation;



    [Header("LinkableObjects")]
    [SerializeField] Animator animMove;
    [SerializeField] Animator animLean;
    private Transform camFollower;


    private bool isSprinting = false;
    private bool isJumping;
    private float currentSpeed;

    private float cooldownBeforeJump;
    private float currentRotationsSpeed;

    private Quaternion lookRotation;

    private Vector3 moveDirection;
    private Vector3 moveDirectionRaw;


    private CharacterController controller;

    private Vector3 camForward;
    private Vector3 camRight;

    private void Awake()
    {
        camFollower = Camera.main.transform;
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        currentSpeed = walkingSpeed;
    }



    private void FixedUpdate()
    {
        camForward = camFollower.forward;
        camRight = camFollower.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;


        // I seperated the scripts into thier own voids so that it's easyer to look at
        Move();
        RotateMovement();
        //LeanAnimation(); // Work in progress ;3
    }


    void Move()
    {
        cooldownBeforeJump -= Time.deltaTime;

        if (Input.GetButtonDown(sprintButton) && controller.isGrounded)
        {
            currentSpeed *= sprintingSpeedMultiplaier;
        }

        if (Input.GetButtonUp(sprintButton) || isJumping == true)
        {
            currentSpeed = walkingSpeed;
        }


        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * walkingSpeed, moveDirection.y, Input.GetAxis("Vertical") * walkingSpeed);
        moveDirection = new Vector3((camFollower.forward.x * Input.GetAxis(verticalAxis) * currentSpeed + camFollower.right.x * Input.GetAxis(horisontalAxis) * currentSpeed)
            , moveDirection.y
            , (camFollower.forward.z * Input.GetAxis(verticalAxis) * currentSpeed) + (camFollower.right.z * Input.GetAxis(horisontalAxis) * currentSpeed));


        if (controller.isGrounded && cooldownBeforeJump <= 0)
        {
            if (Input.GetButtonDown(jumpButton))
            {
                isJumping = true;
                NormalJump();
                cooldownBeforeJump = jumpCooldown;

            }

        }


        if (!controller.isGrounded)
        {
            isJumping = false;

            //Applies gravity only when you are not on the groundwa
            moveDirection.y = moveDirection.y + (Physics.gravity.y * graivityScale);


        }

        controller.Move(moveDirection * Time.deltaTime);
    }

    void NormalJump()
    {
        for (int i = 0; i < 0; i++)
        {
            moveDirection = new Vector3((camFollower.forward.x * Input.GetAxis(verticalAxis) * normaljumpAditinalSpeed + camFollower.right.x * Input.GetAxis(horisontalAxis) * normaljumpAditinalSpeed)
                , jumpForce
                , (camFollower.forward.z * Input.GetAxis(verticalAxis) * normaljumpAditinalSpeed) + (camFollower.right.z * Input.GetAxis(horisontalAxis) * normaljumpAditinalSpeed));
        }

    }



    private void RotateMovement()
    {
        //moveDirectionRaw = new Vector3(Input.GetAxisRaw("Horizontal") * walkingSpeed, moveDirection.y, Input.GetAxisRaw("Vertical") * walkingSpeed);
        moveDirectionRaw = (camForward * Input.GetAxisRaw(verticalAxis)) + (camRight * Input.GetAxisRaw(horisontalAxis));


        if (moveDirection.x < -neededSpeedToTurn || moveDirection.x > neededSpeedToTurn || moveDirection.z < -neededSpeedToTurn || moveDirection.z > neededSpeedToTurn)// So that it dosen't rotate back to z = 0 when not moving
        {
            if (moveDirectionRaw.x > 0 || moveDirectionRaw.x < 0 || moveDirectionRaw.z > 0 || moveDirectionRaw.z < 0)// Allso so that when I let go of all movement it shouldn't move back
            {
                currentRotationsSpeed = minRotationSpeed;

                // This does not use the Character controller to work
                lookRotation = Quaternion.LookRotation(new Vector3(moveDirectionRaw.x, 0, moveDirectionRaw.z)); // Find out how you shold rotate yourself to look in that direction(ny y direction as well)

                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * currentRotationsSpeed); // Smoothly rotate towoards that direction
            }
        }

        else // Only happenes if player is walking slowely. For a faster turning when standing still.
        {
            if (moveDirectionRaw.x > 0 || moveDirectionRaw.x < 0 || moveDirectionRaw.z > 0 || moveDirectionRaw.z < 0) // Allso so that when I let go of all movement it shouldn't move back
            {
                currentRotationsSpeed = maxRotationSpeed;

                lookRotation = Quaternion.LookRotation(new Vector3(moveDirectionRaw.x, 0, moveDirectionRaw.z)); // Find out how you shold rotate yourself to look in that direction(ny y direction as well)

                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * currentRotationsSpeed); // Smoothly rotate towoards that direction
            }
        }
    }

    private void LeanAnimation()
    {
        if (Input.GetAxis("Horizontal") < -0.3f || Input.GetAxis("Horizontal") > 0.3f || Input.GetAxis("Vertical") < -0.3f || Input.GetAxis("Vertical") > 0.3f)
        {
            animMove.SetBool("CharacterIsMoving", true);

        }
        else
        {
            animMove.SetBool("CharacterIsMoving", false);
        }

        if (controller.velocity.x < 0 || controller.velocity.x > 0)
        {



        }
        else if (controller.velocity.z < 0 || controller.velocity.z > 0)
        {

        }

        //if (transform.rotation.y + 0.9 < lookRotation.y || transform.rotation.y - 0.9 > lookRotation.y)
        //{
        //    lookRotation.y *= -1;
        //}

        if (moveDirectionRaw.x == 1 && lookRotation.y < 0)
        {
            lookRotation.y *= -1;
        }
        else if (transform.rotation.y < -0.7f)
        {
            Debug.Log("TEST");
        }

        // Testing
        if (transform.rotation.y - turnAmmountBeforeLeaning > lookRotation.y)
        {
            animLean.SetBool("LeaningLeft", true);
            animLean.SetBool("LeaningRight", false);
        }
        else if (transform.rotation.y + turnAmmountBeforeLeaning < lookRotation.y)
        {
            animLean.SetBool("LeaningLeft", false);
            animLean.SetBool("LeaningRight", true);
        }
        else if (transform.rotation.y + turnAmmountBeforeLeaning > lookRotation.y && transform.rotation.y - turnAmmountBeforeLeaning < lookRotation.y)
        {
            animLean.SetBool("LeaningLeft", false);
            animLean.SetBool("LeaningRight", false);
        }
        // Testing

        destenatedRotation = lookRotation.y;
        currentRotation = transform.rotation.y;
    }
}
