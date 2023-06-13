using UnityEngine;
using UnityEngine.UI;

public class TimeScaler : MonoBehaviour
{
    public Text countdownText;
    public float totalTime = 6f; // Geriye say�lacak toplam s�re

    public static float currentTime;

    private void Start()
    {
        currentTime = totalTime;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        // Saniye de�erini g�ncelle
        int seconds = Mathf.FloorToInt(currentTime % 60);
        countdownText.text = seconds.ToString();

        // Geri say�m tamamland���nda i�lemleri ger�ekle�tir
        if (currentTime <= 0)
        {
            countdownText.text = "0";
            // ��lemler burada yap�labilir (�rne�in oyunu ba�latma)
        }
    }
}
