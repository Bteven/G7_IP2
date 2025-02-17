using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Code for health counter on screen
public class HealthUI : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI numberText;

    [SerializeField]
    private HealthController healthController;

    void Update()
    {
        SetNumberText(healthController.currentHealth); //sets health counter UI to the current health of the finish line
    }

    private void SetNumberText(float value)
    {
        numberText.text = value.ToString();
    }
}
