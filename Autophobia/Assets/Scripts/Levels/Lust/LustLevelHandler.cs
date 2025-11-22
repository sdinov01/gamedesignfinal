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
    public BossShooter bossShooter2; 
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
            if (platformMover == null || bossShooter == null || bossShooter2 == null)
            {
                Debug.LogWarning("LustLevelHandler: platformMover or bossShooter is not assigned!");
            }
            else
            {
                int currPlatform = platformMover.getCurrPosition();
                Debug.Log("Current platform: " + currPlatform);

                BossProjectile proj  = bossShooter.getCurrLaneProj(currPlatform);
                BossProjectile proj2 = bossShooter2.getCurrLaneProj(currPlatform);

                BossProjectile toHit = null;
                BossShooter shooterOfToHit = null;

                // Decide which projectile (if any) we should hit
                if (proj != null && proj2 != null)
                {
                    float t1 = Mathf.Abs(proj.GetTimeToHit());
                    float t2 = Mathf.Abs(proj2.GetTimeToHit());

                    if (t1 <= t2)
                    {
                        toHit = proj;
                        shooterOfToHit = bossShooter;
                    }
                    else
                    {
                        toHit = proj2;
                        shooterOfToHit = bossShooter2;
                    }
                }
                else if (proj != null)
                {
                    toHit = proj;
                    shooterOfToHit = bossShooter;
                }
                else if (proj2 != null)
                {
                    toHit = proj2;
                    shooterOfToHit = bossShooter2;
                }

                if (toHit != null && shooterOfToHit != null)
                {
                    shooterOfToHit.removeProj(currPlatform);
                    toHit.OnClick();
                }
                else
                {
                    ShowResult("Miss");
                }
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
