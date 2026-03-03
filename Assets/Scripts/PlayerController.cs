using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
[RequireComponent(typeof(PlayerController), typeof(PlayerInput))]


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float lookSensitivity = 100f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform barrelMove;
    [SerializeField] private Transform bulletParent;


    private float walkSpeed = 3f;
    private float playerSpeed = 3f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;
    private float bulletMissDistance = 25f;

    private PlayerStamina staminaScript;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction aimAction;
    private InputAction sprintAction;
    private InputAction attackAction;
    private InputAction interactAction;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        staminaScript = GetComponent<PlayerStamina>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        sprintAction = playerInput.actions["Sprint"];
        aimAction = playerInput.actions["Aim"];
        attackAction = playerInput.actions["Attack"];


        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        attackAction.performed += _ => ShootGun();
    }

    private void OnDisable()
    {
        attackAction.performed -= _ => ShootGun();
    }

    private void ShootGun()
    {
        RaycastHit hit;

        GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelMove.position, Quaternion.identity, bulletParent);
        BulletControl bulletControl = bullet.GetComponent<BulletControl>();
       
        //populate the 'hit' variable with whatever the raycast makes contact with when being projected forwards from the camera centre.

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
        {
            bulletControl.target = hit.point;
            bulletControl.hit = true;
        }
        else
        {
            bulletControl.target = cameraTransform.position + cameraTransform.forward * bulletMissDistance;
            bulletControl.hit = true;
        }
    }

    void Update()
    {

        bool isSprinting = Keyboard.current.shiftKey.isPressed;
        bool isWalking = Keyboard.current.wKey.isPressed;

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

    }
}