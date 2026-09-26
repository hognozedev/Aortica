using UnityEngine.InputSystem;

public static class InputStatic
{
    public static PlayerInput playerInput;
    public static InputAction moveAction, sprintAction, clickAction, inventoryAction, aimAction, attackAction, reloadAction, interactAction, cancelAction, debugAction, scrollAction, oneAction, twoAction, threeAction, escapeAction;

    public static void Start()
    {
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

    public static void IsInMenu()
    {
        //link to player functions or make player when a menu is opened (update if tag is active in hierarchy/ hud is active)
    }

}