using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalorieBar : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI caloriesCounter;


    public GameObject playerState;


    private float currentCalories, maxCalories;


   
    void Awake()
    {
        slider = GetComponent<Slider>();


    }


   
    void Update()
    {
        currentCalories = playerState.GetComponent<PlayerState>().currentCalories;
        maxCalories = playerState.GetComponent<PlayerState>().maxCalories;


        float fillValue = currentCalories / maxCalories;
        slider.value = fillValue;


        caloriesCounter.text = currentCalories + "/" + maxCalories;
    }
}




