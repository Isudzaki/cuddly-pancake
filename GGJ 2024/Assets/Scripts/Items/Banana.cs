public sealed class Banana : Item
{
    #region Grab
    public override void Grab()
    {
        if (PlayerController.Instance.haveItem) return;
        base.Grab();
        PlayerThrow.Instance.SpawnBanana();
        PlayerController.Instance.haveItem = true;
        Destroy(gameObject);
    }
    #endregion

    #region Grab AI
    public override void GrabAI()
    {
        Destroy(gameObject);
    }
    #endregion
}
