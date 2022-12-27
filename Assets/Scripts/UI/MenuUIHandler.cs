using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    private GameManager GM;
    private AudioSource canvaAudioSource;

    [Header("MainMenu")]
    [SerializeField] private TMP_InputField playerNameInputField;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject classicMenu;
    [SerializeField] private TMP_Text bestScoreText;
    [SerializeField] private TMP_Text lastScoreText;

    [Header("OptionMenu")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectVolumeSlider;

    private void Awake()
    {
        
        canvaAudioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        Debug.Log("Try to unload Main scene");
        try
        {
            SceneManager.UnloadSceneAsync("Main");
        }
        catch
        {

        }

        GM = GameManager.Instance;
        SetOptionMenuVisibility(false);
        SetDefaultPlayerData();
        SetCursorEnabled(true);
    }

    private void Update()
    {
        if (!Cursor.visible)
        {
            SetCursorEnabled(true);
        }
    }

    public void StartNew()
    {
        SceneManager.LoadScene("Main");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    private void SetDefaultPlayerData()
    {
        if(GM.GetCurrentPlayerName() != "Defaultname")
        {
            playerNameInputField.text = GM.GetCurrentPlayerName();
            UpdateScores(GM.GetCurrentPlayerBestScore(), GM.GetCurrentPlayerLastScore());
        }
    }

    public void UpdateScores(int bestScore, int lastScore)
    {
        bestScoreText.SetText("Best Score : " + bestScore);
        lastScoreText.SetText("Last Score : " + lastScore);
    }

    public void SetOptionMenuVisibility(bool value)
    {
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
    }

    public void PlayButtonSound()
    {
        canvaAudioSource.PlayOneShot(canvaAudioSource.clip);
    }

    private void SetCursorEnabled(bool value)
    {
        Debug.Log("in MenuUIManager, setting cursor as : " + value);
        if (value)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        Cursor.visible = value;
    }
}
