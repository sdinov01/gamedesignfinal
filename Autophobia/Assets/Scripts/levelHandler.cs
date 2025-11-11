using UnityEngine;
using UnityEngine.SceneManagement;

public class levelHandler : MonoBehaviour
{
    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
