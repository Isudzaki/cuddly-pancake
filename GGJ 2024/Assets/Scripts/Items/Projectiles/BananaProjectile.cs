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
        .Append(transform.DOScale(new Vector3(0, 0, 0), 0.5f))
        .AppendCallback(InactiveBanana);
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
}