using UnityEngine;
using UnityEngine.UIElements;

public class AspectRatioLock : MonoBehaviour
{
    void Start()
    {
        Adjust();
    }

    public void Adjust()
    {
        float targetaspect = 4.0f / 3.0f;
        // the ratio size that i want is 4:3

        float windowaspect = (float)Screen.width / (float)Screen.height;
        // the size of the game window itself 

        float scaleheight = windowaspect / targetaspect;

        Camera camera = GetComponent<Camera>();


        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }

        else
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0f;

            camera.rect = rect;
        }

    }
    
}
