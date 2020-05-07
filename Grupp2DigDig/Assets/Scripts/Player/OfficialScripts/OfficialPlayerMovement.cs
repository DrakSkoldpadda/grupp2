using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficialPlayerMovement : MonoBehaviour
{
    [Header("Player view stuff")]
    [SerializeField] private Transform playerView; // Must be a camera

    [Header("CanMove")]
    private Menus menuScript;




    [Header("Frame occuring factors")] // Things that should uppdate every frame
    private float gravity = 20.0f;
    private float friction = 6f; // Ground friction



    [Header("Movement stuff")]
    public float moveSpeed = 7.0f; // Ground move speed
    public float runAcceleration = 14; // Ground accel
    public float runDecceleration = 10; // Decceleration that occures when running on the ground

    public float airAcceleration = 2.0f; // Air accel
    public float airDecceleration = 2.0f; // Decelleration experienced when opposite strafing
    public float airControl = 0.3f; // How precise air controll is

    public float sideStrafeAcceleration = 50f; // How fast acceleration occures to get up to sideStrafeSpeed when straifing
    public float sideStrafeSpeed = 1; // What the max speed to generate when side strafing

    public float jumpSpeed = 8.0f; // The speed at which the character's up axis gains when hitting jump
    public bool holdJumpToBhop = false; // When enabled allows the player to hold jump button to keep on bhopping perfecly. Beware: smells like casual

    [SerializeField] private float slopeForce; // Force added when traveling down a slope to prevent "slope bouncing"
    [SerializeField] private float slopeForceRayLeangth; // The leangth of the ray to se if you are on a slope
    private bool isJumping; // Only used for the slope checking due to me not using anything to see if I'm jumping


    public bool CanMove = true;


    [Header("")]
    public CharacterController controller;



    [Header("Core Movement")]
    private Vector3 moveDirection = Vector3.zero; // Current moving direction of the player
    private Vector3 moveDirectionNorm = Vector3.zero; // Not sure what it's for
    [SerializeField] public Vector3 playerVelocity = Vector3.zero; // Velocity of the player

    private float playerTopVelocity = 0.0f; // What the fastest the player has moved


    // If true the player is fully grounded
    private bool grounded = false;


    // Used for quing the next jump before hitting the ground
    private bool wishJump = false;


    // Used to display real time friction values
    private float playerFriction = 0.0f;



    // Contains the command the user wishes upon the character
    class Cmd
    {
        public float forwardMove;
        public float rightMove;
        public float upMove;
    }
    private Cmd cmd; // Player commands, stores wish commands that the player asks for (Forward, back, jump, etc)



    [Header("Player statuses")]
    private bool isDead = false;

    private Vector3 playerSpawnPos;
    private Quaternion playerSpawnRot;


    private void Start()
    {
        menuScript = GameObject.FindGameObjectWithTag("UI").GetComponent<Menus>();
        playerView = Camera.main.transform;


        controller = GetComponent<CharacterController>();
        cmd = new Cmd();

        // set spawn position
        playerSpawnPos = transform.position;
    }

    private void Update()
    {
        // Movement, Note: Important
        QueueJump();
        if (controller.isGrounded)
            GroundMove();
        else if (!controller.isGrounded)
            AirMove();



        // Move the controller
        if (menuScript.canMove)        
            controller.Move(playerVelocity * Time.deltaTime);        





        if ((cmd.forwardMove != 0 || cmd.rightMove != 0) && OnSlope())        
            controller.Move(Vector3.down * controller.height / 2 * slopeForce * Time.deltaTime);
        


        // Calculate top velocity
        Vector3 udp = playerVelocity;
        udp.y = 0;
        if (udp.magnitude > playerTopVelocity)
            playerTopVelocity = udp.magnitude;


        // Explode/respawn the player

        if (Input.GetKeyUp(KeyCode.X))
            PlayerExplode();
        if (Input.GetButtonDown("Fire1") && isDead)
            PlayerSpawn();

    }

    private bool OnSlope()
    {
        if (isJumping)
            return false;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, controller.height / 2 * slopeForceRayLeangth))
            if (hit.normal != Vector3.up)
                return true;

        return false;
    }


    /***************************\
    |--- MOVEMENT --------------|
    \***************************/



    // Sets the movement direction based on player input
    void SetMovementDir()
    {
        if (CanMove)
        {
            cmd.forwardMove = Input.GetAxis("Vertical");
            cmd.rightMove = Input.GetAxis("Horizontal");

        }
    }


    // Queues the next jump
    void QueueJump()
    {
        if (holdJumpToBhop)
        {
            wishJump = Input.GetKey(KeyCode.Space);
            return;
        }

        // If you are mid air and want to imideatly jump again after landing just hold space again, after releasing it the first time
        if (Input.GetKeyDown(KeyCode.Space) && !wishJump)
            wishJump = true;
        if (Input.GetKeyUp(KeyCode.Space))
            wishJump = false;
    }


    // Only when the player is in the air
    void AirMove()
    {

        Vector3 wishDir;
        float whishVel = airAcceleration;
        float accel;

        SetMovementDir();

        wishDir = new Vector3((playerView.forward.x * cmd.forwardMove + playerView.right.x * cmd.rightMove), 0.0f, (playerView.forward.z * cmd.forwardMove) + (playerView.right.z * cmd.rightMove));
        wishDir = transform.TransformDirection(wishDir);

        var wishSpeed = wishDir.magnitude; // calculates direction I guess ¯\_(ツ)_/¯
        wishSpeed *= moveSpeed; // The speed my character wishes to have

        wishDir.Normalize();
        moveDirectionNorm = wishDir;


        // CMP: Aircontroll
        float wishSpeed2 = wishSpeed;

        /*
         * If the direction I wish to travel is opisite of the one I'm 
         * currently moving in. Deccelerate untill it is not
         */
        if (Vector3.Dot(playerVelocity, wishDir) < 0)
            accel = airDecceleration;
        else
            accel = airAcceleration;



        // If the player is ONLY strafing left or right
        if (cmd.forwardMove == 0 && cmd.rightMove != 0)
        {
            if (wishSpeed > sideStrafeSpeed)
                wishSpeed = sideStrafeSpeed;
            accel = sideStrafeAcceleration;
        }

        Accelerate(wishDir, wishSpeed, accel);

        if (airControl == 0.3f)
            AirControll(wishDir, wishSpeed2);
        //!CPM: Aircontroll

        // Apply gravity
        playerVelocity.y -= gravity * Time.deltaTime;

    }


    /*
     * Air controll occures when the player is in the air, It allows 
     * players to move side to side much faster then being 
     * 'sluggish' when it comes to cornering
     */
    void AirControll(Vector3 wishDir, float wishSpeed)
    {

        float zSpeed;
        float speed;
        float dot;
        float k;


        // Can't controll movement if not moving forward or backward
        if (cmd.forwardMove == 0 || wishSpeed == 0)
            return;

        zSpeed = playerVelocity.y;
        playerVelocity.y = 0;


        // Next two lines are equivalent to idTeck's VectorNormalize(). Whatever that means
        speed = playerVelocity.magnitude;
        playerVelocity.Normalize();

        dot = Vector3.Dot(playerVelocity, wishDir);
        k = 32;
        k *= airControl * dot * dot * Time.deltaTime;


        // Change direction while slowing down
        if (dot > 0)
        {

            playerVelocity.x = playerVelocity.x * speed + wishDir.x * k;
            playerVelocity.y = playerVelocity.y * speed + wishDir.y * k;
            playerVelocity.z = playerVelocity.z * speed + wishDir.z * k;

            playerVelocity.Normalize();
            moveDirectionNorm = playerVelocity;

        }

        playerVelocity.x *= speed;
        playerVelocity.y = zSpeed; // Note this line
        playerVelocity.z *= speed;


    }


    // Calls every frame when the player is grounded
    void GroundMove()
    {

        Vector3 wishDir;


        //// Do not apply friction if the player is queuing up the next jump ( Used for B-hopping )
        //if (!wishJump)
        //    ApplyFriction(1.0f);
        //else
        //    ApplyFriction(0.0f);

        ApplyFriction(1.0f);


        SetMovementDir();

        wishDir = new Vector3((playerView.forward.x * cmd.forwardMove + playerView.right.x * cmd.rightMove), 0.0f, (playerView.forward.z * cmd.forwardMove) + (playerView.right.z * cmd.rightMove));
        wishDir = transform.TransformDirection(wishDir);
        wishDir.Normalize();
        moveDirection = wishDir;


        var wishSpeed = wishDir.magnitude;
        wishSpeed *= moveSpeed;

        Accelerate(wishDir, wishSpeed, runAcceleration);

        // Reset the gravity velocity
        playerVelocity.y = 0;


        // Applies the velovity upwards for the jump
        if (wishJump)
        {
            isJumping = true;
            playerVelocity.y = jumpSpeed;
            wishJump = false;
        }

    }



    // Applies friction to the player, called both in the air and ground
    void ApplyFriction(float t)
    {
        Vector3 vec = playerVelocity;
        float speed;
        float newSpeed;
        float control;
        float drop;

        vec.y = 0.0f;
        speed = vec.magnitude;
        drop = 0.0f;


        // If grounded apply friction
        if (controller.isGrounded)
        {
            isJumping = false;
            control = speed < runDecceleration ? runDecceleration : speed;
            drop = control * friction * Time.deltaTime * t;
        }


        newSpeed = speed - drop;
        playerFriction = newSpeed;
        if (newSpeed < 0)
            newSpeed = 0;
        if (newSpeed > 0)
            newSpeed /= speed;

        playerVelocity.x *= newSpeed;
        playerVelocity.z *= newSpeed;


    }


    // Calculates wish acceleration based on cmd wishes
    void Accelerate(Vector3 wishDir, float wishSpeed, float accel)
    {

        float addSpeed;
        float accelSpeed;
        float currentSpeed;

        currentSpeed = Vector3.Dot(playerVelocity, wishDir);
        addSpeed = wishSpeed - currentSpeed;

        if (addSpeed <= 0)
            return;

        accelSpeed = accel * Time.deltaTime * wishSpeed;
        if (accelSpeed > addSpeed)
            accelSpeed = addSpeed;

        playerVelocity.x += accelSpeed * wishDir.x;
        playerVelocity.z += accelSpeed * wishDir.z;

    }


    private void OnGUI()
    {
        var ups = controller.velocity; // ups = Unit Per Second
        ups.y = 0;
        GUI.Label(new Rect(0, 15, 400, 100), "Speed: " + Mathf.Round(ups.magnitude * 100) / 100 + "ups");
        GUI.Label(new Rect(0, 30, 400, 100), "Top Speed: " + Mathf.Round(playerTopVelocity * 100) / 100 + "ups");

    }

    void PlayerExplode()
    {
        //var velocity = controller.velocity;
        //velocity.Normalize();
        //var gibEffect = Instantiate(gibEffectPrefab, transform.position, Quaternion.identity);
        //gibEffect.GetComponent(GibFX).Explode(transform.position, velocity, controller.velocity.magnitude);
        isDead = true; // Add  gib later
    }
    void PlayerSpawn()
    {
        transform.position = playerSpawnPos;

        playerVelocity = Vector3.zero;
        isDead = false;
    }

}
