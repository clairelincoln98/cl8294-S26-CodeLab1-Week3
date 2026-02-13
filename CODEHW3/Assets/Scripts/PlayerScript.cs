using UnityEngine;

/// <summary>
/// Simple player controller container that uses a singleton pattern so the
/// instance persists across scene loads. Attach to the player GameObject.
/// </summary>
public class PlayerScript : MonoBehaviour
{
    // Singleton instance for global access. Only the first created instance is kept.
    public static PlayerScript instance;

    // Start is called once before the first Update after this MonoBehaviour is created.
    void Start()
    {
        //GameManager.instance.Greeting++;
        // If no instance exists, set this as the singleton and prevent it from being destroyed on scene load.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If another instance already exists, destroy this duplicate to enforce a single instance.
            Destroy(gameObject);
        }
    }

 
}