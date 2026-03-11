using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunMaster : MonoBehaviour
{
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public Transform cameraTransform;

    [SerializeField] private GameObject bulletPrefab;

    public GameObject bulletDecal;
    public GunData gunData;

    private float currentAmmo = 0;
    private float NextTimeToFire = 0;

    public bool isReloading = false;
    public bool isShooting = false;

// bullet behaviour
    //private float speed = 100f;
    //private float timeToDestroy = 3f;
    //private float CurrentCooldown;
    //private float bulletMissDistance = 75f;

    public Vector3 target { get; set; }


    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        cameraTransform = Camera.main.transform;

        currentAmmo = gunData.magSize;
        isShooting = false;
        isReloading = false;

    }

    public void Update()
    {

        if (isShooting)
        {
            Debug.Log("attack pressed");
            TryShoot();
        }

        if (isReloading)
        {
            Debug.Log("reload pressed");
            TryReload();
        }
    }


    public void TryReload()
    {
        if (isReloading == false)
        {
            if (currentAmmo < gunData.magSize)
            {
                StartCoroutine(Reload());
            }

            else
            {
                Debug.Log("Already full");
            }

        }
    }


    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        currentAmmo = gunData.magSize;
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

        if (currentAmmo <= 0f)
        {
            Debug.Log(gunData.gunName + "empty, please reload");
            return;
        }

        if (Time.time >= NextTimeToFire)
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

    private void Shoot()
    {
        RaycastHit hit;
        //GameObject bullet = GameObject.Instantiate(bulletPrefab, Quaternion.identity, bulletParent);
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, gunData.shootingRange))
        {
            Debug.Log(gunData.gunName + "hit" + hit.collider.name);
        }

    }

}