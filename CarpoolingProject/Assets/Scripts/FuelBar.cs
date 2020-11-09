    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    public Slider slider;
    //placeholder for starting fuel count
    private float maxFuel = 100f;
    public static bool fuelEmpty = false;

    private void Start()
    {
        slider.maxValue = maxFuel;
    }
    
    private void Update()
    {
        slider.value = maxFuel - GameManager.usedFuel;
        if (slider.value <= 0)
        {
            GameManager.outOfFuel();
            fuelEmpty = true;
        }
    }
    public static float isFuelEmpty(float speed)
    {
        if (fuelEmpty == true)
        {
            return 0;
        }
        else
        {
            return speed;
        }
    }
    //not sure what to doe with this
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
