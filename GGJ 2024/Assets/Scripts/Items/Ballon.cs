public sealed class Ballon : Item
{
    #region Grab
    public override void Grab()
    {
        if (PlayerController.Instance.haveItem) return;
        base.Grab();
        PlayerController.Instance.haveItem = true;
        PlayerBallon.Instance.SpawnBall();
        Destroy(gameObject);
    }
    #endregion
}
