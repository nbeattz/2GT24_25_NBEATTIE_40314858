using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMushroom : MonoBehaviour
{
    public static int mushroomCollected = 0;

    // Start is called before the first frame update
    void Start()
    {

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

        // Check for win condition
        if (mushroomCollected >= 15)
        {
            Debug.Log("You win!"); // Display win message 
        }
    }
}