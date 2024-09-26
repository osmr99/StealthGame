using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadSceneAsync("SampleScene");
        SceneManager.UnloadSceneAsync("Victory");
    }
}
