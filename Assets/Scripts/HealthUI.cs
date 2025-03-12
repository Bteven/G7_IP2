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
    private PlayerHealthManager healthController;

    void Update()
    {
        SetNumberText(healthController.playerHealth); //sets health counter UI to the current health of the finish line
    }

    private void SetNumberText(float value)
    {
        numberText.text = value.ToString();
    }
}
