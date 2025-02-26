using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyUI : MonoBehaviour
{
    public TextMeshProUGUI currencyText;

    void Update()
    {
        currencyText.text = CurrencyManager.Instance.CurrentCurrency.ToString();
    }
}
