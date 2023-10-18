using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="PickupList", menuName = "PickupList")]
public class PickupList : ScriptableObject
{
    public List<GameObject> pickupList;
    private void Awake()
    {
        List<GameObject> pickupList = new List<GameObject>();
    }

    private void OnEnable()
    {
        pickupList.Clear();
    }

    public int Count()
    {
        return pickupList.Count;
    }

    public void Add(GameObject gameObject)
    {
        pickupList.Add(gameObject);
    }

    public GameObject GetLast() //test method- seeing if i can return from the scriptableobject
    {
        return pickupList[pickupList.Count - 1];
    }

    
}
