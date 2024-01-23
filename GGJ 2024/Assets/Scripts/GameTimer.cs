using UnityEngine;
using UnityEngine.UI;

public sealed class GameTimer : MonoBehaviour
{
    #region Serialized Vars
    [Header("Game Time")]
    [SerializeField] private int gameTime;
    [Header("Time Counter Text")]
    [SerializeField] private Text timeText;
    #endregion

    #region Private Vars
    private int timeLeft;
    #endregion

    #region Start
    //Start's the timer
    private void Start()
    {
        timeLeft = gameTime;
        InvokeRepeating(nameof(MinusTime),0, 1);
    }
    #endregion

    #region Timer
    //Minuses the time and convert it to minutes/seconds for text
    private void MinusTime()
    {
        timeLeft--;

        int minutes = timeLeft / 60;
        int seconds = timeLeft - (minutes * 60);
        timeText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }
    #endregion
}
