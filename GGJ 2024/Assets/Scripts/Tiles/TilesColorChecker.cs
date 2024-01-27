using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public sealed class TilesColorChecker : MonoBehaviour
{
    #region Serialized Vars
    [Header("Desired Color Setter")]
    [SerializeField] private DesiredColorSetter desColorSetter;
    [Header("Audio")]
    [SerializeField] private AudioSource fallSource;
    #endregion

    #region Private Vars
    private List<Tile> fallTiles = new List<Tile>();
    #endregion

    #region Start Timer
    //Invoking SetTiles void
    public void StartCheck()=>Invoke(nameof(CheckTiles), 5f);
    #endregion

    #region Check Tiles
    //Change the tiles color randomly
    public void CheckTiles()
    {
        fallTiles.Clear();
        int i = 0;
        foreach (Tile tile in TilesList.Instance.Tiles)
        {
            if (tile.TileColor.color != desColorSetter.desiredColor)
            {
                fallTiles.Add(tile);
                tile.transform.DOShakePosition(0.5f, 0.05f, 10, 0);
                i++;
            }
        }
        Invoke(nameof(FallTiles), 0.5f);
        Invoke(nameof(CheckTiles), 10f);
    }
    #endregion

    #region Fall Tiles
    //Making tiles Rigidbody non-kinematic
    private void FallTiles()
    {
        for (int i = 0; i < fallTiles.Count; i++)
        {
            Rigidbody tileRb = fallTiles[i].GetComponent<Rigidbody>();
            tileRb.isKinematic = false;
        }
        if (fallTiles.Count > 0) fallSource.Play();
    }
    #endregion
}
