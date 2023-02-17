using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMouseScript : MonoBehaviour
{
    Vector3 mousePosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z += Camera.main.nearClipPlane;
        
        //worldPosition.x *= Camera.main.orthographicSize;
       // worldPosition.y *= Camera.main.scaledPixelHeight;

        gameObject.transform.position = mousePosition;
    }
}
