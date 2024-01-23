using UnityEngine;

public sealed class TilesColorChecker : MonoBehaviour
{
    #region Serialized Vars
    [Header("Desired Color Setter")]
    [SerializeField] private DesiredColorSetter desColorSetter;
    #endregion

    //Invoking SetTiles void
    #region Start
    private void Start()=>Invoke(nameof(CheckTiles), 5f);
    #endregion

    //Change the tiles color randomly
    #region Check Tiles
    public void CheckTiles()
    {
        foreach (Tile tile in TilesList.Instance.Tiles)
        {
            if (tile.TileColor.color != desColorSetter.desiredColor)
            {
                Rigidbody tileRb = tile.GetComponent<Rigidbody>();
                tileRb.isKinematic = false;
            }
        }

        Invoke(nameof(CheckTiles), 10f);
    }
    #endregion
}
