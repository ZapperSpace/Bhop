using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Whats wrong with this controller? this nigga cant walk up stairs... smh :(
    //still a really good controller i dont feel like making a dam square into stairs
    //just makin another wont kill
    [Header("Player-Settings")]
    public bool AutoHop;
    public bool AirControl;
    [SerializeField]
    private float accel = 200f;         // How fast the player accelerates on the ground
    [SerializeField]
    private float maxSpeed = 6.4f;      // Maximum player speed on the ground base speed;
    [SerializeField]
    private float bckAccel = 75f;
    [SerializeField]
    private float airAccel = 200f;      // How fast the player accelerates in the air
    [SerializeField]
    private float maxAirSpeed = 0.6f;   // "Maximum" player speed in the air
    [SerializeField]
    private float runAccel = 300f;      //Running in the 90's
    [SerializeField]
    private float runSpeed = 12f;
    [SerializeField]
    private float crouchAccel = 75f;
    [SerializeField]
    private float crouchSpeed = 4f;
    [SerializeField]
    private float friction = 8f;        // How fast the player decelerates on the ground
    [SerializeField]
    private float jumpForce = 5f;       // How high the player jumps
    [SerializeField]
    private float gravity = 10f;
    [SerializeField]
    private float crouchLevel = 0.5f;
    [SerializeField]
    private Vector3 respawnPoint;

    [SerializeField]
    private LayerMask groundLayers;
    [SerializeField]
    private CapsuleCollider capsuleCollider;
    [SerializeField]
    private BoxCollider boxCollider;
    [SerializeField]
    private GameObject objectGraphics;
    [Header("Camera-Settings")]
    public CameraSettings cameraSettings;
    [SerializeField]
    private GameObject FrstPrsncamObj;
    [SerializeField]
    private GameObject ThrdPrsncamObj;
    [SerializeField]
    private GameObject camObj;
    [SerializeField] private float properRotationY;
    [SerializeField] private float cameraFlip;
    [SerializeField] private bool FrstPrnOn;
    [SerializeField] private bool ThrdPrsnOn;
    [SerializeField] private Vector3 camRotation;
    [SerializeField] private float cameraSettin;
    public float negitiveFlip;
    //
    // [Space]
    // [Header("Player Stats")]
     public Vector3 AxisPosition;
     public bool onGround = false;
     public bool inAir = false;
    public bool Movement;
    public bool isWalking;
    public bool isCrouching;
    public float Crouchfloat;
    public bool isRunning;
    public bool isJumping;
    public bool isFalling;
    public bool isLanded;
    public float landingTime;
    public bool isCrouchedLanded;
    public bool isTurning;
    public bool isFliped;
    public bool isBckwrd;
    public float curAccel;
    public float curMaxSpeed;
    [SerializeField] private float lastJumpPress = -1f;
    [SerializeField] private float jumpPressDuration = 0.1f;
     public float speed;
    public float velocity;
    public float VectorX;
    public float VectorZ;
    public float MouseX;
    public float MouseY;
    public float RotY;
    public float inputSensitivity;
    public float airTime;
    public float velocityX;
    public float idelTime;
    public bool boxison;
    private Vector3 setCameraDirection;

    private string Veritcal = "Vertical";
    private string Horizontal = "Horizontal";
    private bool JumpButton;

    public Animator animator;

    void Start()
    {
       
    }

    private void Update()
    {
        CameraFunctions();
        MovementCalulations();
        Crounchin();
        Respawn();
        //print(new Vector3(GetComponent<Rigidbody>().velocity.x, 0f, GetComponent<Rigidbody>().velocity.z).magnitude);//prints overall speed of the controller 


        if (AutoHop)
        {
            JumpButton = Input.GetButton("Jump");
        }
        else
        {
            JumpButton = Input.GetButtonDown("Jump");
        }
        if (JumpButton)
        {
            lastJumpPress = Time.time;
        }

        //put this in the menu
        //did it boss :)
    //    animator.SetFloat("Speed", speed);
    //    animator.SetBool("Jumping", isJumping);
    //    animator.SetBool("Falling", isFalling);
    //    animator.SetBool("Crouching", isCrouching);
    //    animator.SetFloat("CrouchFloat", Crouchfloat);
    //    animator.SetFloat("VectorX", VectorX);
    //    animator.SetFloat("VectorZ", VectorZ);
    //    animator.SetBool("Movement", Movement);
    //    animator.SetFloat("AirTime", airTime);
    //    animator.SetFloat("IdelTime", idelTime);
    //    animator.SetBool("InAir", inAir);
    //    animator.SetBool("Landed", isLanded);
     //   animator.SetFloat("Velocity", velocity);
    }

    private void CameraFunctions()
    {
        FrstPrnOn = cameraSettings.FstPerson;
        ThrdPrsnOn = cameraSettings.ThrdPerson;

        if (FrstPrnOn)
        {
            camObj = FrstPrsncamObj;
        }
        if (ThrdPrsnOn)
        {
            camObj = ThrdPrsncamObj;
        }

   //     cameraFlip = properRotationY - camRotation.y;

        if (speed == 0 && ThrdPrsnOn)
        {
            properRotationY = properRotationY;
            objectGraphics.transform.localEulerAngles = new Vector3(0, Mathf.Round(properRotationY), 0);
        }
        else
        {
            properRotationY = camRotation.y;
            objectGraphics.transform.localEulerAngles = new Vector3(0, Mathf.Round(properRotationY), 0);

        }

        cameraSettin = camRotation.y;

       
        negitiveFlip = -cameraFlip;

       
        if (properRotationY >  cameraSettin)
        {
            cameraFlip = properRotationY + -cameraSettin;
        }
        else
        {
            cameraFlip = properRotationY - cameraSettin;
        }


        if (!Movement && speed > 0)
        {
            isTurning = true;
        }
        else
        {
            isTurning = false;
        }

        RotY = transform.localEulerAngles.y;

        if (RotY > 180)
        {
           RotY = -(360 - RotY);
        }
       // MouseX += Input.GetAxisRaw("MouseX") * inputSensitivity;
       // MouseY -= Input.GetAxisRaw("MouseY") * inputSensitivity;

    }

    private void FixedUpdate()
    {

        VectorX = Input.GetAxis("Horizontal");
        VectorZ = Input.GetAxis("Vertical");
        MouseX += Input.GetAxisRaw("Mouse X");
        MouseY -= Input.GetAxisRaw("Mouse Y");

        Vector2 input = new Vector2(VectorX, VectorZ);
        //Vector2 input = new Vector2(0, Input.GetAxis(Veritcal)); //previously on dis scripting uhh you want that shit to rotate by a and d and goes in a circle but it dont :/ it follow backend camera movement so past jason ideas was to use the preivous script and see what happensif u morph with the camera shit uknoww uknow
        //well do this latter but there no reason too since we tryina do cs style
        //ApplyInput(turnaxis);
        //float turnaxis = Input.GetAxis(Horizontal);
        // Get player velocity
        Vector3 playerVelocity = GetComponent<Rigidbody>().velocity;
        // Slow down if on ground
        playerVelocity = CalculateFriction(playerVelocity);
        // Add player input
        playerVelocity += CalculateMovement(input, playerVelocity);
        // Assign new velocity to player object
        GetComponent<Rigidbody>().velocity = playerVelocity;

        AxisPosition = playerVelocity;
        velocity = playerVelocity.y;
        speed = new Vector3(GetComponent<Rigidbody>().velocity.x, 0f, GetComponent<Rigidbody>().velocity.z).magnitude;

    }


    private void MovementCalulations()
    {
        onGround = CheckGround();

        //Different acceleration values for ground and air
        curAccel = accel;
        curMaxSpeed = maxSpeed;
        if (!onGround)
        {
            inAir = true;
            curAccel = airAccel;
        }
        if (onGround)
            inAir = false;
        curAccel = !onGround ? airAccel : isCrouching && onGround ? crouchAccel : isRunning && onGround ? runAccel : onGround && isBckwrd ? bckAccel : accel;
        curMaxSpeed = !onGround ? maxAirSpeed : isCrouching && onGround ? crouchSpeed : isRunning && onGround ? runSpeed : maxSpeed;


        if ((Input.GetKey(KeyCode.LeftShift)) && onGround)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        if (velocity > 0)
        {
            isJumping = true;
        }
        if (velocity < 0)
        {
            isFalling = true;
            isJumping = false;
            if (onGround)
            {
                isLanded = true;
                if (velocityX > -8)
                {
                   // Crouchfloat += 1;
                    Debug.Log("soft");
                }
                else
                {
                  //  Crouchfloat += 1;
                    Debug.Log("hard");
                }

            }
        }

        if (onGround)
        {
            isFalling = false;
            isJumping = false;
            landingTime = landingTime;
            airTime = 0;
            if (VectorX == 0 && VectorZ == 0)
            {
                //speed = 0;
            }
            if (VectorZ < 0)
            {
                isBckwrd = true;
            }
            else
            {
                isBckwrd = false;
            }
        }
        if (inAir)
        {
            airTime += Time.deltaTime * 1;
            landingTime = airTime;
            isLanded = false;
            idelTime = 0;
            velocityX = velocity;
        }
        if (VectorZ == 0 && VectorX == 0)
        {
            Movement = false;
            idelTime += Time.deltaTime * 1;
        }
        else
        {
            idelTime = 0;
            Movement = true;
        }

        
    }
    #region HorizontalTurning
    private void ApplyInput(float turnInput)
    {
        //Turn(turnInput);//it turns nigga
    }
    private void Turn(float rotationinput)
    {
        // transform.Rotate(0, rotationRate * Time.deltaTime, 0);//turning..
    }
    #endregion

    /// <summary>
    /// Slows down the player if on ground
    /// </summary>
    /// <param name="currentVelocity">Velocity of the player</param>
    /// <returns>Modified velocity of the player</returns>
    private Vector3 CalculateFriction(Vector3 currentVelocity)
    {
        onGround = CheckGround();
        float speed = currentVelocity.magnitude;

        if (!onGround || Input.GetButton("Jump") || speed == 0f)
            return currentVelocity;

        float drop = speed * friction * Time.deltaTime;
        return currentVelocity * (Mathf.Max(speed - drop, 0f) / speed);
    }

    /// <summary>
    /// Moves the player according to the input. (THIS IS WHERE THE STRAFING MECHANIC HAPPENS)
    /// </summary>
    /// <param name="input">Horizontal and vertical axis of the user input</param>
    /// <param name="velocity">Current velocity of the player</param>
    /// <returns>Additional velocity of the player</returns>
    private Vector3 CalculateMovement(Vector2 input, Vector3 velocity)
    {
        onGround = CheckGround();

        MovementCalulations();

        //Get rotation input and make it a vector

        if (AirControl)
        {
            camRotation = new Vector3(0f, camObj.transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            if (onGround)
            {
                setCameraDirection = camRotation;
                camRotation = new Vector3(0f, camObj.transform.rotation.eulerAngles.y, 0f);
            }
            if (inAir)
            {
                camRotation = setCameraDirection;
            }
        }
        
            Vector3 inputVelocity = Quaternion.Euler(camRotation) * new Vector3(input.x * curAccel, 0f, input.y * curAccel);
        
        //Ignore vertical component of rotated input
        Vector3 alignedInputVelocity = new Vector3(inputVelocity.x, 0f, inputVelocity.z) * Time.deltaTime;

        //Get current velocity
        Vector3 currentVelocity = new Vector3(velocity.x, 0f, velocity.z);

        //How close the current speed to max velocity is (1 = not moving, 0 = at/over max speed)
        float max = Mathf.Max(0f, 1 - (currentVelocity.magnitude / curMaxSpeed));

        //How perpendicular the input to the current velocity is (0 = 90°)
        float velocityDot = Vector3.Dot(currentVelocity, alignedInputVelocity);

        //Scale the input to the max speed
        Vector3 modifiedVelocity = alignedInputVelocity * max;

        //The more perpendicular the input is, the more the input velocity will be applied
        Vector3 correctVelocity = Vector3.Lerp(alignedInputVelocity, modifiedVelocity, velocityDot);

        //Apply jump
        correctVelocity += GetJumpVelocity(velocity.y);

        //gravity
        if (velocity.y < 0)
        {
            correctVelocity += GetJumpVelocity(-gravity);
        }
        //Return
        return correctVelocity;



    }

    /// <summary>
    /// Calculates the velocity with which the player is accelerated up when jumping
    /// </summary>
    /// <param name="yVelocity">Current "up" velocity of the player (velocity.y)</param>
    /// <returns>Additional jump velocity for the player</returns>
	private Vector3 GetJumpVelocity(float yVelocity)
    {
        Vector3 jumpVelocity = Vector3.zero;

        if (Time.time < lastJumpPress + jumpPressDuration && yVelocity < jumpForce && CheckGround())
        {
            //if the jumppresstimer is greater and if the jumpforce is greater than the velocity and if on the ground.
            lastJumpPress = -1f;
            jumpVelocity = new Vector3(0f, jumpForce - yVelocity, 0f);
        }

        return jumpVelocity;
    }

    /// <summary>
    /// Checks if the player is touching the ground. This is a quick hack to make it work, don't actually do it like this.
    /// </summary>
    /// <returns>True if the player touches the ground, false if not</returns>
    private bool CheckGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        bool result = Physics.Raycast(ray, GetComponent<Collider>().bounds.extents.y + 0.1f, groundLayers);
        return result;
    }
    void Crounchin()
    {
        Vector3 crouchedpostion = new Vector3(0f, 0.150f, 0f);
        if (Input.GetKey(KeyCode.C))
        {
            capsuleCollider.height = crouchLevel;
            isCrouching = true;
            Crouchfloat = 1;
            objectGraphics.transform.localPosition = (crouchedpostion);
          //  objectGraphics.transform.Translate(0, +0.152f, 0);

        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            crouchedpostion = new Vector3(0f, 0f, 0f);
            objectGraphics.transform.localPosition = (crouchedpostion);
            capsuleCollider.height = 2f;
            isCrouching = false;
            Crouchfloat = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Plane(min)")
        {
            //If the GameObject has the same tag as specified, output this message in the console
           // Debug.Log("Do something else here");
        }
    }

    void Respawn()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = respawnPoint;
            Debug.Log("Respawned");
        }
    }
}
