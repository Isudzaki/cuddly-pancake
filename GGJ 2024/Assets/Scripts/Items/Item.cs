using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class Item : MonoBehaviour
{
    #region Serialized Vars
    [Header("Grab Audio Clip")]
    [SerializeField] private AudioClip grabClip;
    #endregion

    #region Private Vars
    private AudioSource audioSource;
    #endregion

    #region Grab
    public virtual void Grab()
    {
        audioSource = GameObject.Find("GrabAudio").GetComponent<AudioSource>();
        audioSource.PlayOneShot(grabClip);
    }
    #endregion
}
