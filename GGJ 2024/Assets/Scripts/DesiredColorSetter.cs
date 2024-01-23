using UnityEngine.UI;
using UnityEngine;

public sealed class DesiredColorSetter : MonoBehaviour
{
    #region Public Vars
    [HideInInspector]
    public Color desiredColor;
    [HideInInspector]
    public int desiredNum;
    #endregion

    #region Serialized Vars
    [Header("Desired Color Image")]
    [SerializeField] private Image desColorImg;
    #endregion

    //Invoking SetColor void
    #region Start
    private void Start()=> InvokeRepeating(nameof(SetColor), 0, 10f);
    #endregion

    //Change the desired color randomly
    #region SetColor
    private void SetColor()
    {
        TileColor desColor = ColorDatabase.Colors[Random.Range(0, ColorDatabase.Colors.Length)];
        desiredColor = desColor.color;
        desColorImg.color = desiredColor;
        desiredNum = desColor.num;
        //Repeating function
        Invoke(nameof(SetColor), 10f);
    }
    #endregion
}
