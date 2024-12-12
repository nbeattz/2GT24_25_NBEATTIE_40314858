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
        float width = 200f;
        float height = 20f;
        float xPos = Screen.width - width - 10f;
        float yPos = Screen.height - height - 10f;

        GUI.Box(new Rect(xPos, yPos, width, height), "", new GUIStyle() { normal = new GUIStyleState() { background = MakeTex(2, 2, new Color(0, 0, 0, 0.5f)) } });

        GUI.Box(new Rect(xPos, yPos, width * (currentStamina / maxStamina), height), "", new GUIStyle() { normal = new GUIStyleState() { background = MakeTex(2, 2, Color.blue) } });
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
