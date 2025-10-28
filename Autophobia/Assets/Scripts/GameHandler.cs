using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public TMP_Text resultText; 
    public float displayTime = 1.0f;

    private float timer = 0f;

    void Start() {
        resultText.text = "";
    }
    void Update() {
        //let result disappear
        if (timer > 0) {
            timer -= Time.deltaTime;
            if (timer <= 0){
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
}
