using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    [Header("Character Stats")]
    [SerializeField] float walkingSpeed;
    [SerializeField] float sprintingSpeed;

    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;

    [SerializeField] float minRotationSpeed;
    [SerializeField] float maxRotationSpeed;



    [Header("Mechanical Stuff")]
    [SerializeField] float neededSpeedToTurn;
    [SerializeField] float graivityScale;

    [SerializeField] float turnAmmountBeforeLeaning;


    [SerializeField] Animator animMove;
    [SerializeField] Animator animLean;

    [Range(0.01f, 1f)] public float time;

    private float cooldownBeforeJump;
    private float currentRotationsSpeed;


    private Quaternion lookRotation;

    private Vector3 moveDirection;
    private Vector3 moveDirectionRaw;

    [SerializeField] float currentRotation;
    [SerializeField] float destenatedRotation;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    private void Update()
    {
        cooldownBeforeJump -= Time.deltaTime;

        Time.timeScale = time;

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
    }


    private void FixedUpdate()
    {
        // I seperated the scripts into thier own voids so that it's easyer to look at
        Moving();
        RotateTowoards();
    }


    void Moving()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * walkingSpeed, moveDirection.y, Input.GetAxis("Vertical") * walkingSpeed);

        if (controller.isGrounded && cooldownBeforeJump <= 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
                cooldownBeforeJump = jumpCooldown;
            }
        }

        if (!controller.isGrounded)
        {
            moveDirection.y = moveDirection.y + (Physics.gravity.y * graivityScale);
            //Applies gravity only when you are not on the groundwa
        }

        //controller.Move(moveDirection * Time.deltaTime);
    }


    private void RotateTowoards()
    {
        moveDirectionRaw = new Vector3(Input.GetAxisRaw("Horizontal") * walkingSpeed, moveDirection.y, Input.GetAxisRaw("Vertical") * walkingSpeed);


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

        //else if (moveDirectionRaw.x == 0 && moveDirectionRaw.z == 0) // Makes so that when ther is no input tehere is no rotation left for leaning
        //{
        //    lookRotation = transform.rotation;
        //}

        else // Only happenes if player is walking slowely. For a faster turning when standing still.
        {
            if (moveDirectionRaw.x > 0 || moveDirectionRaw.x < 0 || moveDirectionRaw.z > 0 || moveDirectionRaw.z < 0) // Allso so that when I let go of all movement it shouldn't move back
            {
                currentRotationsSpeed = maxRotationSpeed;

                lookRotation = Quaternion.LookRotation(new Vector3(moveDirectionRaw.x, 0, moveDirectionRaw.z)); // Find out how you shold rotate yourself to look in that direction(ny y direction as well)
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * currentRotationsSpeed); // Smoothly rotate towoards that direction
            }
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
