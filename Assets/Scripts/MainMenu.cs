using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayCallback() {
        SceneManager.LoadScene("World");
	}

    public void MainMenuCallback() {
        SceneManager.LoadScene("Menu");
    }

    public void QuitCallback()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
