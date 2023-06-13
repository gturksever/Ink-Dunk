using UnityEngine;
using UnityEngine.UI;

public class TimeScaler : MonoBehaviour
{
    public Text countdownText;
    public float totalTime = 6f; // Geriye sayýlacak toplam süre

    public static float currentTime;

    private void Start()
    {
        currentTime = totalTime;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        // Saniye deðerini güncelle
        int seconds = Mathf.FloorToInt(currentTime % 60);
        countdownText.text = seconds.ToString();

        // Geri sayým tamamlandýðýnda iþlemleri gerçekleþtir
        if (currentTime <= 0)
        {
            countdownText.text = "0";
            // Ýþlemler burada yapýlabilir (örneðin oyunu baþlatma)
        }
    }
}
