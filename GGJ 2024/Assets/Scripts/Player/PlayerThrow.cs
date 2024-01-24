using UnityEngine;

public sealed class PlayerThrow : MonoBehaviour
{
    #region Serialized Vars
    [Header("Trajectory Line")]
    [SerializeField] private LineRenderer trajectoryLine;
    [Header("Launch Angle")]
    [Range(1, 90)]
    [SerializeField] private float launchAngle = 45f;
    [Header("Gravity Angle")]
    [Range(1, 9.8f)]
    [SerializeField] private float gravity = 9.8f;
    [Header("Projectile Prefab")]
    [SerializeField] private GameObject bombPrefab;
    [Header("Projectile Vars")]
    [SerializeField] private int throwForce;
    [SerializeField] private int launchForce;
    [SerializeField] private int projectileSpeed;
    [Header("Projectile Throw Point")]
    [SerializeField] private Transform throwPoint;
    #endregion

    #region Public Vars
    [HideInInspector]
    public bool haveItem;
    [HideInInspector]
    public GameObject bomb;
    #endregion

    #region Private Vars
    private Vector3 mousePosition;
    private float distanceToPoint;
    #endregion

    #region Start
    private void Start()
    {
        trajectoryLine.positionCount = 2;
        trajectoryLine.gameObject.SetActive(false);
    }
    #endregion

    #region Update
    private void Update()
    {
        if (!haveItem) return;

        if (Input.GetMouseButtonDown(0))
        {
            trajectoryLine.gameObject.SetActive(true);
        }

        if (Input.GetMouseButton(0))
        {
            DrawTrajectory();
        }

        if (Input.GetMouseButtonUp(0))
        {
            haveItem = false;
            trajectoryLine.gameObject.SetActive(false);
            ThrowCore();
        }
    }
    #endregion

    #region DrawTraectory
    private void DrawTrajectory()
    {
        //--Creates a Ray that looks from the camera to the cursor--
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Create an invisible plane upon contact with which our mousePosition will get
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            // Set a point for player Look
            mousePosition = cameraRay.GetPoint(rayLength);
        }

        Vector3 bombPosition = throwPoint.position;

        // Ensure y is the same as the bomb to avoid changes in height.
        mousePosition.y = bombPosition.y;

        Vector3 launchDirection = mousePosition - bombPosition;
        launchDirection.y = 0f; // Project the launch direction onto the x-z plane.

        float launchAngleRad = launchAngle * Mathf.Deg2Rad;
        float projectileTime = Mathf.Sqrt(2 * launchDirection.magnitude * Mathf.Tan(launchAngleRad) / gravity);

        trajectoryLine.positionCount = 50;
        for (int i = 0; i < 50; i++)
        {
            float time = i / 50f * projectileTime;
            Vector3 position = bombPosition + new Vector3(
                launchDirection.x * Mathf.Cos(launchAngleRad) * time,
                launchDirection.magnitude * Mathf.Sin(launchAngleRad) * time - 0.5f * gravity * time * time,
                launchDirection.z * Mathf.Cos(launchAngleRad) * time
            );
            trajectoryLine.SetPosition(i, position);
        }
    }
    #endregion

    #region Throw Core
    public void ThrowCore()
    {
            if (mousePosition == null) return;
            bomb.transform.parent = null;
            Rigidbody bombRb = bomb.GetComponent<Rigidbody>();
            bombRb.useGravity = true;
            bombRb.isKinematic = false;
            // Apply impulse upwards (towards the positive y-axis) to the core
            bombRb.AddForce(Vector3.up * launchForce * (distanceToPoint * -1 / 45), ForceMode.Impulse);

            // Apply additional force in the direction of the player
            bombRb.AddForce(transform.forward * throwForce * (distanceToPoint * -1 / 45), ForceMode.Impulse);

            // Calculate the direction from the bomb to the target
            Vector3 direction = (mousePosition - transform.position).normalized;

            // Apply a force in that direction to propel the bomb
            if (distanceToPoint * -1 < 40 && distanceToPoint * -1 >= 15)
                bombRb.AddForce(direction * (projectileSpeed + 4 * (distanceToPoint * -1 / 43)), ForceMode.Impulse);
            else if (distanceToPoint * -1 < 15 && distanceToPoint * -1 >= 10)
                bombRb.AddForce(direction * (projectileSpeed + 3 * (distanceToPoint * -1 / 43)), ForceMode.Impulse);
            else if (distanceToPoint * -1 < 10 && distanceToPoint * -1 >= 5)
            bombRb.AddForce(direction * (projectileSpeed + 2 * (distanceToPoint * -1 / 43)), ForceMode.Impulse);
            else if (distanceToPoint * -1 < 5)
            bombRb.AddForce(direction * (projectileSpeed + 1 * (distanceToPoint * -1 / 43)), ForceMode.Impulse);
            else
                bombRb.AddForce(direction * (projectileSpeed + 5 * (distanceToPoint * -1 / 43)), ForceMode.Impulse);

            // Apply rotation to the bomb to match the direction of the player
            bomb.transform.rotation = Quaternion.LookRotation(bombRb.velocity);
            distanceToPoint = Vector3.Distance(transform.position, mousePosition) * -1;
    }
    #endregion

    #region Spawn Bomb
    public void SpawnBomb()
    {
        bomb = Instantiate(bombPrefab, throwPoint.position, Quaternion.identity);
        bomb.transform.SetParent(throwPoint);
    }
    #endregion
}
