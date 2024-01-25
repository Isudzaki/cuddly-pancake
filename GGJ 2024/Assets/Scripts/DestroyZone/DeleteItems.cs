using UnityEngine;

public sealed class DeleteItems : MonoBehaviour
{
    #region Trigger Enter
    //Set's the trigger zone opportunity to destroy items
    private void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Item"))
            {
                Destroy(other.gameObject);
            }
    }
    #endregion
}
