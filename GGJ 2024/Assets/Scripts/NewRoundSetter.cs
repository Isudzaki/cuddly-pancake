using System.Collections.Generic;
using UnityEngine;

public sealed class NewRoundSetter : MonoBehaviour
{
    #region Serialized Vars
    [Header("Desired Color Setter")]
    [SerializeField] private DesiredColorSetter desColorSetter;
    #endregion

    //Invoking SetTiles void
    #region Start
    private void Start() => InvokeRepeating(nameof(SetTiles), 0,10f);
    #endregion

    //Change the tiles color randomly
    #region Set Tiles
    private void SetTiles()
    {
        foreach (Tile tile in TilesList.Instance.Tiles)
        {
            Transform tilePos = tile.transform;

            tile.transform.position = new Vector3(tilePos.position.x, 0, tilePos.position.z);
            tile.TileColor = ColorDatabase.Colors[Random.Range(0, ColorDatabase.Colors.Length)];
        }
        //Checking all tiles for desired color ones
        Invoke(nameof(CheckTiles), 0.01f);
    }
    #endregion

    #region Check Tiles
    //Check's all tiles for desired color ones and assign one randomly if it's need
    private void CheckTiles()
    {
        List<Tile> tilesWithDesiredColor = TilesList.Instance.Tiles.FindAll(tile => tile.TileColor.color == desColorSetter.desiredColor);

        if (tilesWithDesiredColor.Count == 0)
        {
            // If no tiles with the desired color, select a random tile and change its color
            if (TilesList.Instance.Tiles.Count > 0)
            {
                Tile randomTile = TilesList.Instance.Tiles[Random.Range(0, TilesList.Instance.Tiles.Count)];
                foreach(TileColor tileColor in ColorDatabase.Colors)
                {
                    if (tileColor.num == desColorSetter.desiredNum)
                        randomTile.TileColor = tileColor;
                }
            }
        }
    }
    #endregion
}
