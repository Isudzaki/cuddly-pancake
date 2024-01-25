using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioChange : MonoBehaviour
{
    #region Serialized Vars
    [Header("Sliders")]
    [SerializeField] private Slider musicSlider,soundsSlider;
    [Header("Audio Mixers")]
    [SerializeField] private AudioMixer musicMixer,soundsMixer;
    #endregion

    #region Start
    private void Start()
    {
        MusicChange();
        SoundsChange();
    }
    #endregion

    #region Music Change
    public void MusicChange()
    {
        musicMixer.SetFloat("Volume",musicSlider.value);
    }
    #endregion

    #region Sounds Change
    public void SoundsChange()
    {
        soundsMixer.SetFloat("Volume", soundsSlider.value);
    }
    #endregion
}
