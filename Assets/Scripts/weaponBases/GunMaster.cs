using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GunMaster : MonoBehaviour
{
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public Transform cameraTransform;
    //public GameObject bulletDecal;
    //public GameObject bulletPrefab;
    public GunData gunData;

    // gundata behaviour
    private float currentAmmo = 0;
    private float NextTimeToFire = 0;
    private bool isReloading = false;

    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public InputAction attackAction;
    [HideInInspector] public InputAction reloadAction;


    // bullet behaviour
    private float speed = 100f;
    private float timeToDestroy = 3f;
    private float CurrentCooldown;
    private float bulletMissDistance = 75f;


    public Vector3 target { get; set; }


    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        cameraTransform = playerController.cameraTransform.transform;
        attackAction = playerInput.actions["Attack"];
        reloadAction = playerInput.actions["Reload"];

        currentAmmo = gunData.magSize;

    }

    public virtual void Update(){}

    public void TryReload()
    {
        if (isReloading && currentAmmo < gunData.magSize)
        {
            StartCoroutine(Reload());
        }

    }

    private IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log(gunData.gunName + "reloading.....");

        yield return new WaitForSeconds(gunData.reloadTime);

        isReloading = false;

        Debug.Log(gunData.gunName + "done!");

    }

    public void TryShoot()
    {
        if (isReloading)
        {
            Debug.Log(gunData.gunName + "currently reloading.");
            return;
        }

        if(currentAmmo <= 0f)
        {
            Debug.Log(gunData.gunName + "empty, please reload");
            return;
        }

        if(Time.time >= NextTimeToFire)
        {
            NextTimeToFire = Time.time + (1 / gunData.fireRate);
            // e.g. if fire rate is 2 then wait between is 0.5 secs
            HandleShoot();
        }

    }

    private void HandleShoot()
    {
        currentAmmo--;
        Debug.Log(gunData.gunName + "has shot. Bullets left = " + currentAmmo);
        Shoot();

    }
    // things that still need to happen when firing, even if no hit target like recoil/ screen shake, etc.


    public abstract void Shoot();
    // inherits to each child for flexibility, defines what happens on the hit event


}