using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    public float soundDistance = 10f; // Distance to play sound (10ft)
    public float attackDistance = 5f; // Distance to perform bite attack (5ft)
    private Animator animator;

    // Timer for path update frequency
    public float pathUpdateInterval = 5f; // Default update interval (in seconds)
    private float lastPathUpdateTime = 0f; // Time of last path update

    // Sound effect
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip soundEffect; // Reference to the sound effect to be played
    private float lastSoundTime = 0f; // Timer to manage sound playback delay
    public float soundDelay = 10f; // 10 second delay before sound can play again

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>(); // Ensure AudioSource component is attached
    }

    void Update()
    {
        // Calculate distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is within 10ft, set path update interval to 0 for continuous updates
        if (distanceToPlayer <= 10f)
        {
            pathUpdateInterval = 0f; // Instant path update (no delay)

            // Check if it's time to play the sound
            if (Time.time - lastSoundTime >= soundDelay)
            {
                PlaySound();
            }
        }
        else
        {
            pathUpdateInterval = 5f; // Default update interval (you can adjust this as needed)
        }

        // Update path only if enough time has passed (based on pathUpdateInterval)
        if (Time.time - lastPathUpdateTime >= pathUpdateInterval)
        {
            agent.destination = player.position; // Set agent's destination to player's position
            lastPathUpdateTime = Time.time; // Update the last path update time
        }

        // Update Animator movement parameters
        if (animator != null)
        {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            // Assuming horizontal and vertical map to local X and Z axes
            animator.SetFloat("horizontal", localVelocity.x);
            animator.SetFloat("vertical", localVelocity.z);
        }

        // Handle bite attack animation
        if (distanceToPlayer <= attackDistance)
        {
            if (animator != null)
            {
                animator.SetBool("isAttacking", true); // Trigger the bite animation
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("isAttacking", false); // Stop the bite animation
            }
        }
    }

    // Play sound effect and set the timer for delay
    private void PlaySound()
    {
        if (audioSource != null && soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect); // Play the sound effect once
            lastSoundTime = Time.time; // Update the last sound played time
        }
    }
}
