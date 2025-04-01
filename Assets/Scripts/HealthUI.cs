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

    [SerializeField]
    private GameObject vignette;

    void Start()
    {
        StartCoroutine(FlashVignette(2, 0.15f)); //Flashes twice, 0.15 seconds apart
    }

    IEnumerator FlashVignette(int flashes, float delay)
    {
        for (int i = 0; i < flashes; i++)
        {
            vignette.SetActive(true);
            yield return new WaitForSeconds(delay);
            vignette.SetActive(false);
            yield return new WaitForSeconds(delay);
        }
    }
    void Update()
    {
        SetNumberText(healthController.playerHealth); //sets health counter UI to the current health of the finish line
    }

    private void SetNumberText(float value)
    {
        numberText.text = value.ToString();
    }
}
