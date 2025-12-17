using UnityEngine;
using UnityEngine.SceneManagement;

public class end_quit : MonoBehaviour
{
    public void GoToMenu()
    {
        Debug.Log("Go to Menu");

        SceneManager.LoadScene("Menu_Scene");
    }
}