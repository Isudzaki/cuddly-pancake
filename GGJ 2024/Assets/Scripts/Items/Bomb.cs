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
}
