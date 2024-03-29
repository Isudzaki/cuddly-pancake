using UnityEngine;
using UnityEngine.UI;

public sealed class RespawnTimer : MonoBehaviour
{
    #region Public Vars
    [HideInInspector]
    public bool canRespawn = false;
    #endregion

    #region Serialized Vars
    [Header("Game Time")]
    [SerializeField] private int gameTime;
    [Header("Time Counter Text")]
    [SerializeField] private Text timeText;
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
        timeText.text = $"{timeLeft} seconds left";

        if (timeLeft == 0)
        {
            canRespawn = true;
            timeText.text = "Wait the next round";
            CancelInvoke(nameof(MinusTime));
        }

    }
    #endregion
}
