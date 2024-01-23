using UnityEngine;

public sealed class DeleteTiles : MonoBehaviour
{
    #region Serialized Vars
    [Header("Tile Prefab")]
    [SerializeField] private GameObject tilePrefab;
    #endregion

    #region Trigger Enter
    //Set's the trigger zone opportunity to destroy old tiles and replacing them by new ones
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Tile tile))
        {
            Transform tilePos = tile.transform;

            GameObject newTile = Instantiate(tilePrefab, new Vector3(tilePos.position.x, -1000, tilePos.position.z),Quaternion.identity);

            TilesList.Instance.Tiles.Add(newTile.GetComponent<Tile>());
            TilesList.Instance.Tiles.Remove(tile);

            Destroy(tile.gameObject);
        }
    }
    #endregion
}
