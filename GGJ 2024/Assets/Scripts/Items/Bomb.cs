public class Bomb : Item
{
    public override void Grab()
    {
        if (PlayerThrow.instance.haveItem) return;
        base.Grab();
        PlayerThrow.instance.SpawnBomb();
        PlayerThrow.instance.haveItem = true;
    }
}
