using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderLeft : MonoBehaviour
{
    void Awake()
    {


        // Kameranýn sol kenarýnýn dünya koordinatlarýný al
        Vector3 cameraLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0,0.5f , Camera.main.nearClipPlane));

        // Nesnenin boyutunu al
        Vector2 objectSize = GetComponent<BoxCollider2D>().bounds.size;

        // Nesneyi kameranýn sol ortasýna sabitle
        Vector3 targetPosition = new Vector3(+cameraLeftEdge.x + objectSize.x +0.10f / 2f, cameraLeftEdge.y, cameraLeftEdge.z);
        transform.position = targetPosition;

    }
}
