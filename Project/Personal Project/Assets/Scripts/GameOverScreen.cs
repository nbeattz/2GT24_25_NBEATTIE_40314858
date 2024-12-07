using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public AudioSource audioPlayer;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("Game Over");

            audioPlayer.PlayOneShot(audioPlayer.clip);

            Time.timeScale = 0;            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SceneManager.LoadScene("GameOver");
        }
    }
}

