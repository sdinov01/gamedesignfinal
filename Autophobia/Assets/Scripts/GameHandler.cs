using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public TMP_Text resultText;
    /* TMP variable for score textbox */
    public TMP_Text scoreText;
    public float displayTime = 1.0f;

    private float timer = 0f;
    private circle currentCircle;
    public void SetCurrentCircle(circle c)
    {
        currentCircle = c;
    } 

    void Start() {
        resultText.text = "";
    }
    void Update()
    {
        // Debug.Log("update");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Current circle parent: " + currentCircle.transform.parent.name);
            currentCircle.OnClick();
        }
        
        //let result disappear
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                resultText.text = "";
            }
        }
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
        SceneManager.LoadScene("Sloth_Test");
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

    public void UpdateScore(float score)
    {
        scoreText.text = $"Score: {score:F1}%";
    }
}
