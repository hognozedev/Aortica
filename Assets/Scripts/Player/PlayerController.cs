using System.Collections;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayerData;

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
    [SerializeField] private float gravityValue = -9.81f, lookSensitivity = 100f;
    [SerializeField] private GameObject interactPrompt, h75, h50, h25;
    public bool inLobby;
    public CinemachineInputAxisController camInputs;

    //other privs
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;
    private float playerSpeed;

    //script refs
    public GunMaster gunMaster;
    public Dialogue dialogueScript;
    public PlayerStamina stamina;
    public CameraSwitching camSwitcher;

    //inputs
    private PlayerInput playerInput;
    [HideInInspector] public InputAction moveAction, sprintAction, clickAction, inventoryAction;
    private InputAction attackAction, reloadAction, interactAction;

    //collision
    [SerializeField] private float radius = 1f;
    [SerializeField] private LayerMask intLayers;
    private Collider[] buffer = new Collider[32];
    private IInteractable focused;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        sprintAction = playerInput.actions["Sprint"];
        attackAction = playerInput.actions["Attack"];
        reloadAction = playerInput.actions["Reload"];
        interactAction = playerInput.actions["Interact"];
        clickAction = playerInput.actions["Click"];
        inventoryAction = playerInput.actions["Inventory"];

        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        currentHealth = maxHealth;

    }

    void Update()
    {
        IInteractable nearest = FindNearestInteractable();
        UpdateFocus(nearest);
        if(focused != null && interactAction.WasPressedThisFrame())
        {
            if(focused.CanInteract()) focused.Interact();
        }

        bool walkForward = moveAction.IsPressed();
        bool isSprinting = sprintAction.IsPressed();

        if (inLobby == false)
        {
            gunMaster.isShooting = attackAction.WasPerformedThisFrame();
            gunMaster.isReloading = reloadAction.WasPerformedThisFrame();

            if (walkForward)
            {
                playerSpeed = walkSpeed;
            }

            if (isSprinting & walkForward & !camSwitcher.aiming)
            {
                if (stamina.currentStamina > 0)
                {
                    stamina.playerSprinting = true;
                    stamina.Sprinting();
                    playerSpeed = sprintSpeed;
                }
            }

            else
            {
                stamina.playerSprinting = false;
            }
        //end of stamina code

        }        
        
            if (inLobby == true)
        {
            playerSpeed = walkSpeed;

        }

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    //gravity

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);

        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);
    //use the vec2 to create a new vec3 where vertical movement is locked to 0 (change for jumping)

        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSensitivity * Time.deltaTime);
    //player will move in direction the camera faces

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


    public void UpdatePlayerHealth(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= (maxHealth * 0.75))
        {
            h75.SetActive(true);
            h50.SetActive(false);
            h25.SetActive(false);

            if(currentHealth <= (maxHealth * 0.5))
            {
                h75.SetActive(false);
                h50.SetActive(true);
                h25.SetActive(false);

                if (currentHealth <= (maxHealth * 0.25))
                {
                    h75.SetActive(false);
                    h50.SetActive(false);
                    h25.SetActive(true);

                    if (currentHealth <= 0)
                    {
                        PlayerDeath();

                    }
                }
            }

        }

    }

    void PlayerDeath()
    {
        Debug.Log("YOU DIE");
    }

    public void InMenu()
    {
        camInputs.enabled = false;
        playerInput.enabled = false;
    }

    public void ExitedMenu()
    {
        camInputs.enabled = true;
        playerInput.enabled = true;
    }

}