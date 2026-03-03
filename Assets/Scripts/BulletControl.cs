using UnityEngine;

public class BulletControl : MonoBehaviour
{

    [SerializeField] private GameObject bulletDecal;

    private float speed = 50f;
    private float timeToDestroy = 3f;

    public Vector3 target {  get; set; }
    public bool hit {  get; set; }

    private void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }
        // destroy the bullet after the delay.

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        // when called, move the bullet in the direction with the custom set speed

        if (!hit && Vector3.Distance(transform.position, target) < 0.01f )
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        ContactPoint contact = other.GetContact(0);
        GameObject.Instantiate(bulletDecal, contact.point, Quaternion.LookRotation(contact.normal));
        Destroy(gameObject);
    }
}
