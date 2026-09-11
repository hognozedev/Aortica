using UnityEngine;

public class MeleeSpawn : MonoBehaviour, IInteractable
{
    public PlayerController playerController;
    [SerializeField] private GameObject interactPrompt = null;
    public GameObject prefab;
    public GameObject spawnPos;
    public GameObject weaponHolder;

    public void Interact()
    {
        var newPrefab = Instantiate(prefab, spawnPos.transform.position, Quaternion.identity);
        newPrefab.transform.parent = weaponHolder.transform;

        Destroy(gameObject);
    }

    public void OnFocusGained()
    {
        interactPrompt.gameObject.SetActive(true);
    }

    public void OnFocusLost()
    {
        interactPrompt.gameObject.SetActive(false);
    }
}