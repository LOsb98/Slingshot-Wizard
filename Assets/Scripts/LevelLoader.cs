using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel(int levelID)
    {
        SceneManager.LoadScene(levelID);
    }

    public void NextLevel(int currentID)
    {
        SceneManager.LoadScene(currentID + 1);
    }

    public void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
