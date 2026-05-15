using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerData;

public class GunMaster : MonoBehaviour
{
    public PlayerController playerController;
    [HideInInspector] public Transform cameraTransform;

    public GunData gunData;

    private float currentAmmo = 0;
    private float NextTimeToFire = 0;

    public bool isReloading = false;
    public bool isShooting = false;

    // bullet behaviour
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform barrelTransform;
    [SerializeField] private float bulletMissDistance = 25f;


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
            TryShoot();
        }

        if (isReloading)
        {
            TryReload();
        }
    }

    public void TryReload()
    {
        if (isReloading)
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
        yield return new WaitForSeconds(gunData.reloadTime);

        if(PlayerData.iRifle < gunData.magSize)
        {
            currentAmmo = PlayerData.iRifle;
        }

        else
        {
            currentAmmo = gunData.magSize;
        }

        isReloading = false;
        Debug.Log(gunData.gunName + " reloaded");
    }

    public void TryShoot()
    {
        if (isReloading)
        {
            Debug.Log("reloading.");
            return;
        }

        if (currentAmmo <= 0f)
        {
            Debug.Log("Reload " + gunData.gunName + " with 'R'");
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
        PlayerData.iRifle--;
        Debug.Log(currentAmmo + " bullets left");
        Debug.Log(PlayerData.iRifle + " total");
        Shoot();

    }
    // things that still need to happen when firing, even if no hit target like recoil/ screen shake, etc.

    private void Shoot()
    {
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity);
        BulletController bulletController = bullet.GetComponent<BulletController>();

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, gunData.shootingRange))
        {
            bulletController.target = hit.point;
            bulletController.hit = true;

            if(hit.collider.gameObject.TryGetComponent<VivisectorAI>(out VivisectorAI vComponent))
            {
                Debug.Log("You hit " + hit.transform.name);
                vComponent.TakeDamage(gunData.bulletDamage);
            }
        }

        else
        {
            bulletController.target = cameraTransform.position + cameraTransform.forward * bulletMissDistance;
            bulletController.hit = true;
        }

    }

}