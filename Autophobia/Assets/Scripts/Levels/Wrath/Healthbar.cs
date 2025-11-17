using UnityEngine;

using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider healthSlider;
    public Image fill;   

    public float maxHealth = 100;
    private float currentHealth;

    public Color fullHealthColor = Color.green;
    public Color midHealthColor = Color.yellow;
    public Color lowHealthColor = Color.red;

    void Start()
    {
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        UpdateColor();
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthSlider.value = currentHealth;

        UpdateColor();
    }

    void UpdateColor()
    {
        float percent = currentHealth / maxHealth;

        if (percent > 0.6f)
            fill.color = fullHealthColor;
        else if (percent > 0.3f)
            fill.color = midHealthColor;
        else
            fill.color = lowHealthColor;
    }
}
