using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    private float horizontalInput;
    private float verticalInput;
    private Vector2 change; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.MovePosition(rb.position + change);
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        change = new Vector2 (horizontalInput, verticalInput);
    }
}
