using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundMovement : MonoBehaviour
{
    public bool move;
    public bool kill;
    private Rigidbody2D rb;
    Vector2 change;
    private float speed = 10f;
    private float horizontalMovement = 0.3f;
    private float interval;
    private float maxInterval = 4f;
    private int counter = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        change = new Vector2(horizontalMovement, 0);
        interval = 0;
        if (!move)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (move)
        {
            if (counter < 40)
            {
                rb.MovePosition(rb.position + change * speed * Time.fixedDeltaTime);
                counter++;
            }
            else
            {
                rb.MovePosition(rb.position - change * speed * Time.fixedDeltaTime);
                counter++;
                if (counter == 80)
                {
                    counter = 0;
                }
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (kill)
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene("Exploration");
        }
    }

}
