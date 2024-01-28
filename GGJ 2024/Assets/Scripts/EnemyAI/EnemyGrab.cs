using UnityEngine;

public sealed class EnemyGrab : MonoBehaviour
{
    #region Trigger Enter
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            item.GrabAI();
        }
    }
    #endregion
}
