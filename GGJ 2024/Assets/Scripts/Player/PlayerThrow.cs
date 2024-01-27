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
    [SerializeField] private float force;
    [Header("Projectile Prefab")]
    [SerializeField] private GameObject bombPrefab,bananaPrefab;
    [Header("Projectile Throw Point")]
    [SerializeField] private Transform throwPoint;
    [Header("Audio")]
    [SerializeField] private AudioSource throwSource;
    #endregion

    #region Public Vars
    [HideInInspector]
    public GameObject obj;
    [HideInInspector]
    public static PlayerThrow Instance;
    #endregion

    #region Private Vars
    private Vector3 mousePosition;
    private int maxThrowDistance = 30;
    #endregion

    #region Awake
    private void Awake()
    {
        Instance = this;
    }
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
        if (!PlayerController.Instance.haveItem || PlayerBallon.Instance.haveBall || PlayerDance.Instance.isDancing || PauseCheck.Instance.opened || PlayerController.Instance.isFreezed) return;

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
            throwSource.Play();
            PlayerController.Instance.haveItem = false;
            trajectoryLine.gameObject.SetActive(false);
            ThrowProjectile();
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

    #region Throw Projectile
    public void ThrowProjectile()
    {
        if (obj == null)
        {
            Debug.LogError("Object is null.");
            return;
        }

        obj.transform.parent = null;
        obj.GetComponent<Collider>().enabled = true;
        Rigidbody objRb = obj.GetComponent<Rigidbody>();
        objRb.useGravity = true;
        objRb.isKinematic = false;

        // Calculate the launch direction
        Vector3 launchDirection = mousePosition - throwPoint.position;
        launchDirection.y = 0f; // Project the launch direction onto the x-z plane.

        // Calculate the distance between throwPoint and mousePosition
        float distance = Vector3.Distance(throwPoint.position, mousePosition);

        // Adjust the throwing strength based on the distance
        float adjustedThrowForce = Mathf.Clamp01(distance / maxThrowDistance);

        // Apply force to the bomb using Rigidbody
        if (objRb != null)
        {
            // Calculate the force based on adjustedThrowForce
            float totalForce = adjustedThrowForce*force; // Adjust this multiplier based on your needs

            // Apply force to each point along the trajectory
            for (int i = 0; i < trajectoryLine.positionCount; i++)
            {
                Vector3 forceDirection = trajectoryLine.GetPosition(i) - obj.transform.position;
                objRb.AddForce(forceDirection.normalized * totalForce, ForceMode.Impulse);
            }
        }
        else
        {
            Debug.LogError("Rigidbody component not found on objPrefab.");
        }
    }
    #endregion


    #region Spawn Bomb
    public void SpawnBomb()
    {
        obj = Instantiate(bombPrefab, throwPoint.position, Quaternion.identity);
        obj.transform.SetParent(throwPoint);
    }
    #endregion

    #region Spawn Banana
    public void SpawnBanana()
    {
        obj = Instantiate(bananaPrefab, throwPoint.position, Quaternion.identity);
        obj.transform.SetParent(throwPoint);
    }
    #endregion
}
