using UnityEngine;

public sealed class EnemyRespawn : MonoBehaviour
{
    #region Public Vars
    [HideInInspector]
    public bool canRespawn = false;
    public EnemyAI enemy;
    #endregion

    #region Serialized Vars
    [Header("Game Time")]
    [SerializeField] private int gameTime;
    #endregion

    #region Private Vars
    private int timeLeft;
    #endregion

    #region Invoke Timer
    //Start's the timer
    public void InvokeTimer()
    {
        timeLeft = gameTime;
        InvokeRepeating(nameof(MinusTime), 0, 1);
    }
    #endregion

    #region Timer
    //Minuses the time and updates time text, if the time ends it will stop the timer
    private void MinusTime()
    {
        timeLeft--;

        if (timeLeft == 0)
        {
            canRespawn = true;
            CancelInvoke(nameof(MinusTime));
        }

    }
    #endregion
}
