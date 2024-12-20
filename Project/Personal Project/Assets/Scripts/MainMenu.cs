using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Difficulty");
    }
    public void PlayEasy()
    {
        DifficultyManager.SetDifficulty(2); 
        SceneManager.LoadScene("My game");
    }

    public void PlayMedium()
    {
        DifficultyManager.SetDifficulty(4); 
        SceneManager.LoadScene("My game");
    }

    // Called when Hard is selected
    public void PlayHard()
    {
        DifficultyManager.SetDifficulty(6); 
        SceneManager.LoadScene("My game");
    }

    // Go to the How to Play scene
    public void GoToHowtoPlayMenu()
    {
        SceneManager.LoadScene("HowtoPlay");
    }

    // Go to the Main Menu scene
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
