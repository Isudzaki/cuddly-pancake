using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public sealed class EnemyLaugh : MonoBehaviour
{
    #region Public Vars
    public static EnemyLaugh Instance { get; private set; }

    private int _score;
    #endregion

    #region Score
    public int Score
    {
        get => _score;

        set
        {
            if (_score == value) return;

            _score = value;

            textSlider.DOValue(_score, 0.5f);
            LaughSound.Instance.Laugh();

            if (_score < 0) Score = 0;
            else if (_score > textSlider.maxValue) Score = Mathf.RoundToInt(textSlider.maxValue);
        }
    }
    #endregion

    #region Serialized Vars
    [Header("Text Slider")]
    [SerializeField] private Slider textSlider;
    #endregion

    #region Awake
    private void Awake() => Instance = this;
    #endregion
}
