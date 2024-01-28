using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Viewers : MonoBehaviour
{
    [SerializeField] private int startNumber = 1;
    [SerializeField] private float transitionDuration = 1f;

    [SerializeField] private Text numberText;

    [HideInInspector] public int endNumber;

    [HideInInspector] public static Viewers instance;

    private Coroutine transitionCoroutine;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        endNumber = startNumber;
        UpdateNumber(startNumber);
    }

    public void UpdateEndNumber(int newEndNumber)
    {
        if (transitionCoroutine != null && newEndNumber!<50)
        {
            StopCoroutine(transitionCoroutine);
        }

        endNumber += newEndNumber;
        transitionCoroutine = StartCoroutine(SmoothTransition());
    }

    IEnumerator SmoothTransition()
    {
        float currentTime = 0f;
        int startValue = int.Parse(numberText.text);

        while (currentTime <= transitionDuration)
        {
            float currentValue = Mathf.Lerp(startValue, endNumber, currentTime / transitionDuration);
            UpdateNumber(Mathf.RoundToInt(currentValue));
            currentTime += Time.deltaTime;
            yield return null;
        }

        UpdateNumber(endNumber);
    }

    private void UpdateNumber(int value)
    {
        numberText.text = value.ToString();
    }
}
