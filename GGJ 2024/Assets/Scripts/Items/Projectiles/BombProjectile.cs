using UnityEngine;

public sealed class BombProjectile : Projectile
{
    #region Serialized Vars
    [Header("Explode Vars")]
    [SerializeField] private int radius;
    [SerializeField] private int force;

    [SerializeField] GameObject particles;
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
        ParticleSystem particleSystem =  Instantiate(particles, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        particleSystem.Play();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<PlayerController>() != null && hitCollider.GetComponent<PlayerShield>().haveShield == false)
            {

                hitCollider.GetComponent<Rigidbody>().AddExplosionForce(force / Vector3.Distance(hitCollider.transform.position, transform.position), transform.position, radius);
            }
            else if (hitCollider.GetComponent<EnemyAI>() != null && hitCollider.GetComponent<EnemyShield>().haveShield == false)
            {
                PlayerLaughIndicator.Instance.Score += 100;
                Viewers.instance.UpdateEndNumber(50);

                hitCollider.GetComponent<Rigidbody>().AddExplosionForce(force / Vector3.Distance(hitCollider.transform.position, transform.position), transform.position, radius);
            }
        }
        base.Activate();
    }
    #endregion

    #region Explode
    protected override void ActivateAI()
    {
        ParticleSystem particleSystem = Instantiate(particles, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        particleSystem.Play();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<PlayerController>() != null && hitCollider.GetComponent<PlayerShield>().haveShield == false)
            {
                EnemyLaugh.Instance.Score += 100;
                Viewers.instance.UpdateEndNumber(50);

                hitCollider.GetComponent<Rigidbody>().AddExplosionForce(force / Vector3.Distance(hitCollider.transform.position, transform.position), transform.position, radius);
            }
            else if (hitCollider.GetComponent<EnemyAI>() != null && hitCollider.GetComponent<EnemyShield>().haveShield == false)
            {
                hitCollider.GetComponent<Rigidbody>().AddExplosionForce(force / Vector3.Distance(hitCollider.transform.position, transform.position), transform.position, radius);
            }
        }
        base.Activate();
    }
    #endregion
}