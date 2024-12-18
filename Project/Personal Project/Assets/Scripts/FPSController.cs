using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 5f;
    public float runSpeed = 7f;
    public float jumpPower = 3f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    public float maxStamina = 5f; // Maximum stamina
    public float staminaRegenRate = 1f; // Stamina regeneration 
    public float staminaDepletionRate = 2f; // Stamina depletion 
    private float currentStamina;

    private bool canRun = true;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;
    public Animator animator; // Animation

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>(); // Animation
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentStamina = maxStamina; 
    }

    void Update()
    {
        #region Handles Movement

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && canRun;

        animator.SetBool("isRunning", isRunning); // Animation
        if (isRunning)
        {
            currentStamina -= staminaDepletionRate * Time.deltaTime;
            if (currentStamina <= 0)
            {
                currentStamina = 0;
                canRun = false; 
            }
        }
        else
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina >= maxStamina)
            {
                currentStamina = maxStamina;
                canRun = true; 
            }
        }

        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);


        animator.SetFloat("horizontal", Input.GetAxis("Horizontal")); // Animation
        animator.SetFloat("vertical", Input.GetAxis("Vertical")); // Animation
        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
            animator.SetBool("isJumping", true); // Animation
        }
        else
        {
            moveDirection.y = movementDirectionY;
            animator.SetBool("isJumping", false); // Animation
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion
    }

    void OnGUI()
    {
        // Reference resolution (1920x1080)
        float referenceWidth = 1920f;
        float referenceHeight = 1080f;

        // Scaling factor based on current screen size
        float scaleX = Screen.width / referenceWidth;
        float scaleY = Screen.height / referenceHeight;

        // Scale uniformly (use the smaller scale to maintain aspect ratio)
        float scale = Mathf.Min(scaleX, scaleY);

        // Define larger bar dimensions relative to the reference resolution
        float width = 400f * scale;   // Increase width
        float height = 40f * scale;  // Increase height
        float xPos = Screen.width - width - (20f * scale);  // Adjust position
        float yPos = Screen.height - height - (20f * scale); // Adjust position

        // Background box for the stamina bar
        GUI.Box(new Rect(xPos, yPos, width, height), "", new GUIStyle()
        {
            normal = new GUIStyleState() { background = MakeTex(2, 2, new Color(0, 0, 0, 0.5f)) }
        });

        // Foreground box for current stamina (purple color)
        GUI.Box(new Rect(xPos, yPos, width * (currentStamina / maxStamina), height), "", new GUIStyle()
        {
            normal = new GUIStyleState() { background = MakeTex(2, 2, new Color(0.5f, 0, 0.5f, 1f)) } // Purple color
        });

        // Display "Stamina" text inside the box
        GUIStyle textStyle = new GUIStyle();
        textStyle.alignment = TextAnchor.MiddleCenter;
        textStyle.normal.textColor = Color.white; // White text color
        textStyle.fontSize = (int)(40 * scale); // Scale the font size
        textStyle.fontStyle = FontStyle.Bold;
        GUI.Label(new Rect(xPos, yPos, width, height), "STAMINA", textStyle);
    }

    // Utility function to create textures for the stamina bar
    private Texture2D MakeTex(int width, int height, Color color)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; i++) pix[i] = color;
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}
