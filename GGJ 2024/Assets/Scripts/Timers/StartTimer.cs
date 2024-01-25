using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public sealed class StartTimer : MonoBehaviour
{
    #region Public Vars
    [HideInInspector]
    public bool isTimeOver = false;
    public static StartTimer Instance;
    #endregion

    #region Serialized Vars
    [Header("Wait Time")]
    [SerializeField] private int waitTime;
    [Header("Time Counter Text")]
    [SerializeField] private Text timeText;
    [Header("Desired Color Image")]
    [SerializeField] private Transform desiredColorImage;
    [Header("Game Timer Text")]
    [SerializeField] private Transform gameTimerTextTF;
    [Header("Managers")]
    [SerializeField] private NewRoundSetter newRoundSetter;
    [SerializeField] private DesiredColorSetter desiredColorSetter;
    [SerializeField] private TilesColorChecker tilesColorChecker;
    [SerializeField] private GameTimer gameTimer;
    #endregion

    #region Private Vars
    private int timeLeft;
    #endregion

    private void Awake() =>Instance = this;

    private void Start()=>InvokeTimer();

    #region Invoke Timer
    //Start's the timer
    private void InvokeTimer()
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
        timeText.text = timeLeft.ToString();

        if (timeLeft == 0)
        {
            isTimeOver = true;           
            timeText.gameObject.SetActive(false);
            newRoundSetter.StartRound();
            tilesColorChecker.StartCheck();
            desiredColorSetter.StartSetting();
            gameTimer.StartTimer();
            desiredColorImage.DOLocalMoveX(Screen.width-13, 0.75f);
            gameTimerTextTF.DOLocalMoveY(Screen.height, 0.75f);
            CancelInvoke(nameof(MinusTime));
        }
    }
    #endregion
}
