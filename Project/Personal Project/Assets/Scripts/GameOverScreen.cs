using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false); 
            Debug.Log("Game Over");

            Time.timeScale = 0; // Pause the game

            // Load the Game Over scene
            SceneManager.LoadScene("GameOver");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Resume the game

        // Load the main game scene
        SceneManager.LoadScene("My Game");
    }
}