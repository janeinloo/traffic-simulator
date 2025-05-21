using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        Debug.Log("Trying to load scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
        Debug.Log("Loading scene: " + sceneName);
    }
    public void LoadSceneByIndex(int sceneIndex)
    {
        Debug.Log("Trying to load scene: " + sceneIndex);
        SceneManager.LoadScene(sceneIndex);
        Debug.Log("Loading scene at index: " + sceneIndex);
    }

}
