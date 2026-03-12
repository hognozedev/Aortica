using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunMaster : MonoBehaviour
{
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public Transform cameraTransform;

    public GunData gunData;

    private float currentAmmo = 0;
    private float NextTimeToFire = 0;

    public bool isReloading = false;
    public bool isShooting = false;

    // bullet behaviour
    private GameObject bulletPrefab;
    private Transform barrelTransform;
    private Transform bulletParent;
    private float bulletMissDistance = 25f;


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
        bool isShooting = Keyboard.current.fKey.wasPressedThisFrame;
        bool isReloading = Keyboard.current.rKey.wasPressedThisFrame;

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
        GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, gunData.shootingRange))
        {
            Debug.Log(gunData.gunName + "hit" + hit.collider.name);
            target = hit.point;
            boolHit = true;
        }
        else
        {
            target = cameraTransform.position + cameraTransform.forward * bulletMissDistance;
            boolHit = true;
        }

    }

}