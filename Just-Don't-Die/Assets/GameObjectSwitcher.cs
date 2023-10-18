using UnityEngine;

public class GameObjectSwitcher : MonoBehaviour
{
    public GameObject gameObject1;
    public GameObject gameObject2;

    private GameObject activeObject;
    private Vector3 lastPosition;

    private bool isControllingObject1 = true; // Start controlling gameObject1

    void Start()
    {
        // Set the initial active object and position
        activeObject = gameObject1;
        gameObject2.SetActive(false);
        lastPosition = gameObject1.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Toggle control between the two GameObjects
            isControllingObject1 = !isControllingObject1;

            // Disable the current active object
            activeObject.SetActive(false);

            // Update the active object and position
            if (isControllingObject1)
            {
                activeObject = gameObject1;
            }
            else
            {
                activeObject = gameObject2;
            }

            // Enable the new active object
            activeObject.SetActive(true);

            // Set the position of the new active object
            activeObject.transform.position = lastPosition;
        }

        // Update the last position of the currently active object
        lastPosition = activeObject.transform.position;
    }
    public GameObject GetActiveObject()
    {
        return activeObject;
    }
}
