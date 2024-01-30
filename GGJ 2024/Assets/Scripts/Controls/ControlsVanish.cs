using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ControlsVanish : MonoBehaviour
{
    [SerializeField] private Text text;
    private void Start()
    {
        Invoke(nameof(VanishControls), 7f);
    }

    private void VanishControls()
    {
        text.DOFade(0, 1);
    }
}
