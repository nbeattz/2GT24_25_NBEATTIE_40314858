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

    
    public float pathUpdateInterval = 5f; 
    private float lastPathUpdateTime = 0f; 

    // Sound effect
    public AudioSource audioSource; 
    public AudioClip soundEffect; 
    private float lastSoundTime = 0f; 
    public float soundDelay = 10f; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = DifficultyManager.enemySpeed; 
        }
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        // Calculates distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        
        if (distanceToPlayer <= 10f)
        {
            pathUpdateInterval = 0f; 

            
            if (Time.time - lastSoundTime >= soundDelay)
            {
                PlaySound();
            }
        }
        else
        {
            pathUpdateInterval = 5f;
        }

        
        if (Time.time - lastPathUpdateTime >= pathUpdateInterval)
        {
            agent.destination = player.position; 
            lastPathUpdateTime = Time.time; 
        }

       
        if (animator != null)
        {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            
            animator.SetFloat("horizontal", localVelocity.x);
            animator.SetFloat("vertical", localVelocity.z);
        }

        
        if (distanceToPlayer <= attackDistance)
        {
            if (animator != null)
            {
                animator.SetBool("isAttacking", true); 
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("isAttacking", false); 
            }
        }
    }

    
    private void PlaySound()
    {
        if (audioSource != null && soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect); 
            lastSoundTime = Time.time; 
        }
    }
}
