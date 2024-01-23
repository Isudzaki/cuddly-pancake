using UnityEngine;

[RequireComponent(typeof(MeshRenderer),typeof(Rigidbody))]
public sealed class Tile : MonoBehaviour
{
    #region Private Vars
    private MeshRenderer _mesh;
    private TileColor _tile;
    #endregion

    //Recieving the TileColor to this tile
    #region Tile Color
    public TileColor TileColor
    {
        get => _tile;

        set
        {
            if (_tile == value) return;

            _tile = value;

            _mesh.material.color = _tile.color;
        }
    }
    #endregion

    //Recieving the MeshRender to this mesh
    #region Awake
    private void Awake()=>_mesh = GetComponent<MeshRenderer>();
    #endregion
}
