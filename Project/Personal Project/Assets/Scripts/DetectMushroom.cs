using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DetectMushroom : MonoBehaviour
{
    public static int mushroomCollected = 0;

    public TMP_Text scoreText;
    public AudioClip mushroomSound; // Sound effect for Mushroom
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        mushroomCollected = 0;
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        UpdateScoreText();
    }

    // Mushroom Gathered
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Smushroom"))
        {
            Destroy(other.gameObject);
            mushroomCollected += 1; // Add 1 point for Smushroom
            PlaySound();
            Debug.Log("Mushroom Collected " + mushroomCollected);
        }

        if (other.CompareTag("Lmushroom"))
        {
            Destroy(other.gameObject);
            mushroomCollected += 2; // Add 2 points for Lmushroom
            PlaySound();
            Debug.Log("Mushroom Collected " + mushroomCollected);
        }

        UpdateScoreText();
        // Check for win condition
        if (mushroomCollected >= 15)
        {
            Debug.Log("You win!");

            SceneManager.LoadScene("Winner");
        }
    }

    private void UpdateScoreText()
    {
        // Update the score text to show the current score out of 15
        scoreText.text = "Score: " + mushroomCollected + "/15";
    }
    private void PlaySound()
    {
        if (mushroomSound != null) // Check if the AudioClip is assigned
        {
            audioSource.clip = mushroomSound; // Assign the AudioClip
            audioSource.Play(); // Play the sound
        }
    }

}