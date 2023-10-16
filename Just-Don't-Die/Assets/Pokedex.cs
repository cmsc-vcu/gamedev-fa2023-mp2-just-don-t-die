using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Pokedex", menuName = "Pokedex")]
public class Pokedex : ScriptableObject
{
    public List<GameObject> pokedex;
    private void Awake()
    {
        List<GameObject> pokedex = new List<GameObject>();
    }

    private void OnEnable()
    {
        pokedex.Clear();
    }

    public int Count()
    {
        return pokedex.Count;
    }

    public void Add(GameObject gameObject)
    {
        pokedex.Add(gameObject);
    }

    public GameObject GetLast() //test method- seeing if i can return from the scriptableobject
    {
        return pokedex[pokedex.Count - 1];
    }

    
}
