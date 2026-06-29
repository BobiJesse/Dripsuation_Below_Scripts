using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour //class to handle player movement and interactions
{
    public static PlayerMovement instance;

    [Header("Movement")]
    public float moveSpeed = 6f; //the speed that the player moves
    public float mouseSensitivity = 2f; //sensitivity for how fast the player turns
    public float controllerSensitivity = 20f; //sensitivity for how fast the player turns
    public float currentSensitivity; //sensitivity for how fast the player turns
    public float jumpForce; //how high the player can jump
    public bool jumpHeld;
    public bool swimmingUp;
    public Transform cameraTransform; //position of the camera
    //public bool isGrounded; //variable to hold info if player is on the ground
    public bool canSwim; //variable to hold info if player is on the ground
    Vector2 moveInput;
    Vector2 lookInput;

    Rigidbody rb; //reference to the players rigidbody
    public PlayerInput playerInput;
    float xRotation = 0f; //tracks the players orientation
    public bool actionHeld;
    public float oxygenLevel;

    [Header("Items")]
    public GameObject bucket; //reference to the bucket gameobject
    public GameObject weldingTool; //reference to the welding tool gameobject
    public Item currentItem; //tracks what item is currently equipped
    private bool weldingActionAvailable;
    Leak currentLeak;

    [Header("LayerMasks")]
    public LayerMask leakLayer; //layermask for only leaks
    public LayerMask groundLayer; //layermask for only ground

    public GameObject text;

    [Header("Speed")]
    public float maxSpeed;

    public enum Item //item equip states
    {
        bucket,
        weldingTool
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

            rb = GetComponent<Rigidbody>(); //get reference to the rigidbody component
    }

    void Start()
    {
        oxygenLevel = 2;
    }

    void Update()
    {
        Look(); //rotates the player

        foreach (InputDevice device in playerInput.devices)
        {
            if (device is Keyboard)
            {
                currentSensitivity = mouseSensitivity;
            }
            else if (device is Mouse)
            {
                currentSensitivity = mouseSensitivity;
            }
            else if (device is Gamepad)
            {
                currentSensitivity = controllerSensitivity;
            }
        }

        //isGrounded = Physics.Raycast(gameObject.transform.position + new Vector3(0, 0.1f, 0), Vector3.down, 0.2f, groundLayer); //raycast to players feet to check if player is on the ground
        //Debug.DrawRay(gameObject.transform.position, Vector3.down * 0.2f, Color.red); //draw the groundcheck raycast for debugging

        if (currentItem == Item.weldingTool) //if holding welding tool, draw raycast 
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3f, leakLayer))
            {
                currentLeak = hit.transform.GetComponent<Leak>();
                text.SetActive(true);
                weldingActionAvailable = true;
            }

            else
            {
                text.SetActive(false);
                weldingActionAvailable = false;
                currentLeak = null;
            }
        }

        if (actionHeld)
        {
            PerformAction();
        }

        if (jumpHeld)
        {
            Jump();
        }

        else if (!jumpHeld)
        {
            swimmingUp = false;
        }

        if ((!actionHeld || !weldingActionAvailable) && currentLeak != null)
        {
            if (currentLeak.weldingParticle.isPlaying)
            {
                currentLeak.weldingParticle.Stop();
            }
        }

        if (oxygenLevel < 0)
        {
            Debug.Log("Game Over, oxygen ran out");
            GameManager.instance.GameOVer();
        }

    }

    void FixedUpdate()
    {
        Move(); //moves the player

        if (!swimmingUp)
        {
            rb.AddForce(new Vector3(0, -4.5f, 0), ForceMode.Force);

            if (rb.linearVelocity.y < maxSpeed)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, maxSpeed, rb.linearVelocity.z);
            }
            //rb.linearVelocity = new Vector3(rb.linearVelocity.x, -1f, rb.linearVelocity.z);
        }
    }

    void Jump()
    {
        /*
        if (isGrounded == true)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
        */

        if (canSwim)
        {
            swimmingUp = true;
            rb.linearVelocity = new Vector3(0f, 2f, 0f);
        }
    }

    void Move()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        Vector3 velocity = new Vector3(
            move.x * moveSpeed,
            rb.linearVelocity.y,
            move.z * moveSpeed
        );

        rb.linearVelocity = velocity;
    }

    void Look()
    {
        float mouseX = lookInput.x * currentSensitivity * SensitivityManager.instance.masterSensitivity;
        float mouseY = lookInput.y * currentSensitivity * SensitivityManager.instance.masterSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    private void OnMove(InputValue inputvalue)
    {
        moveInput = inputvalue.Get<Vector2>();
    }

    private void OnLook(InputValue inputvalue)
    {
        lookInput = inputvalue.Get<Vector2>();
    }

    private void OnInteract(InputValue inputValue)
    {
        ChangeItem();
    }

    private void OnJump(InputValue inputValue)
    {
        jumpHeld = inputValue.isPressed;
    }

    private void OnAction(InputValue inputValue)
    {
        actionHeld = inputValue.isPressed;
    }

    private void OnPause(InputValue inputValue)
    {
        PauseMenu.instance.OpenPauseMenu();
    }

    private void ChangeItem()
    {
        if (currentItem == Item.weldingTool)
        {
            currentItem = Item.bucket;
            bucket.SetActive(true);
            weldingTool.SetActive(false);
        }

        else if (currentItem == Item.bucket)
        {
            currentItem = Item.weldingTool;
            bucket.SetActive(false);
            weldingTool.SetActive(true);
        }
    }

    private void PerformAction()
    {
        if (currentItem == Item.weldingTool && weldingActionAvailable && actionHeld)
        {
            currentLeak.health += 0.25f;

            if (!currentLeak.weldingParticle.isPlaying)
            {
                currentLeak.weldingParticle.Play();
            }
        }
    }
}
