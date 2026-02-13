using System;
using UnityEngine;

public class PrizeScript : MonoBehaviour
{
    // Range (in world units) used when picking a new random position.
    // The new X and Y will be chosen from [-relocationRange, relocationRange].
    int relocationRange = 5;

    // Called by Unity when this GameObject's collider collides with another collider.
    // Requires at least one of the colliding objects to have a Rigidbody.
    void OnCollisionEnter(Collision other)
    {
        // Simple debug log to verify collision occurred.
        Debug.Log("Got it!");

        // Compute a new random position within the defined range.
        // Z is left at 0 here — adjust if your scene uses a different depth.
        Vector3 newPosition = new Vector3(
            UnityEngine.Random.Range(-relocationRange, relocationRange),
            UnityEngine.Random.Range(-relocationRange, relocationRange),
            0
        );

        // Teleport the prize to the new location.
        transform.position = newPosition;

        // Increment the shared score on the GameManager singleton.
        // Ensure `GameManager.instance` is set (GameManager exists) to avoid a null reference.
       // GameManager.instance.Greeting++;
    }
}