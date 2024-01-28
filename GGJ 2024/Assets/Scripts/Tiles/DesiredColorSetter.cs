using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public sealed class DesiredColorSetter : MonoBehaviour
{
    #region Public Vars
    [HideInInspector]
    public Color desiredColor;
    [HideInInspector]
    public int desiredNum;
    [HideInInspector]
    public static DesiredColorSetter instance;
    #endregion

    #region Serialized Vars
    [Header("Desired Color Image")]
    [SerializeField] private Image desColorImg;
    #endregion

    #region Awake
    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region Start Timer
    //Invoking SetColor void
    public void StartSetting()=> InvokeRepeating(nameof(SetColor), 0, 10f);
    #endregion

    #region SetColor
    //Change the desired color randomly
    private void SetColor()
    {
        TileColor desColor = ColorDatabase.Colors[Random.Range(0, ColorDatabase.Colors.Length)];
        desiredColor = desColor.color;
        desColorImg.DOColor(desiredColor,0.75f);
        desiredNum = desColor.num;
        //Repeating function
        Invoke(nameof(SetColor), 10f);
    }
    #endregion
}
