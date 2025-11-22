using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance;
    public TMP_Text resultText;
    /* TMP variable for score textbox */
    //public TMP_Text scoreText;
    /* By default the total possible score is 100 */
    public float displayTime = 1.0f;

    private float timer = 0f;
    private Circle currentCircle;
    public void SetCurrentCircle(Circle c)
    {
        currentCircle = c;
    } 

    void Start() {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Update()
    {
    }  
    
    public void ShowResult(string result)
    {
        resultText.text = result;
        timer = displayTime;

        //change color
        switch (result)
        {
            case "Perfect":
                resultText.color = Color.yellow;
                break;
            case "Good":
                resultText.color = Color.green;
                break;
            case "Miss":
                resultText.color = Color.red;
                break;
        }
    }

    public void PlayGame(){
        SceneManager.LoadScene("Level_Select_Scene");
        // Please also reset all static variables here, for new games!
    }

    public void RestartGame(){
        SceneManager.LoadScene("Menu_Scene");
        // Please also reset all static variables here, for new games!
    }

    public void CreditsScene(){
        SceneManager.LoadScene("Credits_Scene");
        // Please also reset all static variables here, for new games!
    }

    public void QuitGame(){
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
    }

}
