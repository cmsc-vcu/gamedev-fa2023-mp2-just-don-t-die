using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Rigidbody2D rb;
    public Pokedex pokedex;
    public GameObject player;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (pokedex.Count() > 3)
        {
            Debug.Log("This works I guess"); //This doesn't work ?? TT
            //player.GetComponent<SpriteRenderer>().sprite = pokedex.GetLast().GetComponent<SpriteRenderer>().sprite;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        pokedex.Add(gameObject);
        Destroy(gameObject);
    }
}
