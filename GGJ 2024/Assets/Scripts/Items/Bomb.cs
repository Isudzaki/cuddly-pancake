public class Bomb : Item
{
    #region Grab
    public override void Grab()
    {
        if (PlayerController.Instance.haveItem) return;
        base.Grab();
        PlayerThrow.Instance.SpawnBomb();
        PlayerController.Instance.haveItem = true;
        Destroy(gameObject);
    }
    #endregion
}
