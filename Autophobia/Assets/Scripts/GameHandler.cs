using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public TMP_Text resultText; 
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Current circle parent: " + currentCircle.transform.parent.name);
            if (currentCircle != null && currentCircle.CanBeClicked())
            {
                Debug.Log("clickfunction");
                currentCircle.OnClick();
            }
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
}
