public sealed class Shield : Item
{
    #region Grab
    public override void Grab()
    {
        if (PlayerShield.Instance.haveShield) return;
        base.Grab();
        PlayerShield.Instance.SpawnShield();
        Destroy(gameObject);
    }
    #endregion
}
