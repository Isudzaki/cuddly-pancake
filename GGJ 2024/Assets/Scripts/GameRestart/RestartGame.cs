using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class RestartGame : MonoBehaviour
{
    #region Restart Game
    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        SceneManager.LoadScene(scene.name);
    }
    #endregion
}
