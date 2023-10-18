using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public float launchForce = 100f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchProjectile();
        }
    }

    void LaunchProjectile()
    {
        // Instantiate the projectile at the launch point
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);

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
            // If Rigidbody2D is missing, you may want to handle this case accordingly.
            // For example, you could destroy the projectile immediately.
            Destroy(projectile);
        }
    }
}
