using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class healthBar : MonoBehaviour
{
    private float health;
    [SerializeField] private Image healthImage;
 
    void Start()
    {
        /* Default is 100 health */
        health = 100f;
    }

    void Update()
    {
        /* If the player is dead, go to Game Over scene */
        if (health <= 0)
        {
            SceneManager.LoadScene("GameOver_Scene");
        }
    }

    public void setTotalHealth(float totalHealth)
    {
        health = totalHealth;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        healthImage.fillAmount = health / 100f;
    }

    public float healthLeft()
    {
        return health;
    }
}
