using UnityEngine;

[RequireComponent(typeof(MeshRenderer),typeof(Rigidbody))]
public sealed class Tile : MonoBehaviour
{
    #region Private Vars
    private MeshRenderer _mesh;
    private TileColor _tile;
    #endregion

    #region Public Vars
    [HideInInspector]
    public float x,z;
    #endregion

    #region Tile Color
    //Recieving the TileColor to this tile
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

    #region Awake
    //Recieving the MeshRender to this mesh and 
    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
        x = transform.position.x;
        z = transform.position.z;
    }
    #endregion
}
