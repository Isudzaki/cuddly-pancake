using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] Image img;

    private string nameScene;

    private void Start()
    {
        img.DOFade(0, 1);
    }

    public void FadeIn(string name)
    {
        nameScene = name;
        DOTween.Sequence()
            .Append(img.DOFade(1,1))
            .AppendCallback(ChangeScene);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(nameScene);
    }
}
