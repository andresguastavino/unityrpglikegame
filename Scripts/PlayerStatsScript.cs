using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsScript : MonoBehaviour
{

    public Slider healthBarSlider;
    public Slider staminaBarSlider;
    public Text healthBarText;
    public Text staminaBarText;
    public int maxHealth;
    public int currentHealth;
    public int maxStamina;
    public int currentStamina;

    public void SetMaxHealth(float health)
    {
        maxHealth = Mathf.FloorToInt(health);
    }

    public void SetMaxStamina(float stamina)
    {
        maxStamina = Mathf.FloorToInt(stamina);
    }

    public void SetHealth(float health)
    {
        currentHealth = Mathf.FloorToInt(health);
    }

    public void SetStamina(float stamina)
    {
        currentStamina = Mathf.FloorToInt(stamina);
    }

    private void Update()
    {
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
        staminaBarSlider.maxValue = maxStamina;
        staminaBarSlider.value = currentStamina;
        healthBarText.text = currentHealth + "/" + maxHealth;
        staminaBarText.text = currentStamina + "/" + maxStamina;
    }

}
