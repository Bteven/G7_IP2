using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI numberText;

    [SerializeField]
    private HealthController healthController;

    void Start()
    {
        SetNumberText(healthController.currentHealth);
    }

    private void SetNumberText(float value)
    {
        numberText.text = value.ToString();
    }
}
