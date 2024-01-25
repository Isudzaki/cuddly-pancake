using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    #region Public Vars
    [HideInInspector] public static PlayerShield Instance;
    [HideInInspector] public bool haveShield;
    #endregion

    #region Serialized Vars
    [Header("Shield Time")]
    [SerializeField] private int time;
    [Header("Shield Time")]
    [SerializeField] private GameObject shieldPrefab;
    #endregion

    #region Private Vars
    private GameObject _shield;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    //Spawn's shield and deletes it after time seconds
    #region Shield
    public void SpawnShield()
    {
        _shield = Instantiate(shieldPrefab, PlayerController.Instance.transform.position, Quaternion.identity);
        _shield.transform.SetParent(PlayerController.Instance.transform);
        haveShield = true;
        Invoke(nameof(DestroyShield), time);
    }

    private void DestroyShield()
    {
        Destroy(_shield);
        haveShield = false;
    }
    #endregion
}
