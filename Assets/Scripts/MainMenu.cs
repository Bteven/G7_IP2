using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject videoPlayer;
    public GameObject Canvas;
    public void LoadGame()
    {
        videoPlayer.SetActive(true);
        Canvas.SetActive(false);
        Invoke("PlayGame", 7);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenOptions()
    {
        //SceneManager.LoadScene(0); // Load the options scene.

        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the game.
        Debug.Log("Game has been quit."); // Log to confirm quit action in the Unity Editor.
    }
}
