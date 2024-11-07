using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{
    private bool isGameOver = false; 

 
    public void RestartGame()
    {
        Time.timeScale = 1; 

        
        SceneManager.LoadScene("My Game");
    }
    
    public void GameOver()
    {
        isGameOver = true; 
        Time.timeScale = 0; 
    }
}
