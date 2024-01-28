using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject enemyRespawnPrefab;

    private Transform target;

    public EnemyRespawn enemyRespawn;

    public int speed;

    public static EnemyAI Instance;

    public Animator animator;

    private void Awake()
    {
        Instance = this;
        Invoke(nameof(CheckTiles), 5f);
    }

    private void Start()
    {
        enemyRespawn = Instantiate(enemyRespawnPrefab,transform.position,Quaternion.identity).GetComponent<EnemyRespawn>();
        enemyRespawn.enemy = this;
    }

    #region Check Tiles
    //Change the tiles color randomly
    public void CheckTiles()
    {
        if (TilesList.Instance.Tiles != null && DesiredColorSetter.instance.desiredColor != null)
        {
            foreach (Tile tile in TilesList.Instance.Tiles)
            {
                if (tile.TileColor.color == DesiredColorSetter.instance.desiredColor)
                {
                    target = tile.transform;
                }
            }
        }

        Invoke(nameof(CheckTiles), 10f);
    }
    #endregion

    public void Update()
    {
        if (target != null && Vector3.Distance(transform.position,target.position)>0.05)
        {
            animator.SetFloat("Speed", 1f);
            // Move the enemy towards the target
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x,transform.position.y,target.position.z), speed * Time.deltaTime);

            Vector3 direction = target.position - transform.position;
            direction.y = 0f; // Ignore the Y axis

            // Create a rotation based on the direction
            Quaternion rotation = Quaternion.LookRotation(direction);

            // Rotate to face the target (optional)
            transform.rotation = rotation;

            // Check if the enemy has reached the target
            if (Vector3.Distance(transform.position, target.position) < 0.2f)
            {
                animator.SetFloat("Speed", 0.01f);
                // Do something when the enemy reaches the target
                Debug.Log("Enemy reached the target!");
                target = null; // Set target to null to stop moving
            }
        }
    }

}
