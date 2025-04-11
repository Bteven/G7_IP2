using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLostPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject gameLostPanel;
    [SerializeField] private GameObject menuWarningPanel;
    [SerializeField] private GameObject restartWarningPanel;

    public void ToggleGameLost()
    {
        bool isGameLost = gameLostPanel.activeSelf;
        gameLostPanel.SetActive(!isGameLost);
        Time.timeScale = isGameLost ? 1.0f : 0.0f;
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
