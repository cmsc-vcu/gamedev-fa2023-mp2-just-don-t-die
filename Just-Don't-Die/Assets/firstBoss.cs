using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class firstBoss : MonoBehaviour
{
    public int maxHealth = 100;
    private Rigidbody2D rb;
    private int currentHealth;
    public float attackCooldown = 5.0f; // Cooldown between attacks
    private float lastAttackTime;
    public float attackRange = 10.0f; // Attack range
    private bool playerWasInRange; // Track if player was previously in range
    private bool playerInRange; // Track if player is in range
    public float moveSpeed = 2.0f;
    public int attackPower = 10;
    private Animator anim;



    public Transform object1; // Reference to the player
    public Transform object2;
    public Transform activeObject;

    private Vector3 startPosition;
    public GameObjectSwitcher gameObjectSwitcher;
    private bool isRunning;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("idle");
        currentHealth = maxHealth;
        lastAttackTime = 0; // Initialize to allow immediate attack
        playerInRange = false; // Initialize player in range to false
        startPosition = object2.position;
        if (gameObjectSwitcher == null)
        {
            Debug.LogError("GameObjectSwitcher reference not set in the inspector.");
            return;
        }

        // Set activeObject to object1 at the start
        // Set activeObject to object1 at the start
        if (gameObjectSwitcher.gameObject1 != null)
        {
            Debug.Log(gameObjectSwitcher.gameObject1.name);
            activeObject = gameObjectSwitcher.gameObject1.transform;
        }
    }



    void Update()
    {

        UpdateAnimationState();
       
        if (gameObjectSwitcher != null)
        {
            GameObject activeGameObject = gameObjectSwitcher.GetActiveObject();
            if (activeGameObject != null)
            {
                activeObject = activeGameObject.transform;
                object1 = activeObject;
            }
            else
            {
                Debug.LogWarning("No active object found.");
            }
        }

        bool isPlayerInRange = IsPlayerInRange();

        if (isPlayerInRange && !playerInRange)
        {
            StartAttackSequence();
        }

        playerInRange = isPlayerInRange;

        if (IsPlayerInRange())
        {
           
            moveToPlayer();
        }

        else
        {

            if (Vector3.Distance(startPosition, object2.position) > 2)
            {
               
                rb.velocity = new Vector2(1 * moveSpeed, rb.velocity.y);
              isRunning = true;


            }



        } 
    }


    // Everytime the Player enters the Boss's attack range it will attack them after a breif delay (player must weave in-out of attacks)
    private void StartAttackSequence()
    {
        Debug.Log("starting sequence");
        StartCoroutine(AttackSequence());
       
    }

    private void moveToPlayer()
    {
        isRunning = true;
    
        if (currentHealth < 80)
        {
            moveSpeed = 3;
        }

        if (currentHealth < 20)
        {
            moveSpeed = 4;
        }
       
        rb.velocity = new Vector2(-1 * moveSpeed, rb.velocity.y);
       
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
            playerScript.TakeDamage(attackPower);
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
            if (distance >= attackRange)
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

    private void UpdateAnimationState()
    {
       // anim.SetTrigger("idle");

        float distance = Vector3.Distance(object1.position, object2.position);


        if (distance <= attackRange)
        {
            // state = MovementState.running;
            //sprite.flipX = false;

           anim.SetTrigger("run");
        }
        else
        {
            anim.SetTrigger("idle");
        }

    }
    void Die()
    {
        Debug.Log("Boss defeated!");
        Destroy(gameObject); // Destroy the boss object
    }
}
