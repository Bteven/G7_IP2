using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLostPanelManager : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] private GameObject gameLostPanel;
    [SerializeField] private GameObject menuWarningPanel;
    [SerializeField] private GameObject restartWarningPanel;

    [Header("Rocket Settings")]
    [SerializeField] private GameObject rocket;
    [SerializeField] private Vector3 rocketTargetPosition;
    [SerializeField] private float rocketTakeoffDuration = 2.0f;

    public void TriggerGameLostSequence()
    {
        Time.timeScale = 0.0f;
        gameLostPanel.SetActive(true);
        StartCoroutine(RocketTakeoffCoroutine());   
    }

    private IEnumerator RocketTakeoffCoroutine()
    {
        Vector3 startPos = rocket.transform.position;
        Vector3 targetPos = startPos + Vector3.up * 25f;
        float elapsed = 0f;
        while (elapsed < rocketTakeoffDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / rocketTakeoffDuration);
            float acceleratedT = t * t;
            rocket.transform.position = Vector3.Lerp(startPos, targetPos, acceleratedT);
            yield return null;
        }
    }

    public void GoToMainMenu()
    {
        menuWarningPanel.SetActive(true);
    }

    public void ConfirmMenuYes()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ConfirmMenuNo()
    {
        menuWarningPanel.SetActive(false);
    }

    public void RestartGame()
    {
        restartWarningPanel.SetActive(true);
    }

    public void ConfirmRestartYes()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ConfirmRestartNo()
    {
        restartWarningPanel.SetActive(false);
    }
}