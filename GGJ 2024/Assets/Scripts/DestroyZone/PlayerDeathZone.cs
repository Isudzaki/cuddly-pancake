using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public sealed class PlayerDeathZone: MonoBehaviour
{
    #region Serialized Vars
    [Header("Loose Screen")]
    [SerializeField] private GameObject looseScreen;
    [Header("Respawn Timer")]
    [SerializeField] private RespawnTimer respawnTimer;
    [Header("Mixer Groups")]
    [SerializeField] private AudioMixerGroup[] mixerGroups;
    [Header("Audio Death")]
    [SerializeField] private AudioSource deathSource;
    #endregion

    #region Trigger Enter
    //Set's the trigger zone opportunity to kill player
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController playerController))
        {
            deathSource.Play();
            playerController.enabled = false;
            playerController.GetComponent<PlayerController>().haveItem = false;
            Destroy(playerController.GetComponent<PlayerThrow>().bomb);
            Transform looseScreenTF = looseScreen.transform;
            looseScreen.transform.position = new Vector3(Screen.width*2,looseScreenTF.position.y, looseScreenTF.position.z);
            looseScreen.transform.DOLocalMoveX(0, 0.75f).SetEase(Ease.InBounce);
            looseScreen.SetActive(true);
            respawnTimer.InvokeTimer();
            for(int i = 0; i < mixerGroups.Length; i++)
            {
                mixerGroups[i].audioMixer.DOSetFloat("LowPass", 300, 0.75f);
            }
            PlayerLaughIndicator.Instance.Score -= PlayerLaughIndicator.Instance.Score/2;
        }
    }
    #endregion
}
