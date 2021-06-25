using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
   
    void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float ratio = (float)Screen.width / Screen.height;
        float scaleWidth = ratio / ((float)16/9);
        float scaleHeight = 1 / scaleWidth;
        if(scaleWidth < 1)
        {
            rect.height = scaleWidth;
            rect.y = (1 - scaleWidth) / 2;
        }
        else
        {
            rect.width = scaleHeight;
            rect.x = (1 - scaleHeight) / 2;
        }
        camera.rect = rect;
    }
}
