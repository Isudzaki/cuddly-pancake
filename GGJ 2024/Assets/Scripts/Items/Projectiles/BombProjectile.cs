using UnityEngine;

public sealed class BombProjectile : Projectile
{
    #region Serialized Vars
    [Header("Explode Vars")]
    [SerializeField] private int radius;
    [SerializeField] private int force;
    #endregion

    #region Collision Enter
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Activate();
        }
    }
    #endregion

    #region Explode
    protected override void Activate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<PlayerController>() != null && hitCollider.GetComponent<PlayerShield>().haveShield == false)
            {
                hitCollider.GetComponent<Rigidbody>().AddExplosionForce(force / Vector3.Distance(hitCollider.transform.position, transform.position), transform.position, radius);
            }
        }
        base.Activate();
    }
    #endregion
}