using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] Button easyButton;
    [SerializeField] GameObject easySelected;
    [SerializeField] Button mediumButton;
    [SerializeField] GameObject mediumSelected;
    [SerializeField] Button hardButton;
    [SerializeField] GameObject hardSelected;

    private GameManager GM;

    private void Start()
    {
        //Debug.Log("Start of Difficulty manager");
        GM = GameManager.Instance;
        ActualizeDifficulty(GM.settingsData.difficulty);
    }

    public void SetDifficultyEasy()
    {
        GM.setDifficulty("easy");
        easyButton.interactable = false;
        easySelected.SetActive(true);
        mediumButton.interactable = true;
        mediumSelected.SetActive(false);
        hardButton.interactable = true;
        hardSelected.SetActive(false);
    }

    public void SetDifficultyMedium()
    {
        GM.setDifficulty("medium");
        easyButton.interactable = true;
        easySelected.SetActive(false);
        mediumButton.interactable = false;
        mediumSelected.SetActive(true);
        hardButton.interactable = true;
        hardSelected.SetActive(false);
    }

    public void SetDifficultyHard()
    {
        GM.setDifficulty("hard");
        easyButton.interactable = true;
        easySelected.SetActive(false);
        mediumButton.interactable = true;
        mediumSelected.SetActive(false);
        hardButton.interactable = false;
        hardSelected.SetActive(true);
    }

    public void ActualizeDifficulty(string difficulty)
    {
        switch (difficulty)
        {
            case "easy":
                SetDifficultyEasy();
                break;
            case "medium":
                SetDifficultyMedium();
                break;
            case "hard":
                SetDifficultyHard();
                break;
            default:
                SetDifficultyMedium();
                break;
        }
    }

}
