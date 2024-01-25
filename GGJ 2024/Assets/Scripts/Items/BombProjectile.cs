using UnityEngine;

public class BombProjectile : MonoBehaviour
{
    #region Serialized Vars
    [Header("Explode Vars")]
    [SerializeField] private int radius;
    [SerializeField] private int force;
    #endregion

    #region Private Vars
    private AudioSource audioSource;
    #endregion

    #region Collision Enter
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Explode();
        }
    }
    #endregion

    #region Explode
    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<PlayerController>() != null && hitCollider.GetComponent<PlayerShield>().haveShield==false)
            {
                hitCollider.GetComponent<Rigidbody>().AddExplosionForce(force / Vector3.Distance(hitCollider.transform.position, transform.position), transform.position, radius);
            }
        }
        audioSource = GameObject.Find("ExplodeAudio").GetComponent<AudioSource>();
        audioSource.Play();
        Destroy(gameObject);
    }
    #endregion
}