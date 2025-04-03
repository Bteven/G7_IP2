using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Code for health counter on screen
public class HealthUI : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI numberText;

    [SerializeField]
    private PlayerHealthManager healthController;

    [SerializeField]
    private GameObject vignette;

    [SerializeField]
    private float fadeDelay = 1f;

    void Start()
    {
        vignette.SetActive(true);
        StartCoroutine(Fade(vignette.GetComponent<Image>(), fadeDelay, false));
        Invoke(nameof(DelayedSetActive), fadeDelay);
    }

    void DelayedSetActive()
    {
        vignette.SetActive(false);
    }

    IEnumerator Fade(Image image, float fadeTime, bool fadeIn)
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < fadeTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            if (fadeIn)
            {
                c.a = Mathf.Clamp01(elapsedTime / fadeTime);
            }
            else
            {
                c.a = 1f - Mathf.Clamp01(elapsedTime / fadeTime);
            }
            image.color = c;
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
