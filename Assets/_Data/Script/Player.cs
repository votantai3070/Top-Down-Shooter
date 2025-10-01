using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Gun Data")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;

    [Header("Movement Data")]
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;

    float verticalInput;
    float horizontalInput;

    [Header("Tower Data")]
    [SerializeField] Transform towerTransform;
    [SerializeField] float towerRotationSpeed;

    [Header("Aim Data")]
    [SerializeField] LayerMask whatIsAimMask;
    [SerializeField] Transform aimTransform;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        UpdateAim();
        CheckInputs();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }


    private void FixedUpdate()
    {
        // Move and rotate player
        ApplyMovement();

        ApplyBodyRotation();

        // Rotate tower
        ApplyTowerRotation();
    }
    private void CheckInputs()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if (verticalInput < 0)
        {
            horizontalInput = -Input.GetAxis("Horizontal");
        }
    }

    private void ApplyTowerRotation()
    {
        Vector3 direction = aimTransform.position - towerTransform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        towerTransform.rotation = Quaternion.RotateTowards(towerTransform.rotation, targetRotation, towerRotationSpeed);
    }

    private void ApplyBodyRotation()
    {
        transform.Rotate(0, horizontalInput * rotationSpeed, 0);
    }

    private void ApplyMovement()
    {
        Vector3 movement = moveSpeed * verticalInput * transform.forward;
        rb.linearVelocity = movement;
    }

    private void UpdateAim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, whatIsAimMask))
        {
            float fixedY = aimTransform.position.y;
            aimTransform.position = new Vector3(hit.point.x, fixedY, hit.point.z);
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        bulletRb.linearVelocity = firePoint.forward * bulletSpeed;

        Destroy(bullet, 5f);
    }
}
