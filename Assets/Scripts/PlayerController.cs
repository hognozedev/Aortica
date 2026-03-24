using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

interface IInteractable
{
    public void Interact();
}

[RequireComponent(typeof(PlayerController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float lookSensitivity = 100f;

    public Transform interactSource;
    private float walkSpeed = 3f;
    private float playerSpeed = 3f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    private PlayerStamina staminaScript;
    public GunMaster gunMaster;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction attackAction;
    private InputAction reloadAction;
    private InputAction interactAction;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        staminaScript = GetComponent<PlayerStamina>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        sprintAction = playerInput.actions["Sprint"];
        attackAction = playerInput.actions["Attack"];
        reloadAction = playerInput.actions["Reload"];
        interactAction = playerInput.actions["Interact"];

        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        bool isSprinting = sprintAction.IsPressed();
        bool isWalking = moveAction.IsPressed();       

        gunMaster.isShooting = attackAction.WasPerformedThisFrame();
        gunMaster.isReloading = reloadAction.WasPerformedThisFrame();

        staminaScript.playerSprinting = false;

        if (isWalking)
        {
            staminaScript.playerSprinting = false;
            playerSpeed = walkSpeed;
        }

        if (isSprinting & isWalking)
        {
            if (staminaScript.playerStamina > 0)
            {
                    staminaScript.playerSprinting = true;
                    staminaScript.Sprinting();

                    playerSpeed = sprintSpeed;                 
            }
        }

        if(staminaScript.playerStamina <= 0 - 0.1)
        {
            staminaScript.playerSprinting = false;
            playerSpeed = walkSpeed;
        }
        // end of stamina code

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        // gravity

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);

        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);
        // use the vec2 to create a new vec3 where vertical movement is locked to 0 (change for jumping)


        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSensitivity * Time.deltaTime);
        // player will move in direction the camera faces

        if (interactAction.WasPressedThisFrame())
        {

        }

    }

    void ItemInteract(Vector3 center, float radius)
    {

    }
}