using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider healthSlider;
    public HealthSystem playerHealth;
    private ParasiteController parasite;

    void Start()
    {
        parasite = FindObjectOfType<ParasiteController>();
        healthSlider.minValue = 0;
        healthSlider.maxValue = playerHealth.maxHealth;
    }

    void Update()
    {
        if (parasite == null) return;

        HealthSystem activeHealth = parasite.GetCurrentHealth();
        if (activeHealth != null)
        {
            healthSlider.maxValue = activeHealth.maxHealth;
            healthSlider.value = activeHealth.currentHealth;
        }
    }
}