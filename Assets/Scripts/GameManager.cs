using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    private MenuUIHandler menuUIHandler;

    [SerializeField] private const string defaultName = "defaultName";
    private string playerNameInput;
    public int currentScore { get; private set; }
    public ScoreData scoreData { get; private set; }

    public SettingsData settingsData;

    private void Awake()
    {
        
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        

        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitialiseData();
        LoadSettingsData();
    }

    private void InitialiseData()
    {
        currentScore = 0;
        playerNameInput = defaultName;

        LoadGame(this.playerNameInput);
        scoreData.bestScore = 0;
        scoreData.lastScore = 0;
        SaveScoreData();
    }

    private void Start()
    {
        menuUIHandler = GameObject.FindObjectOfType<MenuUIHandler>();
        LoadPlayerData();
    }

    public void UpdateplayerNameInput(string inputName)
    {
        string onlyLetters = Regex.Replace(inputName.ToLowerInvariant(), @"[^a-z]+", String.Empty);
        playerNameInput = onlyLetters;
        //Debug.Log("Player name is now : " + onlyLetters);
        LoadPlayerData();
    }

    public void LoadPlayerData()
    {
        LoadGame(this.playerNameInput);
        //Debug.Log("Best score of " + playerNameInput + " = " +scoreData.bestScore);
        menuUIHandler.UpdateScores(scoreData.bestScore, scoreData.lastScore);
    }

    public string GetCurrentPlayerName()
    {
        return scoreData.playerName;
    }

    public int GetCurrentPlayerBestScore()
    {
        return scoreData.bestScore;
    }
    public int GetCurrentPlayerLastScore()
    {
        return scoreData.lastScore;
    }

    public void SetCurrentPlayerLastScore()
    {
        scoreData.lastScore = currentScore;
        if(currentScore > scoreData.bestScore)
        {
            scoreData.bestScore = currentScore;
        }
    }

    public void SetCurrentScore(int value)
    {
        currentScore = value;
        SaveScoreData();
    }

    private void LoadGame(string inputName)
    {
        
        string path = "Saves/"+inputName+".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            scoreData = JsonUtility.FromJson<ScoreData>(json);
            if(scoreData == null)
            {
                scoreData = new ScoreData();
            }
        }
        else
        {
            string nameOfPlayer;
            if (inputName != "")
            {
                nameOfPlayer = char.ToUpperInvariant(inputName[0]) + inputName.Substring(1);
            }
            else
            {
                nameOfPlayer = defaultName;
            }
            
            scoreData = new ScoreData();
            scoreData.playerName = nameOfPlayer;
            SaveScoreData();
        }
    }

    public void LoadSettingsData()
    {
        string path = "Saves/settings.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            settingsData = JsonUtility.FromJson<SettingsData>(json);
            //Debug.Log("We load settings : " + settingsData.ToString());
            if (settingsData == null)
            {
                settingsData = new SettingsData();
                SaveSettingsData();
            }
        }
        else
        {
            settingsData = new SettingsData();
            SaveSettingsData();
        }


        this.GetComponent<AudioSource>().volume = settingsData.musicVolume;
    }

    public void SaveScoreData()
    {
        string jsonText = JsonUtility.ToJson(scoreData);
        File.WriteAllText("Saves/" + playerNameInput + ".json", jsonText);
    }

    [System.Serializable]
    public class ScoreData
    {
        public string playerName = "Defaultname";
        public int bestScore = 0;
        public int lastScore = 0;
    }

    public void SaveSettingsData()
    {
        settingsData.musicVolume = this.GetComponent<AudioSource>().volume;
        string jsonText = JsonUtility.ToJson(settingsData);
        File.WriteAllText("Saves/settings.json", jsonText);
    }

    [System.Serializable]
    public class SettingsData
    {
        public string difficulty = "medium";
        public float musicVolume = 0.2f;
        public float effectVolume = 0.6f;
    }

    private void OnDisable()
    {
        SaveScoreData();
        SaveSettingsData();
    }

    public void setDifficulty(string value)
    {
        settingsData.difficulty = value;
    }
}
