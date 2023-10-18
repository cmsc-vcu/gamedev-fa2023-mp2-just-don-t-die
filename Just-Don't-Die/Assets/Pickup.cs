using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Rigidbody2D rb;
    public PickupList pickupList;
    public GameObject player;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        pickupList.Add(gameObject);
        Destroy(gameObject);
    }
}
