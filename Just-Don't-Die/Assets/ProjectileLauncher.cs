using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public float launchForce = 100f;
    public GameObjectSwitcher gameObjectSwitcher;

    void Update()
    {
        if (gameObjectSwitcher == null)
        {
            Debug.LogError("GameObjectSwitcher reference not set in the inspector.");
            return;
        }

        GameObject activeObject = gameObjectSwitcher.GetActiveObject();
        if (activeObject == null)
        {
            Debug.LogWarning("No active object found.");
            return;
        }

        if (activeObject == gameObjectSwitcher.gameObject1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LaunchProjectile();
            }
        }
        else
        {
            Debug.LogWarning("Cannot shoot projectiles. Active object is not gameObject1.");
        }
    }






    void LaunchProjectile()
    {
        // Instantiate the projectile at the launch point
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);

        // Check if the projectile is null (e.g., if instantiation failed)
        if (projectile == null)
        {
            Debug.LogError("Failed to instantiate the projectile.");
            return;
        }

        // Get the Rigidbody2D component of the projectile
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();

        // Check if the projectileRigidbody is not null before applying force
        if (projectileRigidbody != null)
        {
            // Apply the launch force
            projectileRigidbody.AddForce(launchPoint.right * launchForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogError("Rigidbody2D not found in the projectile prefab.");
            // Destroy the projectile if Rigidbody2D is missing
            Destroy(projectile);
        }
    }

}
