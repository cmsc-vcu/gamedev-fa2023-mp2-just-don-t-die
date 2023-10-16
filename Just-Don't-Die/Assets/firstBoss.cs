using System.Collections;
using UnityEngine;

public class firstBoss : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public float attackCooldown = 5.0f; // Cooldown between attacks
    private float lastAttackTime;
    public float attackRange = 10.0f; // Attack range
    private bool playerWasInRange; // Track if player was previously in range
    private bool playerInRange; // Track if player is in range

    public Transform object1; // Reference to the player
    public Transform object2;
    void Start()
    {
        currentHealth = maxHealth;
        lastAttackTime = 0; // Initialize to allow immediate attack
        playerInRange = false; // Initialize player in range to false
    }



    void Update()
    {
        bool isPlayerInRange = IsPlayerInRange();

        if (isPlayerInRange && !playerInRange)
        {
            StartAttackSequence();
        }

        playerInRange = isPlayerInRange;
    }


    // Everytime the Player enters the Boss's attack range it will attack them after a breif delay (player must weave in-out of attacks)
    private void StartAttackSequence()
    {  
        Debug.Log("starting sequence");
        StartCoroutine(AttackSequence());
      
    }

    // Incorporate a way to remove terrain during battle
    private IEnumerator AttackSequence()
    {
        yield return new WaitForSeconds(attackCooldown);

        if (IsPlayerInRange())
        {
            AttackPlayer();
        }
    }


    private bool IsPlayerInRange()
    {
        if (object1 != null)
        {
            float distance = Vector3.Distance(transform.position, object1.position);
            return distance <= attackRange;
        }
        return false;
    }

    public void AttackPlayer()
    {
        // Implement the boss's attack logic here
        Debug.Log("Boss attacked the player!");

        PlayerMovement playerScript = object1.GetComponent<PlayerMovement>();
        if (playerScript != null)
        {
            playerScript.TakeDamage(10);
        }
        else
        {
            Debug.LogWarning("Player script not found on object1.");
        }
    }



    private void CheckObjectsInRange()
    {
        // Check if both objects are not null
        if (object1 != null && object2 != null)
        {
            // Calculate the distance between the objects
            float distance = Vector3.Distance(object1.position, object2.position);

            // Check if the distance is within the detection range
            if (distance <= attackRange)
            {
                // Debug.Log("Objects are within range! Distance: " + distance);
            }
        }
    }

    private bool IsObjectsInRange()
    {
        // Check if both objects are not null
        if (object1 != null && object2 != null)
        {
            // Calculate the distance between the objects
            float distance = Vector3.Distance(object1.position, object2.position);

            // Check if the distance is within the detection range
            return distance <= attackRange;
        }

        return false;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if the boss is defeated
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("Boss took " + damage + " damage. Current health: " + currentHealth);
        }
    }
    void Die()
    {
        Debug.Log("Boss defeated!");
        Destroy(gameObject); // Destroy the boss object
    }
}
