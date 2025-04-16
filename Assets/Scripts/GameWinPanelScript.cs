using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinPanelScript : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] private GameObject gameWinPanel;
    [SerializeField] private GameObject menuWarningPanel;
    [SerializeField] private GameObject restartWarningPanel;
    public PlayerHealthManager playerHealth;
    public TextMeshProUGUI healthAtTheEndText;

    public void WinGame()
    {

        healthAtTheEndText.text = "You Won With " + playerHealth.playerHealth + " Health Left";
        gameWinPanel.SetActive(true);
        Time.timeScale = 0.0f;
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
