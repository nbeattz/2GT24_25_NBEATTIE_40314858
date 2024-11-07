using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DetectMushroom : MonoBehaviour
{
    public static int mushroomCollected = 0;

    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Mushroom Gathered
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Smushroom"))
        {
            Destroy(other.gameObject);
            mushroomCollected += 1; // Add 1 point for Smushroom
            Debug.Log("Mushroom Collected " + mushroomCollected);
        }

        if (other.CompareTag("Lmushroom"))
        {
            Destroy(other.gameObject);
            mushroomCollected += 2; // Add 2 points for Lmushroom
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

}