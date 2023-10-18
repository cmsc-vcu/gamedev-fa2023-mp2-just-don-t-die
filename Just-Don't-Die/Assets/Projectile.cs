using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Speed of the projectile
    public float lifespan = 5f; // Time before the projectile is destroyed

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the projectile on collision
        Destroy(gameObject);
    }
}
