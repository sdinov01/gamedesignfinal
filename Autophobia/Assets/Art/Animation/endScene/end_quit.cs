using UnityEngine;
using UnityEngine.SceneManagement;

public class end_quit : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Go to Menu");

        SceneManager.LoadScene("Menu_Scene");
    }
}