using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Serialize Vars
    [Header("Player Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [Header("Audio")]
    [SerializeField] private AudioSource footStepSource,jumpSource;
    #endregion

    #region System vars
    //Links
    private Rigidbody rb;
    private Camera mainCamera;
    private Animator animator;

    //System
    [HideInInspector] public static PlayerController Instance;
    private Vector3 moveVelocity;

    //Check
    private bool isGrounded;
    #endregion

    #region Awake()
    private void Awake()
    {
        //Make an example script for accessing it in other classes
        Instance = this;
    }
    #endregion

    #region Start()
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }
    #endregion

    #region Update()
    private void Update()
    {
        Look();
        if (StartTimer.Instance.isTimeOver != true) return;
        //--Get player Input and assign them to the magnitude of the character?s movement multiplied by speed--
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        animator.SetFloat("Speed", moveInput.magnitude);
        if (moveInput.magnitude > 0.1f &&(!footStepSource.isPlaying)) footStepSource.Play();
        if (moveInput.magnitude < 0.1f) footStepSource.Stop();
        //

        Jump();
    }
    #endregion

    #region FixedUpdate()
    private void FixedUpdate()
    {
        Move();
    }
    #endregion

    #region Move()
    private void Move()
    {
        //--Moving the player using "MovePosition"--
        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
        //
    }
    #endregion


    #region Jump()
    private void Jump()
    {
        //--Checks if the "space" key is pressed and if the player is on the ground--
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //"AddForce" to make player jump and set "isGrounded = false;" to remove double jump
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("Jump", true);
            isGrounded = false;
            jumpSource.Play();
        }
    }
    #endregion

    #region Look()
    private void Look()
    {
        if (PauseCheck.Instance.opened) return;
        //--Creates a Ray that looks from the camera to the cursor--
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        //?reate an invisible plane upon contact with which our player will look at the point of impact
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLenth;

        if (groundPlane.Raycast(cameraRay, out rayLenth))
        {
            //Set a point for player Look
            Vector3 pointToLook = cameraRay.GetPoint(rayLenth);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
    #endregion

    #region Collision()
    //--Check if player on ground --> "isGrounded = true"--
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("Jump", false);
            isGrounded = true;
        }
    }
    #endregion
}
