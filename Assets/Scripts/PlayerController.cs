using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
[RequireComponent(typeof(PlayerController), typeof(PlayerInput))]


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 10;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float lookSensitivity = 100f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    private PlayerStamina staminaScript;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction aimAction;
    private InputAction sprintAction;
    private InputAction attackAction;
    private InputAction interactAction;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        staminaScript = GetComponent<PlayerStamina>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        sprintAction = playerInput.actions["Sprint"];

        cameraTransform = Camera.main.transform;
        
    }

    public void SprintSpeed(float speed)
    {
        playerSpeed = speed;
    }


    void Update()
    {

        bool isSprinting = Keyboard.current.shiftKey.isPressed;
        bool isWalking = Keyboard.current.wKey.isPressed;

        staminaScript.playerSprinting = false;

        if (isWalking)
        {
            staminaScript.playerSprinting = false;
        }

        if (isSprinting & isWalking)
        {

            if (staminaScript.playerStamina > 0)
            {
                staminaScript.playerSprinting = true;
                staminaScript.Sprinting();
            }

            else
            {
                isWalking = true;
            }
        }


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

    }
}