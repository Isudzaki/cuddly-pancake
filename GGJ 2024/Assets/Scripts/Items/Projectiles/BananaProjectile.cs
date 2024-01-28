using UnityEngine;
using DG.Tweening;

public class BananaProjectile : Projectile
{
    #region Private Vars
    private bool _canActivate;
    #endregion

    #region Collision Enter
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _canActivate = true;
            GetComponent<Rigidbody>().freezeRotation = true;
            GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        }

        else if (collision.gameObject.TryGetComponent(out PlayerController playerController) && _canActivate)
        {
            Activate();
        }

        else if (collision.gameObject.TryGetComponent(out EnemyAI enemyAI) && _canActivate)
        {
            ActivateAI();
        }
    }
    #endregion

    #region Activate
    protected override void Activate()
    {
        PlayerLaughIndicator.Instance.Score -= 100;
        PlayerController.Instance.isFreezed = true;
        PlayerController.Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        Camera.main.DOShakePosition(0.5f, 0.5f, 10, 0, true);
        DOTween.Sequence()
        .Append(transform.DOScale(new Vector3(0, 0, 0), 1f))
        .AppendCallback(InactiveBanana);
        base.Activate();
    }
    #endregion

    #region Activate
    protected override void ActivateAI()
    {
        Debug.Log("Banana!");
        EnemyLaugh.Instance.Score -= 100;
        EnemyAI.Instance.speed = 0;
        EnemyAI.Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        DOTween.Sequence()
        .Append(transform.DOScale(new Vector3(0, 0, 0), 1f))
        .AppendCallback(InactiveBananaEnemy);
        base.Activate();
    }
    #endregion

    #region Inavtive Banana
    protected void InactiveBanana()
    {
        PlayerController.Instance.isFreezed = false;
        Destroy(gameObject);
    }
    #endregion

    #region Inavtive Banana
    protected void InactiveBananaEnemy()
    {
        EnemyAI.Instance.speed = 1;
        Destroy(gameObject);
    }
    #endregion
}