using UnityEngine;


[RequireComponent(typeof(CharacterController))]
// only executes the following if on a component with a chara controller.
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 5.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        // velocity is the rate of player falling, so this doesnt execute if the player is 'grounded'.


        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        // converts the vector 2 into a vector3, where '0f' is defined as how high off the ground the player is.

        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        // another precaution to make sure the player stays on the ground unless falling/ jumping.

        //if (move != Vector3.zero)
        //{
        //    transform.forward = move;
        //}
        // move in the faced direction (need to define what 'forawrd' is).

        playerVelocity.y += gravityValue * Time.deltaTime;

        // Combine horizontal and vertical movement
        Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);
    }
} 
