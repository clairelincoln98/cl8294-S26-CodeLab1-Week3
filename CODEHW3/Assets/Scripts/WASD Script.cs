using UnityEngine;

public class WASDScript : MonoBehaviour
{
    public float forceAmount = 10f;
    public float maxSpeed = 10f;
    
    public KeyCode upwardKey = KeyCode.W;
    public KeyCode downwardKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    
    private Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(upwardKey))
        {
            rb.AddForce(Vector3.up * forceAmount);
        }
        
        if (Input.GetKey(downwardKey))
        {
            rb.AddForce(Vector3.down * forceAmount);
        }
        
        if (Input.GetKey(leftKey))
        {
            rb.AddForce(Vector3.left * forceAmount);
        }
        
        if (Input.GetKey(rightKey))
        {
            rb.AddForce(Vector3.right * forceAmount);
        }

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
}
