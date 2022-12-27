using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public bool isPaused { get; private set; }
    private MainUIHandler mainUIHandler;
    private SpawnerManager spawnerManager;
    [SerializeField] private float spawnRate = 7;
    private PlayerManager playerManager;

    private GameManager GM;

    private void Awake()
    {
        mainUIHandler = GameObject.FindObjectOfType<MainUIHandler>();
        mainUIHandler.updatePauseMenu(false);
        mainUIHandler.updateDeathMenu(false);
        isPaused = false;
    }

    private void Start()
    {
        GM = GameManager.Instance;
        spawnerManager = GameObject.FindObjectOfType<SpawnerManager>();
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        SetDifficultyFloat();
        StartCoroutine(SpawnSkeletons());
    }

    private void SetDifficultyFloat()
    {
        //Debug.Log("Setting Difficulty time");
        string difficultyString = GM.settingsData.difficulty;
        switch (difficultyString)
        {
            case "easy":
                spawnRate = 10;
                break;
            case "medium":
                spawnRate = 7;
                break;
            case "hard":
                spawnRate = 4.5f;
                break;
            default:
                spawnRate = 7;
                break;
        }
        //Debug.Log("difficultyFLoat = " + difficultyFloat);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !playerManager.isDead)
        {
            if (!isPaused)
            {
                Pause();
            }
            
        }
    }

    IEnumerator SpawnSkeletons()
    {
        //Debug.Log("difficultyFLoat = " + difficultyFloat);
        //Debug.Log("Coroutine Started, waiting time : "+ (spawnRate / difficultyFloat));
        yield return new WaitForSeconds(spawnRate);
        if (!playerManager.isDead)
        {
            //Debug.Log("Skeleton Summoned");
            spawnerManager.SummonSkeleton();
            StartCoroutine(SpawnSkeletons());
        }
    }

    private void SetCursorEnabled(bool value)
    {
        Debug.Log("in MainManager, setting cursor as : " + value);
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

    private void Pause()
    {
        SetCursorEnabled(true);
        isPaused = true;
        Time.timeScale = 0;
        mainUIHandler.updatePauseMenu(true);
    }

    public void Play()
    {
        SetCursorEnabled(false);
        isPaused = false;
        Time.timeScale = 1;
        mainUIHandler.updatePauseMenu(false);
    }


}
