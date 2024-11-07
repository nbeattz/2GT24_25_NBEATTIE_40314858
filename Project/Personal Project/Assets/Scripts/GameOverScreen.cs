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
            SceneManager.LoadScene("Scene 1");
        }
    }
}
