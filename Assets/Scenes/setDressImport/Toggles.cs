using UnityEngine;

public class Toggles : MonoBehaviour
{
    public GameObject volumes;
    public AspectRatioLock sidebars;

    public GameObject player;
    public GameObject freecam;

    bool filtersOn;
    bool playerView;

    void Start()
    {
        if (volumes == true) filtersOn = true;
        if (player == true) playerView = true;
    }

    public void ToggleFilters()
    {
        if (filtersOn)
        {
            volumes.SetActive(false);
            sidebars.enabled = false;
            filtersOn = false;
        }

        else if(!filtersOn)
        {
            volumes.SetActive(true);
            sidebars.enabled = true;
            filtersOn = true;
        }

    }

    public void ChangeView()
    {
        if (playerView)
        {
            player.SetActive(false);
            freecam.SetActive(true);
            playerView = false;
        }

        else if (!playerView)
        {
            freecam.SetActive(false);
            player.SetActive(true);
            playerView = true;
        }
    }

}