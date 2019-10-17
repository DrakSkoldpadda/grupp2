using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    [Header("Character Stats")]
    [SerializeField] private float walkingSpeed = 6f;
    [SerializeField] private float sprintingSpeedMultiplaier = 12f;

    [SerializeField] bool isSprinting = false;
    [SerializeField] bool isJumping;
    private float currentSpeed;
    [SerializeField] bool isGrounded;

    [Header("")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float jumpCooldown = 0.5f;
    [SerializeField] float jumpForwardForce;

    [SerializeField] private float normaljumpAditinalSpeed;

    private float cooldownBeforeJump;
    private float currentRotationsSpeed;

    [Header("")]
    [SerializeField] private float minRotationSpeed = 5f;
    [SerializeField] private float maxRotationSpeed = 10f;


    [Header("Inputs")]
    [SerializeField] private string verticalAxis = "Vertical"; // Forward and backwards
    [SerializeField] private string horisontalAxis = "Horizontal"; // Left and Right 

    [SerializeField] private string jumpButton = "Jump";
    [SerializeField] private string sprintButton = "Sprint";


    [Header("Mechanical Stuff")]
    [SerializeField] private float neededSpeedToTurn = 3f;
    [SerializeField] private float graivityScale = 0.07f;

    [SerializeField] private float turnAmmountBeforeLeaning = 0.2f;

    [Header("")]
    [SerializeField] private float currentRotation;
    [SerializeField] private float destenatedRotation;

    [Header("Player Sphare Cast Components")]
    public GameObject currentHitObject;

    public float sphareRadius;
    public float maxDistance;
    public LayerMask layerMask;

    private Vector3 origin;
    private Vector3 direction;

    private float currentHitDistance;



    [Header("LinkableObjects")]
    [SerializeField] private Animator animMove;
    [SerializeField] private Animator animLean;

    [SerializeField] CapsuleCollider playerCol;
    [SerializeField] BoxCollider feetCol;
    private Rigidbody rigBody;


    private Transform camFollower;

    private Quaternion lookRotation;

    private Vector3 moveDirection;
    private Vector3 moveDirectionRaw;

    private Vector3 maximumValecity;

    //private CharacterController controller;

    private Vector3 camForward;
    private Vector3 camRight;


    enum PlayerState { grounded, falling, jumping }
    PlayerState currentYaxisState;

    enum PlayerMovingState { standingStill, walking, running }
    PlayerMovingState currentMovingState;

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


    void CheckIsGrounded()
    {
        // This code sends out a sphare to check if it tuches ground or not.
        origin = transform.position;
        direction = transform.TransformDirection(-Vector3.up);
        RaycastHit hit;

        if (Physics.SphereCast(origin, sphareRadius, direction, out hit, maxDistance, layerMask))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;

            isGrounded = true;

            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * hit.distance, Color.green);
        }
        else
        {
            currentHitDistance = maxDistance;
            currentHitObject = null;
            isGrounded = false;

            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * maxDistance, Color.red);

        }
    }
    private void OnDrawGizmos()
    {
        if (isGrounded)
            Gizmos.color = Color.green;

        else if (!isGrounded)
            Gizmos.color = Color.red;


        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphareRadius);
    }

    void Move()
    {
        cooldownBeforeJump -= Time.deltaTime;
        float fallVelocity = rigBody.velocity.y;

        // Is Sprinting
        if (Input.GetButtonDown(sprintButton) && isGrounded)
        {
            currentMovingState = PlayerMovingState.running;
            currentSpeed *= sprintingSpeedMultiplaier;
        }
        // Is Not Sprinitng
        if (Input.GetButtonUp(sprintButton) || !isGrounded)
        {
            currentMovingState = PlayerMovingState.walking;
            currentSpeed = walkingSpeed;
        }
        // Is Not Moving at all
        if(moveDirection.x == 0 && moveDirection.z == 0)
        {
            currentMovingState = PlayerMovingState.standingStill;
            Debug.Log("Is not moving");
        }

        if (isGrounded)
        {
            isJumping = false;
        }

        // Checks if you are able to Jump. 
        if (isGrounded && cooldownBeforeJump <= 0)
        {
            if (Input.GetButtonDown(jumpButton))
            {
                isJumping = true;
                NormalJump();
                cooldownBeforeJump = jumpCooldown;

            }

        }



        Debug.Log(isJumping);


        Debug.Log("Is able to move");
        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * walkingSpeed, moveDirection.y, Input.GetAxis("Vertical") * walkingSpeed);
        moveDirection = new Vector3((camFollower.forward.x * Input.GetAxis(verticalAxis) + camFollower.right.x * Input.GetAxis(horisontalAxis))
            , 0.0f
            , (camFollower.forward.z * Input.GetAxis(verticalAxis)) + (camFollower.right.z * Input.GetAxis(horisontalAxis)));






        rigBody.AddForce(moveDirection.x * currentSpeed / Time.deltaTime, 0.0f, moveDirection.z * currentSpeed / Time.deltaTime);

        if (rigBody.velocity.magnitude > currentSpeed)
        {
            maximumValecity = new Vector3(moveDirection.x * currentSpeed / Time.deltaTime, 0.0f, moveDirection.z * currentSpeed / Time.deltaTime).normalized * currentSpeed;

            rigBody.velocity = new Vector3(maximumValecity.x, rigBody.velocity.y, maximumValecity.z);

        }

    }

    void NormalJump()
    {
        rigBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        rigBody.AddForce(Vector3.forward * jumpForwardForce, ForceMode.Impulse);
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
