using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layout : MonoBehaviour
{
    private int length = 10;
    private int height = 4;
    public GameObject prefab;
    private MeshRenderer meshRenderer;
    private Collider2D thing;

    void Start()
    {
        //meshRenderer = prefab.GetComponent<MeshRenderer>();
        thing = prefab.GetComponent<Collider2D>();

        GameObject[,] ground = new GameObject[length, height]; 
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < length; j++)
            {
                //Instantiate(prefab, new Vector2((meshRenderer.bounds.size.x) * j, (meshRenderer.bounds.size.y) * i), Quaternion.identity);
                //Instantiate(prefab, new Vector2(1 + j, 1 + i), Quaternion.identity);
            }
        }



    }

    void Update()
    {
        
    }
}
