using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    /*
    public Vector2 MoveInput { get; private set; }
    public bool IsMoving { get; private set; }
    public bool SprintInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool ReloadInput { get; private set; }
    public bool InteractInput { get; private set; }
    public bool InventoryInput { get; private set; }
    public bool ClickInput { get; private set; }
    public bool CancelInput { get; private set; }
    public float ScrollInput { get; private set; }
    public bool OneInput { get; private set; }
    public bool TwoInput { get; private set; }
    public bool ThreeInput { get; private set; }
    public bool EscapeInput { get; private set; }
    public bool AimInput { get; private set; }
    public bool AimCancel { get; private set; }
    public bool DebugInput { get; private set; }

    */

    public static InputManager instance;
    public static PlayerInput playerInput;

    private InputAction moveAction, sprintAction, clickAction, inventoryAction, aimAction, attackAction, reloadAction, interactAction;
    private InputAction cancelAction, debugAction, scrollAction, oneAction, twoAction, threeAction, escapeAction;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        sprintAction = playerInput.actions["Sprint"];
        attackAction = playerInput.actions["Attack"];
        reloadAction = playerInput.actions["Reload"];
        interactAction = playerInput.actions["Interact"];
        inventoryAction = playerInput.actions["Inventory"];
        clickAction = playerInput.actions["Click"];
        cancelAction = playerInput.actions["Cancel"];
        scrollAction = playerInput.actions["Scroll"];
        oneAction = playerInput.actions["Key1"];
        twoAction = playerInput.actions["Key2"];
        threeAction = playerInput.actions["Key3"];
        escapeAction = playerInput.actions["PauseMenu"];
        aimAction = playerInput.actions["Aim"];
        debugAction = playerInput.actions["DEBUG"];
    }

    /*
    private void Update()
    {
        MoveInput = moveAction.ReadValue<Vector2>();
        IsMoving = moveAction.WasPressedThisFrame();
        SprintInput = sprintAction.WasPressedThisFrame();
        AttackInput = attackAction.WasPressedThisFrame();
        ReloadInput = reloadAction.WasPressedThisFrame();
        InteractInput = interactAction.WasPressedThisFrame();
        InventoryInput = inventoryAction.WasPressedThisFrame();
        ClickInput = clickAction.WasPressedThisFrame();
        CancelInput = cancelAction.WasPressedThisFrame();
        ScrollInput = scrollAction.ReadValue<float>();
        OneInput = oneAction.WasPressedThisFrame();
        TwoInput = twoAction.WasPressedThisFrame();
        ThreeInput = threeAction.WasPressedThisFrame();
        EscapeInput = escapeAction.WasPressedThisFrame();
        AimInput = aimAction.WasPressedThisFrame();
        AimCancel = aimAction.WasReleasedThisFrame();

        DebugInput = debugAction.WasReleasedThisFrame();
    }
    */

}