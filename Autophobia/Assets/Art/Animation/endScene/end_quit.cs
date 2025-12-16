using UnityEngine;

public class end_quit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void QuitGame()
    {
        Debug.Log("you quit.");
        Application.Quit();
    }
}
