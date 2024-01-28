using UnityEngine;

public class LaughSound : MonoBehaviour
{
    [SerializeField] private AudioSource laughSound;

    public static LaughSound Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Laugh()
    {
        laughSound.Play();
    }
}
