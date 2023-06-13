using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ballin"))
        {
            gameObject.SetActive(false);
            _GameManager.Continue(transform.position);
            TimeScaler.currentTime = 6f;
        }

        else if (collision.gameObject.CompareTag("GameOver"))
        {
            _GameManager.GameOver();
            gameObject.SetActive(false);
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            _GameManager.BallTouchBorder();
        }
    }
}
