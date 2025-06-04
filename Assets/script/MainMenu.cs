using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;

    public GameObject SettingsWindow;

    public void startGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void SettingsBouton()
    {
        SettingsWindow.SetActive(true);
    }

    public void CloseSettingsWindows()
    {
        SettingsWindow.SetActive(false);
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("credit");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
