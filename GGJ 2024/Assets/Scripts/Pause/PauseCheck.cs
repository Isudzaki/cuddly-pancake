using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PauseCheck : MonoBehaviour
{
    #region Serialized Vars
    [Header("Panel")]
    [SerializeField] private GameObject panel;
    [Header("Respawn Timer")]
    [SerializeField] private RespawnTimer respawnTimer;
    [Header("Buttons")]
    [SerializeField] private Button playButton,exitButton;
    #endregion

    #region Private Vars
    private bool canchange = true;
    #endregion

    #region Public Vars
    [HideInInspector]
    public bool opened = false;

    public static PauseCheck Instance;
    #endregion

    #region Awake
    private void Awake() => Instance = this;
    #endregion

    #region Update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !respawnTimer.isPlayerDied && StartTimer.Instance.isTimeOver==true)
            Check();
    }
    #endregion

    #region Check
    public void Check()
    {
        if (opened == false && canchange == true)
        {
            Open();
        }
        if (opened == true && canchange == true)
        {
            Time.timeScale = 1;
            Close(0.5f);
        }
    }
    #endregion

    #region Open
    private void Open()
    {
        playButton.interactable = true;
        exitButton.interactable = true;
        canchange = false;
        Invoke(nameof(Changing), 0.47f);
        DOTween.Sequence()
        .Append(panel.transform.DOScaleX(1, 0.5f))
        .AppendCallback(Freeze);
        panel.transform.DOScaleY(1f, 0.5f);
        opened = true;
    }
    #endregion

    #region Freeze
    private void Freeze()
    {
        Time.timeScale = 0;
    }
    #endregion

    #region Close
    private void Close(float speed)
    {
        playButton.interactable = false;
        exitButton.interactable = false;
        canchange = false;
        Invoke(nameof(Changing), 0.47f);
        panel.transform.DOScaleX(0, speed);
        panel.transform.DOScaleY(0, speed);
        opened = false;
    }
    #endregion

    #region Changing
    private void Changing()
    {
        canchange = true;
    }
    #endregion
}
