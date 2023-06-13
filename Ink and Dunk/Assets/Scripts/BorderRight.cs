using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderRight : MonoBehaviour
{
    void Awake()
    {
        
        
        // Kameranýn sað kenarýnýn dünya koordinatlarýný al
        Vector3 cameraRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, Camera.main.nearClipPlane));

        // Nesnenin boyutunu al
        Vector2 objectSize = GetComponent<BoxCollider2D>().bounds.size;

        // Nesneyi kameranýn sað ortasýna sabitle
        Vector3 targetPosition = new Vector3(cameraRightEdge.x - objectSize.x -0.10f / 2f, cameraRightEdge.y, cameraRightEdge.z);
        transform.position = targetPosition;
        
    }
    
}
