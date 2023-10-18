using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    // Initializing Variables for the different Game Objects
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    public AudioSource audio;
    public AudioSource audio2;

    // Enumeration Variable for the different animations
    private enum MovementState { idle, running, jumping, falling, attack1 }

    // Initializing Player Variables
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float baseMoveSpeed = 7f;
    [SerializeField] private float baseJumpForce = 14f;
    private float powerLevel = 0;
    private float moveSpeed;
    private float jumpForce;
   
    [SerializeField] private bool isPlayer1 = true;


    private float dirX;
    public int maxHealth = 10;
    private int currentHealth;
    bool player1_attacking;
    bool player2_attacking;
    private bool canAttack = true;  // Track if the player can attack
    public int attackPower = 10;

    private bool isAttacking; // Added to track attack animation
    private bool isBlocking = false;

    public float playerAttackCooldown = 3.0f; // Cooldown between player attacks
    private float lastPlayerAttackTime;

    public Transform object1;
    public Transform object2;
    public GameObject projectile;
    [SerializeField]  public float detectionRange = 10.0f;
    private bool isDashing = false;
    [SerializeField]  private float dashDuration = 1f;
    [SerializeField] private float dashCooldown = 4f;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashJumpForce = 20f;


    private bool canDash = true;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        lastPlayerAttackTime = 0;
        moveSpeed = baseMoveSpeed; 
        jumpForce = baseJumpForce;
        projectile = GetComponent<GameObject>();
    }

    void Update()
    {
        HandleMovementInput();
        HandleJumping();
        HandleAttack();
        CheckObjectsInRange();
        UpdateAnimationState();
        HandleDashing();
        
        //  UpdateAnimationState();
    }


    private void HandleMovementInput()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }

    private void HandleJumping()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void HandleDashing()
    {
      if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(!canDash)
            {
               Debug.Log("Dash on Cooldown!");
            }
            else
            { 
                Dash();
            }  
        }
    }

    private void Dash()
    {
        if (isDashing)
            return;

        isDashing = true;
        moveSpeed += powerLevel;
        //jumpForce += (powerLevel/3);
        canDash = false;  // Dash is on cooldown

        // Reset dash after the dash duration
        StartCoroutine(ResetDash());

        // Reset dash cooldown after dash cooldown
        StartCoroutine(ResetDashCooldown());
    }

    private IEnumerator ResetDash()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        moveSpeed = baseMoveSpeed; 
        jumpForce = baseJumpForce;
        powerLevel = 0;
    }

    private IEnumerator ResetDashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;  // Dash is available again
    }

    private void HandleAttack()


    {
       

            if (Input.GetKeyDown(KeyCode.Mouse0) && IsObjectsInRange())
        {
            if (!canAttack)
            {
                Debug.Log("Attack on Cooldown!");
            }
            if (Time.time - lastPlayerAttackTime >= playerAttackCooldown && canAttack)
            {
                // Set canAttack to false, preventing rapid attacks
                canAttack = false;

                // Assuming you have a reference to the boss script
                firstBoss bossScript = object2.GetComponent<firstBoss>();

                if (bossScript != null)
                {
                    // Call the TakeDamage method on the boss script to deal damage
                    bossScript.TakeDamage(attackPower);
                     powerLevel+= 5;
                   Debug.Log("Power Level: " + powerLevel);
                }
                else
                {
                    Debug.LogWarning("Boss script not found on object2.");
                }

                StartCoroutine(ResetAttack());
            }
           
        }
    }

private IEnumerator ResetAttack()
{
    yield return new WaitForSeconds(playerAttackCooldown);
    canAttack = true;  // Player can attack again after cooldown
}



    private void CheckObjectsInRange()
    {
        // Check if both objects are not null
        if (object1 != null && object2 != null)
        {
            // Calculate the distance between the objects
            float distance = Vector3.Distance(object1.position, object2.position);

            // Check if the distance is within the detection range
            if (distance <= detectionRange)
            {
               //  Debug.Log("Objects are within range! Distance: " + distance);
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
            return distance <= detectionRange;
        }

        return false;
    }

    public void TakeDamage(int damage)
    {
        // Implement player taking damage logic here
        // For example, deduct health from the player
        currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);

        // Check if the player is defeated
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player defeated!");
        Destroy(gameObject); // Destroy the player object
        // Implement actions for player defeat (e.g., game over, respawn, etc.)
    }

    private void UpdateAnimationState()
    {
        MovementState state = MovementState.idle;

        float dirX = Input.GetAxisRaw("Horizontal");

        if (dirX > 0f)
        {
            // state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            //state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            //  state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            //  state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            // state = MovementState.falling;
        }

    }
    // Method to check is the player is touching the ground
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
