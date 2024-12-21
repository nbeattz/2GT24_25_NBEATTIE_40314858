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

    private bool hasReachedEntryFloor = false; 

    
    void Start()
    {
        mushroomCollected = 0;
        UpdateScoreText();
    }

   
    void Update()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        UpdateScoreText();

       
        if (mushroomCollected >= 15 && hasReachedEntryFloor)
        {
            Debug.Log("You win!");

            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SceneManager.LoadScene("Winner"); // Load the winning scene
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        // Mushroom collection triggers
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

        if (other.CompareTag("Magic Mushroom"))
        {
            Destroy(other.gameObject);
            mushroomCollected += 15; // Add 15 points for Magic Mushroom - Win Tester
            PlaySound();
            Debug.Log("Mushroom Collected " + mushroomCollected);
        }

        
        if (other.CompareTag("EntryFloor") && mushroomCollected >= 15)
        {
            hasReachedEntryFloor = true; 
            Debug.Log("Player has reached EntryFloor and collected 15 mushrooms!");
        }

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (mushroomCollected >= 15)
        {
            scoreText.text = "Escape!!!"; 
            scoreText.color = Color.red; 
        }
        else
        {
            scoreText.text = "Mushrooms: " + mushroomCollected + "/15";
            scoreText.color = Color.green; 
        }
    }

    private void PlaySound()
    {
        if (mushroomSound != null) 
        {
            audioSource.clip = mushroomSound; 
            audioSource.Play(); 
        }
    }
}
