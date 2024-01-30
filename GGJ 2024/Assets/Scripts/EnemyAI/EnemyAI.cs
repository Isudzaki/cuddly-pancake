using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject enemyRespawnPrefab;

    private Transform target;
    private bool isTargetReached;

    public EnemyRespawn enemyRespawn;

    public int speed;

    public static EnemyAI Instance;

    public Animator animator;

    private void Awake()
    {
        Instance = this;
        Invoke(nameof(CheckTiles), 5f);
    }

    #region Start
    private void Start()
    {
        enemyRespawn = Instantiate(enemyRespawnPrefab,transform.position,Quaternion.identity).GetComponent<EnemyRespawn>();
        enemyRespawn.enemy = this;
    }
    #endregion

    #region Check Tiles
    public void CheckTiles()
    {
        Tile[] targetTiles = new Tile[0];
        int i = 0;
        if (TilesList.Instance.Tiles != null && DesiredColorSetter.instance.desiredColor != null)
        {
            foreach (Tile tile in TilesList.Instance.Tiles)
            {
                if (tile.TileColor.color == DesiredColorSetter.instance.desiredColor)
                {
                    Array.Resize(ref targetTiles, targetTiles.Length + 1);
                    targetTiles[i] = tile;
                    i++;
                }
            }

            foreach (Tile needTile in targetTiles)
            {
                CalculateAndSetNearestTile(needTile);
            }
        }
    }
    #endregion
#region Calculate Tiles
    private void CalculateAndSetNearestTile(Tile needTile)
    {
        Transform currentTransform = transform; // Assuming this script is attached to a GameObject

        Tile nearestTile = null;
        float minDistance = float.MaxValue;

        foreach (Tile tile in TilesList.Instance.Tiles)
        {
            if (tile == needTile)
            {
                float distance = Vector3.Distance(currentTransform.position, tile.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTile = tile;
                }
            }
        }

        if (nearestTile != null)
        {
            target = nearestTile.transform;
            isTargetReached = false;
        }
    }
    #endregion

    public void Update()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) >= 0.5f)
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
            if (Vector3.Distance(transform.position, target.position) <= 0.55f)
            {
                animator.SetFloat("Speed", 0.01f);
                // Do something when the enemy reaches the target
                Debug.Log("Enemy reached the target!");
                if (isTargetReached) return;
                int danceRandom = UnityEngine.Random.Range(1, 4);
                if (danceRandom == 1)
                    Dance();
                isTargetReached = true;
            }
        }
    }

    private void Dance()
    {
        EnemyLaugh.Instance.Score += 100;
        Viewers.instance.UpdateEndNumber(50);
        animator.SetTrigger("StartDance");
        speed = 0;
        Invoke(nameof(EndDance), 5f);
    }

    private void EndDance()
    {
        animator.SetTrigger("EndDance");
        speed = 1;
    }

}
