using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    [Header("Character Properties")]
    [SerializeField] private float walkingSpeed = 6f;
    [SerializeField] private float straifingSpeedMultiply = 2;
    private Vector3 currentStrafingSpeed;
    private Vector3 maxStrafingSpeed;

    [SerializeField] private float sprintingSpeedMultiplaier = 1.2f;
    [SerializeField] private float decelerationSpeed;

    bool isSprinting = false;
    bool isJumping;
    private float currentSpeed;

    public bool isGrounded;

    public float velocityForStunned;
    private bool stunned = false;
    public float stunnedDuration = 1f;

    [Header("")]
    [SerializeField] private float sprintJumpForce = 4f;
    [SerializeField] float jumpUpForce = 7f;
    [SerializeField] float jumpForwardForce;
    [SerializeField] float sprintJumpMultiplayier = 1.3f;
    [SerializeField] private float jumpCooldown = 0.5f;


    private float cooldownBeforeJump;
    private float currentRotationsSpeed;

    [Header("")]
    [SerializeField] private float minRotationSpeed = 5f;
    [SerializeField] private float maxRotationSpeed = 10f;


    [Header("Inputs")]

    [SerializeField] private string verticalAxis = "Vertical"; // Forward and backwards
    [SerializeField] private string horisontalAxis = "Horizontal"; // Left and Right 

    [SerializeField] public string jumpButton = "Jump";
    [SerializeField] private string sprintButton = "Sprint";

    [SerializeField] private float xInputValue;
    [SerializeField] private float zInputValue;


    [Header("Mechanical Stuff")]
    [SerializeField] private float neededSpeedToTurn = 3f;
    [SerializeField] private float graivityScale = 0.07f;

    [SerializeField] private float turnAmmountBeforeLeaning = 0.2f;

    [Header("")]
    [SerializeField] private float currentRotation;
    [SerializeField] private float destenatedRotation;

    [SerializeField] float currentVelocity;



    [Header("Player Sphare Cast Components")]
    public GameObject currentHitObject;

    public float sphareRadius;
    public float maxDistance;
    public LayerMask layerMask;

    private Vector3 origin;
    private Vector3 direction;

    private float currentHitDistance;



    [Header("LinkableObjects")]


    [SerializeField] CapsuleCollider playerCol;
    [SerializeField] BoxCollider feetCol;
    private Rigidbody rigBody;


    private Transform camFollower;

    private Quaternion lookRotation;

    public Vector3 moveDirection;
    private Vector3 moveDirectionRawRotate;

    private Vector3 maximumValecity;

    //private CharacterController controller;

    private Vector3 camForward;
    private Vector3 camRight;

    enum PlayerMovingState { standingStill, walking, running, unGrounded }
    [Header("States")]
    [SerializeField] PlayerMovingState currentMovingState;

    private void Awake()
    {
        camFollower = Camera.main.transform;
        //controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        currentSpeed = walkingSpeed;
        rigBody = GetComponent<Rigidbody>();
        isGrounded = false;

    }

    private void Update()
    {
        CheckIsGrounded();

        xInputValue = Input.GetAxis(horisontalAxis);
        zInputValue = Input.GetAxis(verticalAxis);

        Move();

    }

    private void FixedUpdate()
    {
        camForward = camFollower.forward;
        camRight = camFollower.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;


        currentVelocity = rigBody.velocity.magnitude;

        // I seperated the scripts into thier own voids so that it's easyer to look at
        if (currentMovingState == PlayerMovingState.unGrounded)
            FixedAirMove();
        else
            FixedMove();




        RotateMovement();
        //LeanAnimation(); // Work in progress ;3
    }

    void CheckIsGrounded()
    {
        // This code sends out a sphare to check if it tuches ground or not.
        origin = transform.position;
        direction = transform.TransformDirection(-Vector3.up);
        RaycastHit hit;

        if (Physics.SphereCast(origin, sphareRadius, direction, out hit, maxDistance, layerMask) && !isGrounded)
        {
            isGrounded = true;
            if (currentVelocity >= velocityForStunned)
                StartCoroutine(Stunnded());
        }
        else if (Physics.SphereCast(origin, sphareRadius, direction, out hit, maxDistance, layerMask))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;

            isGrounded = true;

            Debug.DrawRay(transform.position, direction * hit.distance, Color.green);
        }
        else
        {
            currentHitDistance = maxDistance;
            currentHitObject = null;
            isGrounded = false;

            Debug.DrawRay(transform.position, direction * maxDistance, Color.red);

        }
    }
    private void OnDrawGizmos()
    {
        if (isGrounded)
            Gizmos.color = Color.green;

        else if (!isGrounded)
            Gizmos.color = Color.red;


        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphareRadius);
    } // Draw Is grounded ball to visulize.

    void Move()
    {
        cooldownBeforeJump -= Time.deltaTime;


        if (Input.GetButtonDown(sprintButton) && isGrounded) // If Sprint is pressed AND is grounded
            isSprinting = true;
        else if (Input.GetButtonUp(sprintButton)) // Else if Sprint is released OR is not grounded
            isSprinting = false;



        if (!isGrounded)
        {

            // Is Not Moving at all. Is standing still
            if (xInputValue == 0 && zInputValue == 0)
            {
                currentMovingState = PlayerMovingState.standingStill;
            }
            else // If you are moving!
            {
                // If you hold any move direction, has released sprint or is not grounded
                if (!isSprinting && currentMovingState != PlayerMovingState.walking)
                {
                    currentMovingState = PlayerMovingState.walking;
                    currentSpeed = walkingSpeed;
                }

                // If you hold Sprint, is grounded and where not allready sprinting. You Start Sprinting.
                else if (isSprinting && currentMovingState == PlayerMovingState.walking)
                {
                    currentMovingState = PlayerMovingState.running;
                    currentSpeed *= sprintingSpeedMultiplaier;
                }
            }// If you are moving!
        }
        else
        {

            isJumping = false;
        }


        // Are you grounded AND able to jump? THEN JUMP!
        if (isGrounded && cooldownBeforeJump <= 0)
        {
            if (Input.GetButtonDown(jumpButton))
            {

                isJumping = true;
                if (currentMovingState == PlayerMovingState.standingStill)
                {

                    NormalJump();
                    Debug.Log("Normal jump");
                }
                else if (currentMovingState == PlayerMovingState.walking)
                {
                    WalkJump();
                    Debug.Log("Walk jump");
                }
                else if (currentMovingState == PlayerMovingState.running)
                {
                    LongJump();
                    Debug.Log("Long jump");
                }

                cooldownBeforeJump = jumpCooldown;

            }

        }


    }

    void FixedAirMove()
    {

        moveDirection = new Vector3((camFollower.forward.x * zInputValue + camFollower.right.x * xInputValue)
            , 0.0f
            , (camFollower.forward.z * zInputValue) + (camFollower.right.z * xInputValue));

        maxStrafingSpeed = new Vector3(moveDirection.x * straifingSpeedMultiply, rigBody.velocity.y, moveDirection.z * straifingSpeedMultiply);

        rigBody.velocity = new Vector3(maxStrafingSpeed.x * Time.deltaTime, maxStrafingSpeed.x, maxStrafingSpeed.y * Time.deltaTime);

        if(maxStrafingSpeed.magnitude > straifingSpeedMultiply)
        {
            maxStrafingSpeed = rigBody.velocity.normalized * straifingSpeedMultiply;
        }



    }

    void FixedMove()
    {



        if ((currentMovingState == PlayerMovingState.walking || currentMovingState == PlayerMovingState.running) && !stunned)
        {

            moveDirection = new Vector3((camFollower.forward.x * zInputValue + camFollower.right.x * xInputValue), 0.0f, (camFollower.forward.z * zInputValue) + (camFollower.right.z * xInputValue));

            rigBody.velocity = new Vector3(moveDirection.x * currentSpeed / Time.deltaTime, rigBody.velocity.y, moveDirection.z * currentSpeed / Time.deltaTime);





        }// If you are not standing still. Well then... Um.. move?
        else if (currentMovingState == PlayerMovingState.standingStill && isGrounded)
        {
            rigBody.velocity = rigBody.velocity * decelerationSpeed;
        } // IF you are grounded and not moving. Decelerate AND STOP THAT MOVING!.


        // Limits my moving speed. So that the character dosen't accelerate to the third dimension you know?
        if (rigBody.velocity.magnitude > currentSpeed && !isJumping)
        {

            rigBody.velocity = rigBody.velocity.normalized * currentSpeed;

        }


    }



    void NormalJump()
    {
        rigBody.AddForce(transform.up * jumpUpForce, ForceMode.Impulse);
    }

    void WalkJump()
    {
        rigBody.AddForce(Vector3.up * jumpUpForce, ForceMode.Impulse);
        rigBody.AddForce(transform.forward * -jumpUpForce / 2, ForceMode.Impulse);
    }

    void LongJump()
    {
        rigBody.AddForce((Vector3.up * sprintJumpForce), ForceMode.Impulse);
        rigBody.AddForce((transform.forward * sprintJumpForce), ForceMode.Impulse);
    }

    IEnumerator Stunnded()
    {
        stunned = true;
        yield return new WaitForSeconds(stunnedDuration);
        stunned = false;
    }

    private void RotateMovement()
    {
        //moveDirectionRaw = new Vector3(Input.GetAxisRaw("Horizontal") * walkingSpeed, moveDirection.y, Input.GetAxisRaw("Vertical") * walkingSpeed);
        moveDirectionRawRotate = (camForward * Input.GetAxisRaw(verticalAxis)) + (camRight * Input.GetAxisRaw(horisontalAxis));

        // If the player moves slower then half of the walking speed. He turns faster then normal. 
        if (rigBody.velocity.magnitude < walkingSpeed / 2)
        {
            currentRotationsSpeed = maxRotationSpeed;

        }
        else
        {
            currentRotationsSpeed = minRotationSpeed;
        }



        if (moveDirection.x < -neededSpeedToTurn || moveDirection.x > neededSpeedToTurn || moveDirection.z < -neededSpeedToTurn || moveDirection.z > neededSpeedToTurn)// So that it dosen't rotate back to z = 0 when not moving
        {
            if ((moveDirectionRawRotate.x > 0 || moveDirectionRawRotate.x < 0 || moveDirectionRawRotate.z > 0 || moveDirectionRawRotate.z < 0) && isGrounded)// Allso so that when I let go of all movement it shouldn't move back
            {

                // This does not use the Character controller to work
                lookRotation = Quaternion.LookRotation(new Vector3(moveDirectionRawRotate.x, 0, moveDirectionRawRotate.z)); // Find out how you shold rotate yourself to look in that direction(ny y direction as well)

                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * currentRotationsSpeed); // Smoothly rotate towoards that direction
            }
        }

        else
        {
            if ((moveDirectionRawRotate.x > 0 || moveDirectionRawRotate.x < 0 || moveDirectionRawRotate.z > 0 || moveDirectionRawRotate.z < 0) && isGrounded) // Allso so that when I let go of all movement it shouldn't move back
            {

                lookRotation = Quaternion.LookRotation(new Vector3(moveDirectionRawRotate.x, 0, moveDirectionRawRotate.z)); // Find out how you shold rotate yourself to look in that direction(ny y direction as well)

                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * currentRotationsSpeed); // Smoothly rotate towoards that direction
            }
        }
    }

    //private void LeanAnimation()
    //{
    //    if (Input.GetAxis("Horizontal") < -0.3f || Input.GetAxis("Horizontal") > 0.3f || Input.GetAxis("Vertical") < -0.3f || Input.GetAxis("Vertical") > 0.3f)
    //    {
    //        animMove.SetBool("CharacterIsMoving", true);

    //    }
    //    else
    //    {
    //        animMove.SetBool("CharacterIsMoving", false);
    //    }

    //    if (controller.velocity.x < 0 || controller.velocity.x > 0)
    //    {



    //    }
    //    else if (controller.velocity.z < 0 || controller.velocity.z > 0)
    //    {

    //    }

    //    //if (transform.rotation.y + 0.9 < lookRotation.y || transform.rotation.y - 0.9 > lookRotation.y)
    //    //{
    //    //    lookRotation.y *= -1;
    //    //}

    //    if (moveDirectionRaw.x == 1 && lookRotation.y < 0)
    //    {
    //        lookRotation.y *= -1;
    //    }
    //    else if (transform.rotation.y < -0.7f)
    //    {
    //        Debug.Log("TEST");
    //    }

    //    // Testing
    //    if (transform.rotation.y - turnAmmountBeforeLeaning > lookRotation.y)
    //    {
    //        animLean.SetBool("LeaningLeft", true);
    //        animLean.SetBool("LeaningRight", false);
    //    }
    //    else if (transform.rotation.y + turnAmmountBeforeLeaning < lookRotation.y)
    //    {
    //        animLean.SetBool("LeaningLeft", false);
    //        animLean.SetBool("LeaningRight", true);
    //    }
    //    else if (transform.rotation.y + turnAmmountBeforeLeaning > lookRotation.y && transform.rotation.y - turnAmmountBeforeLeaning < lookRotation.y)
    //    {
    //        animLean.SetBool("LeaningLeft", false);
    //        animLean.SetBool("LeaningRight", false);
    //    }
    //    // Testing

    //    destenatedRotation = lookRotation.y;
    //    currentRotation = transform.rotation.y;
    //}
}
