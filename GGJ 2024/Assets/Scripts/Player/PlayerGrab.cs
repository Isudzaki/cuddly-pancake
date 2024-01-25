using UnityEngine;

[RequireComponent(typeof(PlayerThrow))]
public sealed class PlayerGrab : MonoBehaviour
{
    #region Trigger Enter
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item) && !PlayerThrow.instance.haveItem)
        {
            PlayerThrow.instance.haveItem = true;
            PlayerThrow.instance.SpawnBomb();

            Destroy(item.gameObject);
        }
    }
    #endregion
}
