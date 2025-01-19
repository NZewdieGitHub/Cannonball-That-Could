using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Slider Fields
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    /// <summary>
    /// Set the health bar to maximum
    /// </summary>
    /// <param name="health"></param>
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        // make health yellow
        fill.color = gradient.Evaluate(1f);
    }
    /// <summary>
    /// Modifies healthbar when neccessary
    /// </summary>
    /// <param name="health"></param>
    public void SetBar(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
