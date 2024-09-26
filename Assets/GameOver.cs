using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Guard 1" || other.gameObject.name == "Guard 2")
        {
            SceneManager.LoadSceneAsync("GameOver");
            SceneManager.UnloadSceneAsync("SampleScene");
        }
    }

}
