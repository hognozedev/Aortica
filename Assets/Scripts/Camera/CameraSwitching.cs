using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.UI;

public class CameraSwitching : MonoBehaviour
{
    public PlayerController player;
    public Image reticleHip;
    public Image reticleAim;

    private CinemachineCamera aimCamera;
    [HideInInspector] public bool isAiming;

    int priorityBoostAmount = 10;

    private void Awake()
    {
        aimCamera = GetComponent<CinemachineCamera>();

        isAiming = false;
        reticleHip.enabled = true;
        reticleAim.enabled = false;
    }

    public void Update()
    {
        if (InputManager.instance.AimInput && !isAiming) StartAim();
        if (InputManager.instance.AimCancel && isAiming) CancelAim();

    }

    private void StartAim()
    {
        if (Time.timeScale == 0) return;

        isAiming = true;
        aimCamera.Priority += priorityBoostAmount;
        reticleAim.enabled = true;
        reticleHip.enabled = false;

    }

    private void CancelAim()
    {
        isAiming = false; 

        aimCamera.Priority -= priorityBoostAmount;
        reticleAim.enabled = false;
        reticleHip.enabled = true;
    }
// adds 10 to the priority order in order to ensure it is well above the current highest (which is 2)

}