using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class RestartGame : MonoBehaviour
{
    [SerializeField] private LevelChanger levelChanger;

    #region Restart Game
    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        levelChanger.FadeIn(scene.name);
    }
    #endregion
}
