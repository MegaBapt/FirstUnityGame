using UnityEngine;
using UnityEngine.UI;

public class barre_de_vie : MonoBehaviour
{
    public Slider slider;

    public Gradient gradiant;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradiant.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        if(slider.value < health+1)
        { 
            fill.color = gradiant.Evaluate(slider.normalizedValue);
        }else
        {
            fill.color = new Color(1f, 1f, 0);
        }

    }
}
