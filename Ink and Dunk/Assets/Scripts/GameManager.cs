using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    private const string MusicEnabledKey = "IsMusicEnabled";
    
    [Header("GENERAL.")]
    [SerializeField] private ThrowingBall _ThrowingBall;
    [SerializeField] private DrawLine _Drawline;
    [Header("SOUNDS.")]
    [SerializeField] private AudioSource BasketSound;
    [SerializeField] private AudioSource BallJumpSound;
    [SerializeField] private AudioSource FailSound;
    [SerializeField] private AudioSource MenuSound;
    [Header("UI PANELS.")]
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private GameObject NewText;
    [SerializeField] private Toggle MusicToggle;
    [SerializeField] private GameObject inGameTime;
    public GameObject[] RemainingDrawList;
    public GameObject[] RemainingDrawOffList;
    [Header("SCORE.")]
    public Text FirstPanelBestScore;
    public Text LastPanelBestScore;
    public Text LastPanelScore;
    public GameObject InGameScoreObject;
    public Text InGameScore;
    [Header("EFFECTS.")]
    [SerializeField] private ParticleSystem HoopEffect;
    public bool hasGameStart = false;
    public static int BasketScore;
    
    void Start()
    {
        BasketScore = 0;
        bool isMusicEnabled = PlayerPrefs.GetInt(MusicEnabledKey, 1) == 1;
        MusicToggle.isOn = isMusicEnabled;

        // Müziði duruma göre ayarla
        SetMusicState(isMusicEnabled);

        // Toggle'ýn deðer deðiþimini dinle
        MusicToggle.onValueChanged.AddListener(OnToggleValueChanged);



        if (PlayerPrefs.HasKey("BestScore"))
        {
            FirstPanelBestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
            LastPanelBestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("BestScore", 0);
            FirstPanelBestScore.text = "0";
            LastPanelBestScore.text = "0";
        }

        if (PlayerPrefs.GetInt("tryAgain") == 1)
        {
            GameStart();
        }
        else
        {
            Panels[0].SetActive(true);
        }
       
    }

    

    public void Continue(Vector2 Pos)
    {
        HoopEffect.transform.position = Pos;
        HoopEffect.gameObject.SetActive(true);
        HoopEffect.Play();

        
        BasketScore++;
        BasketSound.Play();
        InGameScore.text = BasketScore.ToString();
        _ThrowingBall.Continue();
        _Drawline.Continue();
    }

    public void GameOver() // if player failed we need call this func and set score and if player has a new  bestscore set bestscore panels.
    {
        hasGameStart = false;
        MenuSound.Stop();
        FailSound.Play(); 
       if (BasketScore > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", BasketScore);
            NewText.SetActive(true);
        } 

        LastPanelBestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
        LastPanelScore.text = BasketScore.ToString();

        Panels[1].SetActive(true);
        InGameScoreObject.SetActive(false);
        InGameScoreObject.SetActive(false);
        RemainingDrawList[0].SetActive(false);
        RemainingDrawOffList[0].SetActive(false);
        RemainingDrawList[1].SetActive(false);
        RemainingDrawOffList[1].SetActive(false);
        inGameTime.SetActive(false);

    }

    public void GameStart() //we call this func for starting the game.
    {
        hasGameStart = true;
        MenuSound.Play();
        InGameScoreObject.SetActive(true);
        RemainingDrawList[0].SetActive(true);
        RemainingDrawOffList[0].SetActive(true);
        RemainingDrawList[1].SetActive(true);
        RemainingDrawOffList[1].SetActive(true);
        inGameTime.SetActive(true);

        Panels[0].SetActive(false);
        

        
        _ThrowingBall.GameStart();
    }

    public void RestartGame()   //second panel try again button. we set "tryAgain" playerpref 1 because if player touch the tryagain button we need close 0 index panel.
    {
        
        Panels[1].SetActive(false);
        PlayerPrefs.SetInt("tryAgain",1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);



    }
    
    public void menu()         //panels[1] menu button
    {
        Panels[1].SetActive(false);
        PlayerPrefs.SetInt("tryAgain", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void BallTouchBorder()
    {
        BallJumpSound.Play();
    }

    public void GameQuit()          //if game closed with by close button tryagain value set 0 because this value controlled first panel.
    {
        
        PlayerPrefs.SetInt("tryAgain", 0);
        Application.Quit();
    }

    private void OnApplicationQuit()  //if game closed without close button. 
    {
        
        PlayerPrefs.SetInt("tryAgain", 0);
        
    }

    private void OnToggleValueChanged(bool isOn)
    {
        // Toggle durumunu kaydet
        PlayerPrefs.SetInt(MusicEnabledKey, isOn ? 1 : 0);

        // Müziði duruma göre ayarla
        SetMusicState(isOn);
    }

    private void SetMusicState(bool isEnabled)
    {
        BasketSound.mute = !isEnabled;
        BallJumpSound.mute = !isEnabled;
        FailSound.mute = !isEnabled;
        MenuSound.mute = !isEnabled;
    }

    public void PressSettingsButton() //if player pressed settings button we open Panels[2]
    {
        Panels[2].SetActive(true);
    }

    public void CloseSettingsPanelButton() // we need  this button for close the settings panel.
    {
        Panels[2].SetActive(false);
    }

    public void ResetSaveGameButton()
    {
        PlayerPrefs.SetInt("BestScore",0);
        FirstPanelBestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
        LastPanelBestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
    }
}
