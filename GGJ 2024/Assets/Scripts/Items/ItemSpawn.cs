using UnityEngine;

public sealed class ItemSpawn : MonoBehaviour
{
    #region Serialized Vars
    [Header("Items")]
    [SerializeField] GameObject[] items;
    [Header("Player Spawn Borders")]
    [SerializeField] private float minX, maxX, minZ, maxZ;
    #endregion

    #region Spawn Item
    public void SpawnItem()
    {
        int count = Random.Range(1, 4);
        for(int c = 0; c < count; c++)
        {
            Instantiate(items[Random.Range(0, items.Length)], new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ)),Quaternion.identity);
        }
    }
    #endregion
}
