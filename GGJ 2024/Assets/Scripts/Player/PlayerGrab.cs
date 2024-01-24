using UnityEngine;

[RequireComponent(typeof(PlayerThrow))]
public sealed class PlayerGrab : MonoBehaviour
{
    #region Private Vars
    private PlayerThrow playerThrow;
    #endregion

    #region Start
    private void Start()
    {
        playerThrow = GetComponent<PlayerThrow>();
    }
    #endregion

    #region Trigger Enter
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item) && !playerThrow.haveItem)
        {
            playerThrow.haveItem=true;
            playerThrow.SpawnBomb();
            Destroy(item.gameObject);
        }
    }
    #endregion
}
