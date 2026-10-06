using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerData;

interface IInteractable
{
    public void Interact();
    public void OnFocusGained();
    public void OnFocusLost();
}

[RequireComponent(typeof(PlayerController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Variables")]
    public float lookSensitivity;
    [SerializeField] private LayerMask interactableLayers;
    public bool inLobby;

    [Header("References")]
    public CinemachineInputAxisController camInputs;
    [SerializeField] private GameObject interactPrompt, h75, h50, h25;
    public GameObject playerHUD;
    public CharacterController cc;

    [Header("Scripts")]
    public DeathScreen deathScreen;
    public CameraSwitching camSwitcher;
    public PauseManager pauseManager;

    //movement
    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;
    private float playerSpeed;
    [HideInInspector] public bool hasRun, inMenu;

    //script refs
    private PlayerStamina stamina;
    private CharacterController controller;

    //inputs
    private PlayerInput playerInput;
    [HideInInspector] public InputAction moveAction, sprintAction, clickAction, inventoryAction, attackAction, reloadAction, cancelAction, interactAction, debugAction, scrollAction, oneAction, twoAction, threeAction, escapeAction, aimAction, menuInteractAction;

    //collision
    private float radius = 1f;
    private Collider[] buffer = new Collider[32];
    private IInteractable focused;

    public LobbyData lobbyTest;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        stamina = GetComponent<PlayerStamina>();

        moveAction = playerInput.actions["Move"];
        sprintAction = playerInput.actions["Sprint"];
        attackAction = playerInput.actions["Attack"];
        reloadAction = playerInput.actions["Reload"];
        interactAction = playerInput.actions["Interact"];
        inventoryAction = playerInput.actions["Inventory"];
        clickAction = playerInput.actions["Click"];
        cancelAction = playerInput.actions["Cancel"];
        debugAction = playerInput.actions["DEBUG"];
        scrollAction = playerInput.actions["Scroll"];
        oneAction = playerInput.actions["Key1"];
        twoAction = playerInput.actions["Key2"];
        threeAction = playerInput.actions["Key3"];
        escapeAction = playerInput.actions["PauseMenu"];
        aimAction = playerInput.actions["Aim"];
        menuInteractAction = playerInput.actions["MenuInteract"];

        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        currentHealth = maxHealth;
        if (inLobby == false) UpdatePlayerHealth(0);

        nextLobby = lobbyTest;
    }

    void Update()
    {
        bool debug = debugAction.WasPerformedThisFrame();
        if (debug) Debug.Log(playerInput.currentActionMap.ToString());

        IInteractable nearest = FindNearestInteractable();
        UpdateFocus(nearest);
        if (focused != null && interactAction.WasPressedThisFrame()) focused.Interact();

        bool walkForward = moveAction.IsPressed();
        bool isSprinting = sprintAction.IsPressed();

        if (inLobby == false)
        {
            if (walkForward) playerSpeed = walkSpeed;

            if (isSprinting & walkForward & !camSwitcher.isAiming)
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

        }

        if (inLobby == true) playerSpeed = walkSpeed;
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0) playerVelocity.y = 0f;

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
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (lookSensitivity * 100) * Time.deltaTime);
    //player will move in direction the camera faces
    }

    private IInteractable FindNearestInteractable()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, radius, buffer, interactableLayers, QueryTriggerInteraction.Collide);
        IInteractable nearest = null;

        float bestDistSq = float.MaxValue;

        for (int i = 0; i < count; i++)
        {
            Collider col = buffer[i];
            if (col == null) continue;
            IInteractable interactable = col.GetComponentInParent<IInteractable>();

            if (interactable == null) continue;

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
        if (ReferenceEquals(focused, nearest)) return;
        focused?.OnFocusLost();
        focused = nearest;
        focused?.OnFocusGained();
    }

    public void UpdatePlayerHealth(int damage)
    {
        currentHealth -= damage;

        if((currentHealth -= damage) >= maxHealth) currentHealth = maxHealth;


        Debug.Log(currentHealth);

        if (currentHealth >= maxHealth)
        {
            h75.SetActive(false);
            h50.SetActive(false);
            h25.SetActive(false);
        }

        if (currentHealth <= (maxHealth * 0.75))
        {
            h75.SetActive(true);
            h50.SetActive(false);
            h25.SetActive(false);
        }

        if (currentHealth <= (maxHealth * 0.5))
        {
            h75.SetActive(false);
            h50.SetActive(true);
            h25.SetActive(false);
        }

        if (currentHealth <= (maxHealth * 0.25))
        {
            h75.SetActive(false);
            h50.SetActive(false);
            h25.SetActive(true);
        }

        if (currentHealth <= 0)
        {
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        inMenu = true;
        deathScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
    }

    public void InMenu()
    {
        playerInput.actions.FindActionMap("Player").Disable();
        playerInput.actions.FindActionMap("UI").Enable();

        inMenu = true;
        Cursor.visible = true;
        if (playerHUD != null) playerHUD.SetActive(false);
    }

    public void ExitedMenu()
    {
        playerInput.actions.FindActionMap("Player").Enable();

        inMenu = false;
        Cursor.visible = false;
        if (playerHUD != null) playerHUD.SetActive(true);
    }
}