using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement Data")]
    public float moveSpeed;
    public float rotationSpeed;

    public float verticalInput;
    public float horizontalInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if (verticalInput < 0)
        {
            horizontalInput = -Input.GetAxis("Horizontal");
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = moveSpeed * verticalInput * transform.forward;
        rb.linearVelocity = movement;

        transform.Rotate(0, horizontalInput * rotationSpeed, 0);
    }
}
