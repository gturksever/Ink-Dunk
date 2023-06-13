using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderLeft : MonoBehaviour
{
    void Awake()
    {


        // Kameran�n sol kenar�n�n d�nya koordinatlar�n� al
        Vector3 cameraLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0,0.5f , Camera.main.nearClipPlane));

        // Nesnenin boyutunu al
        Vector2 objectSize = GetComponent<BoxCollider2D>().bounds.size;

        // Nesneyi kameran�n sol ortas�na sabitle
        Vector3 targetPosition = new Vector3(+cameraLeftEdge.x + objectSize.x +0.10f / 2f, cameraLeftEdge.y, cameraLeftEdge.z);
        transform.position = targetPosition;

    }
}
