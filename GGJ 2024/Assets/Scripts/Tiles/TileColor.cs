using UnityEngine;

[CreateAssetMenu(menuName = "Tiles/Color")]
public sealed class TileColor : ScriptableObject
{
    //Tiles color
    [Header("Color")]
    public Color color;

    //Tiles num
    [Header("Number")]
    public int num;
}
