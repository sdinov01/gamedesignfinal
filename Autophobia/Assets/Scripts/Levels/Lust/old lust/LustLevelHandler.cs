using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LustLevelHandler : MonoBehaviour
{
    public TMP_Text resultText;         
    public healthBar health;

    public float displayTime = 1.0f;

    private float timer = 0f;

    public BossShooter bossShooter;         
    public linearPlatMove platformMover;    

    void Start()
    {
        if (resultText != null)
        {
            resultText.text = "";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (platformMover == null || bossShooter == null)
            {
                Debug.LogWarning("LustLevelHandler: platformMover or bossShooter is not assigned!");
            }
            else
            {
                int currPlatform = platformMover.getCurrPosition();
                Debug.Log("Current platform: " + currPlatform);


                BossProjectile toHit = null;
                BossShooter shooterOfToHit = null;
            }
        }
    }

    public void ShowResult(string result)
    {
        if (resultText == null) return;

        resultText.text = result;
        timer = displayTime;

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

    public void UpdateHealth(float damage) 
    {
        health.takeDamage(damage);
    }
}
