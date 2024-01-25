using UnityEngine;

[RequireComponent(typeof(PlayerThrow))]
public sealed class PlayerGrab : MonoBehaviour
{
    #region Trigger Enter
    private void OnTriggerEnter(Collider other)
    {
<<<<<<< HEAD
        if (other.TryGetComponent(out Item item) && !PlayerThrow.instance.haveItem)
        {
            PlayerThrow.instance.haveItem = true;
            PlayerThrow.instance.SpawnBomb();

=======
        if (other.TryGetComponent(out Item item))
        {
            item.Grab();
>>>>>>> e9b1690a47e3333af7f7aa527fea7320d3cd291b
            Destroy(item.gameObject);
        }
    }
    #endregion
}
