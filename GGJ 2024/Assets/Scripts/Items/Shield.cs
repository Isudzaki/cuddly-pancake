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

    #region Grab AI
    public override void GrabAI()
    {
        if (EnemyShield.Instance.haveShield) return;
        EnemyShield.Instance.SpawnShield();
        Destroy(gameObject);
    }
    #endregion
}
