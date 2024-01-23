using UnityEngine;

public sealed class PlayerDeathZone: MonoBehaviour
{
    #region Public Vars
    [HideInInspector]
    public bool isPlayerDied = false;
    #endregion

    #region Serialized Vars
    [Header("Loose Screen")]
    [SerializeField] private GameObject looseScreen;
    #endregion

    #region Trigger Enter
    //Set's the trigger zone opportunity to kill player
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController playerController))
        {
            playerController.enabled = false;
            isPlayerDied = true;
            looseScreen.SetActive(true);
        }
    }
    #endregion
}
