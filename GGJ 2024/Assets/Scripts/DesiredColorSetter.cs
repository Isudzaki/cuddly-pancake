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

    #region Start
    //Invoking SetColor void
    private void Start()=> InvokeRepeating(nameof(SetColor), 0, 10f);
    #endregion

    #region SetColor
    //Change the desired color randomly
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
