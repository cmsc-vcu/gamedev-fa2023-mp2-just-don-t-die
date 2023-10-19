using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    private Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        HandleMovement();

    }
    private void HandleMovement()
    {
        float moveInputX = Input.GetAxis("Horizontal");
        float moveInputY = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(moveInputX, moveInputY).normalized;
        rb.velocity = moveDirection * moveSpeed;
    }

}
