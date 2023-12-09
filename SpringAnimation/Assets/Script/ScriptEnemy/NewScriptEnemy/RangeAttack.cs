using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    public GameObject player;

    void Update()
    {
        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            // Instantiate the projectile at the enemy's position
            GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

            // Calculate the direction from the enemy to the player
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

            // Initial shooting direction (upwards)
            Vector3 initialDirection = Vector3.up;
            Vector3 goal = Vector3.Lerp(transform.position, player.transform.position, 0.5f);
            goal.y = 4 *
                     CalculateNormalizedDistance(Vector3.Distance(transform.position, player.transform.position), 15);

            Vector3 p = player.transform.position;
            // Set up initial movement upwards followed by movement towards the player
            newProjectile.transform.DOMove(goal, 0.3f)
                .SetEase(Ease.InSine)
                .OnComplete(() =>
                {
                    // Move towards the player after initial upward movement
                    newProjectile.transform.DOMove(p, 0.3f)
                        .SetEase(Ease.InOutSine)
                        .OnComplete(() =>
                        {
                            // Destroy the projectile when it reaches the player
                            Destroy(newProjectile);
                        });
                });
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
    float CalculateNormalizedDistance(float currentDistance, float maxDistance)
    {
        // Ensure the distance is within the acceptable range to avoid division by zero
        if (maxDistance <= 0)
        {
            Debug.LogError("Max distance should be greater than zero.");
            return 0.0f;
        }

        // Clamp the current distance to avoid negative values
        currentDistance = Mathf.Clamp(currentDistance, 0.0f, maxDistance);

        // Calculate the normalized value between 0 and 1
        float normalizedDistance = currentDistance / maxDistance;

        return normalizedDistance;
    }
}
