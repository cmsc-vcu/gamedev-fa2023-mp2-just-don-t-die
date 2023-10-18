using UnityEngine;

public class GameObjectSwitcher : MonoBehaviour
{
    public GameObject gameObject1;
    public GameObject gameObject2;
    public GameObject gameObject3;

    private GameObject activeObject;
    private Vector3 lastPosition;

    void Start()
    {
        // Set the initial active object and position
        activeObject = gameObject1;
        gameObject2.SetActive(false);
        gameObject3.SetActive(false);
        lastPosition = gameObject1.transform.position;
    }

    void Update()
    {
        // Update the last position of the currently active object
        lastPosition = activeObject.transform.position;

        // Toggle control between the three GameObjects
        if (Input.GetKeyDown(KeyCode.Alpha1) && activeObject != gameObject1)
        {
            SwitchToGameObject(gameObject1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && activeObject != gameObject2)
        {
            SwitchToGameObject(gameObject2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && activeObject != gameObject3)
        {
            SwitchToGameObject(gameObject3);
        }
    }

    private void SwitchToGameObject(GameObject targetObject)
    {
        // Disable the current active object
        activeObject.SetActive(false);

        // Update the active object and position
        activeObject = targetObject;

        // Enable the new active object
        activeObject.SetActive(true);

        // Set the position of the new active object
        activeObject.transform.position = lastPosition;
    }

    public GameObject GetActiveObject()
    {
        return activeObject;
    }
}
