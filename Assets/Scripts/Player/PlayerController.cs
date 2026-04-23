using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using System.Collections;

interface IInteractable
{
    public bool CanInteract();
    public void Interact();
    public void OnFocusGained();
    public void OnFocusLost();
}

[RequireComponent(typeof(PlayerController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    //inspector variables
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float lookSensitivity = 100f;
    [SerializeField] private GameObject interactPrompt;

    //other privs
    private float walkSpeed = 3f;
    private float playerSpeed;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    //script refs
    public GunMaster gunMaster;
    private PlayerStamina playerStamina;

    //inputs
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction attackAction;
    private InputAction reloadAction;
    private InputAction interactAction;

    //collision
    [SerializeField] private float radius = 1f;
    [SerializeField] private LayerMask intLayers;
    private Collider[] buffer = new Collider[32];
    private IInteractable focused;



    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        playerStamina = GetComponent<PlayerStamina>();

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
        IInteractable nearest = FindNearestInteractable();
        UpdateFocus(nearest);
        if(focused != null && interactAction.WasPressedThisFrame())
        {
            if(focused.CanInteract()) focused.Interact();
        }

        bool isSprinting = sprintAction.IsPressed();
        bool walkForward = moveAction.IsPressed();      //make so only for forward motion (player local z axis)                    

        gunMaster.isShooting = attackAction.WasPerformedThisFrame();
        gunMaster.isReloading = reloadAction.WasPerformedThisFrame();

        playerStamina.playerSprinting = false;

        if (walkForward)
        {
            playerStamina.playerSprinting = false;
            playerSpeed = walkSpeed;
        }

        if (isSprinting & walkForward)
        {
            if (playerStamina.currentStamina > 0)
            {
                playerStamina.playerSprinting = true;
                playerStamina.Sprinting();

                playerSpeed = sprintSpeed;                 
            }
        }

        if(playerStamina.currentStamina <= 0 - 0.1)
        {
            playerStamina.playerSprinting = false;
            playerSpeed = walkSpeed;
        }
        // end of stamina code

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        // gravity

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

    private IInteractable FindNearestInteractable()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, radius, buffer, intLayers, QueryTriggerInteraction.Collide);
        IInteractable nearest = null;

        float bestDistSq = float.MaxValue;

        for(int i = 0; i < count; i++)
        {
            Collider col = buffer[i];
            if(col == null) continue;
            IInteractable interactable = col.GetComponentInParent<IInteractable>();

            if(interactable == null) continue;
            if(!interactable.CanInteract()) continue;

            float distSq = (col.transform.position - transform.position).sqrMagnitude;
            if (distSq < bestDistSq)
            {
                bestDistSq = distSq;
                nearest = interactable;
            }
        }
        return nearest;
    }
    private void UpdateFocus(IInteractable nearest)
    {
        if(ReferenceEquals(focused, nearest)) return;
        focused?.OnFocusLost();
        focused = nearest;
        focused?.OnFocusGained();

    }

}
