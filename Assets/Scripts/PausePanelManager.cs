using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject menuWarningPanel;
    [SerializeField] private GameObject restartWarningPanel;

    public void TogglePause()
    {
        bool isPused = pausePanel.activeSelf;
        pausePanel.SetActive(!isPused);
        Time.timeScale = isPused ? 1.0f : 0.0f;
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

    public void SettingsPanel()
    {
        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void Back()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
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
