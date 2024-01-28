using UnityEngine;

public sealed class PlayerDance : MonoBehaviour
{
    #region Public Vars
    [HideInInspector]
    public bool isDancing=false;

    public static PlayerDance Instance;

    public Animator animator;
    #endregion

    #region Awake
    private void Awake() => Instance = this;
    #endregion

    #region Update
    //If player press "B" the dance will start
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && !isDancing)
        {
            isDancing=true;
            animator.SetTrigger("StartDance");
            PlayerLaughIndicator.Instance.Score += 100;
            Viewers.instance.UpdateEndNumber(50);
            Invoke(nameof(EndDance),5f);
        }
    }
    #endregion

    #region End Dance
    private void EndDance()
    {
        isDancing = false;
        animator.SetTrigger("EndDance");
    }
    #endregion
}
