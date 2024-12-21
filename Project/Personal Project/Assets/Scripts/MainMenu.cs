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
        Time.timeScale = 1f;
    }

    public void PlayMedium()
    {
        DifficultyManager.SetDifficulty(4); 
        SceneManager.LoadScene("My game");
        Time.timeScale = 1f;
    }

    
    public void PlayHard()
    {
        DifficultyManager.SetDifficulty(6); 
        SceneManager.LoadScene("My game");
        Time.timeScale = 1f;
    }

    
    public void GoToHowtoPlayMenu()
    {
        SceneManager.LoadScene("HowtoPlay");
    }

    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

   
    public void QuitGame()
    {
        Application.Quit();
    }
}
