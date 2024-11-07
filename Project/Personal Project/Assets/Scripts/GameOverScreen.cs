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
            other.gameObject.SetActive(false); // Deactivate the player object instead of destroying it
            Debug.Log("Game Over");

            Time.timeScale = 0; // Pause the game

            // Load "Scene 1"
            SceneManager.LoadScene("GameOver");
        }
    }
    void Update()
    {
        // Check if the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("My Game");
    }
}
