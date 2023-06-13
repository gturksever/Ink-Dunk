using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBalls : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MenuBorder"))
        {
            float angle = Random.Range(40f, 140f);
            int Power = Random.Range(200, 1450);
            Vector3 Pos = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            gameObject.GetComponent<Rigidbody2D>().AddForce(Pos * Power);
        }
        
    }

}
