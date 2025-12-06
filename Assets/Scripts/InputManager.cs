using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    // this is the only input manager in the game so im defining it as a 'singleton'

    public static InputManager Instance
    {
        get 
        {
            return _instance; 
        }
    }
    // this is called an 'accessor' meaning that whenever i call this function from another script it makes sure to use '_instance' correctly.

    private PlayerControls playerControls;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        // make sure that this script only exists once, and if not to assign the variable to this script.

        playerControls = new PlayerControls();

    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }


    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Move.ReadValue<Vector2>(); 
    }
    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }




}
