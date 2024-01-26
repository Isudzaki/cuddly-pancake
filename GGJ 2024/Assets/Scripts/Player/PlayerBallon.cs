using UnityEngine;

public class PlayerBallon : MonoBehaviour
{
    #region Public Vars
    [HideInInspector] public static PlayerBallon Instance;
    [HideInInspector] public bool haveBall;
    #endregion

    #region Serialized Vars
    [Header("Shield Time")]
    [SerializeField] private int time;
    [Header("Item Point")]
    [SerializeField] private Transform itemPoint;
    [Header("Shield Time")]
    [SerializeField] private GameObject ballPrefab;
    #endregion

    #region Private Vars
    private GameObject _ball;
    private Rigidbody rb;
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
        rb = GetComponent<Rigidbody>();
    }
    #endregion

    //Spawn's ball and deletes it after time seconds
    #region Ball
    public void SpawnBall()
    {
        _ball = Instantiate(ballPrefab, new Vector3(itemPoint.position.x, itemPoint.position.y+0.3f, itemPoint.position.z), Quaternion.identity);
        _ball.transform.SetParent(PlayerController.Instance.transform);
        rb.mass = 0.7f;
        haveBall = true;
        Invoke(nameof(DestroyBall), time);
    }

    private void DestroyBall()
    {
        PlayerController.Instance.haveItem = false;
        rb.mass = 1;
        haveBall = false;
        _ball.GetComponent<BoxCollider>().enabled = true;
        Rigidbody ballRb = _ball.GetComponent<Rigidbody>();
        ballRb.isKinematic = false;
        ballRb.velocity=new Vector3(0,5,0);
    }
    #endregion
}

