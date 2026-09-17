using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using UnityEngine.UI;

public class CameraSwitching : MonoBehaviour
{
    public PlayerController player;
    [SerializeField] private int priorityBoostAmount = 10;
    [SerializeField] private Image reticleHip;
    [SerializeField] private Image reticleAim;

    private CinemachineCamera aimCamera;
    [HideInInspector] public InputAction aimAction;
    [HideInInspector] public bool aiming;

    public void Awake()
    {
        aimCamera = GetComponent<CinemachineCamera>();
        reticleHip.enabled = true;
        reticleAim.enabled = false;
    }

    public void Update()
    {
        if (player.aimAction.WasPressedThisFrame() && !aiming)
        {
            Debug.Log("pressed"); StartAim();
        }

        if (player.aimAction.WasReleasedThisFrame() && aiming)
        {
            Debug.Log("pressed"); CancelAim();
        }
    }

    public void OnEnable()
    {

    }


    private void StartAim()
    {
        if (Time.timeScale == 0) return;

        aiming = true;
        aimCamera.Priority += priorityBoostAmount;
        reticleAim.enabled = true;
        reticleHip.enabled = false;

    }

    private void CancelAim()
    {
        aiming = false; 

        aimCamera.Priority -= priorityBoostAmount;
        reticleAim.enabled = false;
        reticleHip.enabled = true;
    }
// adds 10 to the priority order in order to ensure it is well above the current highest (which is 2)

}