using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the Projectile Prefab

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Get the player's position and rotation
            Vector3 spawnPosition = transform.position;
            Quaternion spawnRotation = transform.rotation;

            // Instantiate a projectile at the specified position and rotation
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, spawnRotation);

            // Additional settings for the projectile (if needed)
            // e.g., setting projectile velocity or properties

            // Destroy the projectile after a certain time (optional)
            Destroy(projectile, 2.0f);
        }
    }
}
