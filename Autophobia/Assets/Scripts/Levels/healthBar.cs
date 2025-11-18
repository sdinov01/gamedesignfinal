using UnityEngine;
using UnityEngine.UI;
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
