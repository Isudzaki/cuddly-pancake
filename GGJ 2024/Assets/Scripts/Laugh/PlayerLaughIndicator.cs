using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public sealed class PlayerLaughIndicator : MonoBehaviour
{
    public static PlayerLaughIndicator Instance { get; private set; }

    private int _score;

    public int Score
    {
        get => _score;

        set
        {
            if (_score == value) return;

            _score = value;

            textSlider.DOValue(_score,0.5f);
        }
    }

    [SerializeField] private Slider textSlider;

    private void Awake() => Instance = this;
}
