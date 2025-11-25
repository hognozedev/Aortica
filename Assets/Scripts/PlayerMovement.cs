using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction pMove;

    [SerializeField] float speed = 5;
// this is the same as an Unreal public variable!

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        pMove = playerInput.actions.FindAction("Move");
    }


    void Update()
    {
        MovePlayer();
    }
// Every frame call the MovePlayer function

    void MovePlayer()
    {
        Vector2 direction = pMove.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime;
    }

}
