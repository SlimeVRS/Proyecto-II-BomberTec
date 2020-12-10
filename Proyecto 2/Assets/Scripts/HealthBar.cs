using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public Text healthText;

    public Text speedText;

    public void SetMaxHealth(int health, float speed)
    {
        string[] tmpHealth = healthText.text.Split(':');  
        string[] tmpSpeed = speedText.text.Split(':');
        
        healthText.text = tmpHealth[0] + ": " + health;
        speedText.text = tmpSpeed[0] + ": " + speed;

        slider.maxValue = health;
        slider.value = health;
    }
   
    public void SetHealth(int health, float speed)
    {
        string[] tmpHealth = healthText.text.Split(':');
        slider.value = health;
        healthText.text = tmpHealth[0] + ": " + health;

        string[] tmpSpeed = speedText.text.Split(':');
        speedText.text = tmpHealth[0] + ": " + speed;
    }
}
