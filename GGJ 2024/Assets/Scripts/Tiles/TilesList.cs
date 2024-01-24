using System.Collections.Generic;
using UnityEngine;

public sealed class TilesList : MonoBehaviour
{
    #region Vars
    public static TilesList Instance { get; private set; }

    private List<Tile> _tiles = new List<Tile>();
    #endregion

    #region Tiles
    //The tiles list that can be changed by any class
    public List<Tile> Tiles
    {
        get => _tiles;

        set
        {
            _tiles.AddRange(value);
        }
    }
    #endregion

    #region Awake
    //Set's the instance of the TilesList script and add's all tiles on scene
    private void Awake()
    {
        Instance = this;
        Instance.Tiles.AddRange(FindObjectsOfType<Tile>());
    }
    #endregion
}
