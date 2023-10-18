using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Speed of the projectile
    public float lifespan = 2f; // Time before the projectile is destroyed
    public int damage = 10;


    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        // Destroy the projectile after the specified lifespan
        Destroy(gameObject, lifespan);
    }

    void Update()
    {
        // Move the projectile forward if Rigidbody2D is present
        if (rb2D != null)
        {
            rb2D.velocity = transform.right * speed; // Use right for 2D
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the boss
        firstBoss boss = collision.gameObject.GetComponent<firstBoss>();
        if (boss != null)
        {
            // Inflict damage to the boss
            boss.TakeDamage(damage);
        }

        // Destroy the projectile on collision
        Destroy(gameObject);
    }






}
