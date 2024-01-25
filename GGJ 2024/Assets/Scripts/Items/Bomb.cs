<<<<<<< HEAD
using UnityEngine;

public sealed class Bomb : Item
{
    #region Start
    protected override void Start()
    {
    }
    #endregion
=======
public class Bomb : Item
{
    public override void Grab()
    {
        PlayerThrow playerThrow = FindFirstObjectByType<PlayerThrow>();
        if (playerThrow.haveItem) return;
        base.Grab();
        playerThrow.SpawnBomb();
        playerThrow.haveItem = true;
    }
>>>>>>> e9b1690a47e3333af7f7aa527fea7320d3cd291b
}
