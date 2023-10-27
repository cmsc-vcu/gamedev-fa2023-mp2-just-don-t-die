using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public Rigidbody2D rb;
    public PickupList pickupList;
    public Image img;

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
        var temp = img.color;
        temp.a = 1;
        img.color = temp;
    }
}
