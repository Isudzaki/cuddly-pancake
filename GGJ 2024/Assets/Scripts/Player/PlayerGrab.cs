using UnityEngine;

[RequireComponent(typeof(PlayerThrow))]
public sealed class PlayerGrab : MonoBehaviour
{
    #region Trigger Enter
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            item.Grab();
        }
    }
    #endregion
}
