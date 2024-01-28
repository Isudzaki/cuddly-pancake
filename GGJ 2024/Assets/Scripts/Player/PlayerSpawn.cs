using UnityEngine;

public sealed class PlayerSpawn : MonoBehaviour
{
    #region Serialized Vars
    [Header("Player Transform")]
    [SerializeField] private Transform playerTF;

    [Header("Player Spawn Borders")]
    [SerializeField] private float minX, maxX, minZ, maxZ;
    #endregion

    #region Start
    private void Start()
    {
        SpawnPlayer();
    }
    #endregion

    #region Respawn Player
    public void RespawnPlayer()
    {
        playerTF.GetComponent<PlayerController>().enabled = true;
        playerTF.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        SpawnPlayer();
    }
    #endregion

    #region Spawn Player
    private void SpawnPlayer()
    {
        playerTF.position = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
    }
    #endregion

    #region Respawn Enemy
    public void RespawnEnemy(EnemyAI enemy)
    {
        enemy.enabled = true;
        enemy.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        enemy.transform.position = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
    }
    #endregion
}
