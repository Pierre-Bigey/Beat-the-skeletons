using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainUIHandler : MonoBehaviour
{
    MainManager mainManager;

    [Header("HUD")]
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Slider healthBar;

    [Header("PauseMenu")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject classicMenu;

    [Header("OptionMenu")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectVolumeSlider;

    private GameManager GM;
    private PlayerManager playerManager;
    private AudioSource canvaAudioSource;
    private string playerName;
    private float maxHealth = 7;

    private PlayerAudio playerAudio;

    private void Awake()
    {
        
        
        
        canvaAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GM = GameManager.Instance;
        Debug.Log("Starting MainUIHandler");
        playerName = GM.GetCurrentPlayerName();
        UpdateScoreText();
        //GM.SaveGame();

        playerAudio = GameObject.FindObjectOfType<PlayerAudio>();
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        mainManager = GameObject.FindObjectOfType<MainManager>();
        SetOptionMenuVisibility(false);

        maxHealth = playerManager.health;
    }

    public void Resume()
    {
        mainManager.Play();
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    public void updatePauseMenu(bool value)
    {
        pauseMenu.SetActive(value);
        healthBar.gameObject.SetActive(!value);
    }

    public void updateDeathMenu(bool value)
    {
        deathMenu.SetActive(value);
        
    }

    public void UpdateHealthBar()
    {
        healthBar.value = playerManager.health/maxHealth;
    }

    public void UpdateScoreText()
    {
        int score = GM.currentScore;
        scoreText.SetText(playerName + " : " + score);
    }

    public void SetOptionMenuVisibility(bool value)
    {
        Debug.Log("in MainManager, setting cursor as : " + value);
        optionMenu.SetActive(value);
        classicMenu.SetActive(!value);
        if (value)
        {
            musicVolumeSlider.value = GM.GetComponent<AudioSource>().volume;
            effectVolumeSlider.value = GM.settingsData.effectVolume;
        }
    }

    public void backOption()
    {
        SetOptionMenuVisibility(false);
    }

    public void SetMusicVolume(float volumeInput)
    {
        GM.GetComponent<AudioSource>().volume = volumeInput;
    }

    public void SetEffectVolume(float volumeInput)
    {
        GM.settingsData.effectVolume = volumeInput;
        canvaAudioSource.volume = volumeInput;
        playerAudio.SetEffectVolume(volumeInput);
        SkeletonAudio[] listSKAudio = GameObject.FindObjectsOfType<SkeletonAudio>();
        foreach (SkeletonAudio skAudio in listSKAudio)
        {
            skAudio.SetEffectVolume(volumeInput);
        }
    }

    public void PlayButtonSound()
    {
        canvaAudioSource.PlayOneShot(canvaAudioSource.clip);
    }
    private void OnDisable()
    {
        GM.SetCurrentPlayerLastScore();
    }
}
