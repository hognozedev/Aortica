using System.Collections;
using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject bulletDecal;
    private float speed = 350f;
    private float decalDestroy = 10f;

    public Vector3 target { get; set; }
    public bool hit { get; set; }

    private void OnEnable()
    {
        Destroy(gameObject, 0.2f);

    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        ContactPoint contact = other.GetContact(0);      
        GameObject spawnedObject = Instantiate(bulletDecal, contact.point + contact.normal * .0001f, Quaternion.LookRotation(contact.normal));
        Destroy(spawnedObject, decalDestroy);

        Destroy(gameObject);
    //destroy bullet prefab //only spawn hole decal on environments (requires rigidbody)

    }

}