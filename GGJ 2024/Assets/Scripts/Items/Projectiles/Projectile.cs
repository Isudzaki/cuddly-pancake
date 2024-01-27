using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    #region Private Vars
    protected AudioSource audioSource;
    #endregion

    #region Serialized Vars
    [Header("Activate Clip")]
    [SerializeField] private AudioClip activateClip;
    #endregion

    #region Collision Enter
    protected abstract void OnCollisionEnter(Collision collision);
    #endregion

    #region Grouded
    protected virtual void Activate()
    {
        audioSource = GameObject.Find("ActivateAudio").GetComponent<AudioSource>();
        audioSource.PlayOneShot(activateClip);
        PlayerLaughIndicator.Instance.Score += 100;
        Destroy(gameObject);
    }
    #endregion
}
