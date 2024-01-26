using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public sealed class GameTimer : MonoBehaviour
{
    #region Serialized Vars
    [Header("Game Time")]
    [SerializeField] private int gameTime;
    [Header("Time Counter Text")]
    [SerializeField] private Text timeText;
    [Header("End Screen")]
    [SerializeField] private GameObject endScreen;
    [Header("Audio")]
    [SerializeField] private AudioSource gameEndSource;
    #endregion

    #region Private Vars
    private int timeLeft;
    #endregion

    #region Start Timer
    //Start's the timer
    public void StartTimer()
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
        if (timeLeft == 0)
        {
            CancelInvoke(nameof(MinusTime));
            PlayerController.Instance.enabled = false;
            endScreen.SetActive(true);
            endScreen.transform.DOScale(new Vector3(1, 1, 1),0.5f);
            gameEndSource.Play();
            Invoke(nameof(PauseGame), 0.5f);
        }
    }
    #endregion

    #region Pause Game
    private void PauseGame()
    {
        Time.timeScale = 0;
    }
    #endregion
}
