using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadSceneAsync("SampleScene");
        SceneManager.UnloadSceneAsync("GameOver");
    }
}
