using UnityEngine;
using UnityEngine.UI;

public sealed class StartTimer : MonoBehaviour
{
    #region Public Vars
    [HideInInspector]
    public bool isTimeOver = false;
    #endregion

    #region Serialized Vars
    [Header("Wait Time")]
    [SerializeField] private int waitTime;
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
        timeLeft = waitTime;
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
            isTimeOver = true;
            CancelInvoke(nameof(MinusTime));
        }

    }
    #endregion
}
