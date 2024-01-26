using UnityEngine;

public sealed class TilesColorChecker : MonoBehaviour
{
    #region Serialized Vars
    [Header("Desired Color Setter")]
    [SerializeField] private DesiredColorSetter desColorSetter;
    [Header("Audio")]
    [SerializeField] private AudioSource fallSource;
    #endregion

    #region Start Timer
    //Invoking SetTiles void
    public void StartCheck()=>Invoke(nameof(CheckTiles), 5f);
    #endregion

    #region Check Tiles
    //Change the tiles color randomly
    public void CheckTiles()
    {
        int i = 0;
        foreach (Tile tile in TilesList.Instance.Tiles)
        {
            if (tile.TileColor.color != desColorSetter.desiredColor)
            {
                Rigidbody tileRb = tile.GetComponent<Rigidbody>();
                tileRb.isKinematic = false;
                i++;
            }
        }
        if (i > 0) fallSource.Play();
        Invoke(nameof(CheckTiles), 10f);
    }
    #endregion
}
