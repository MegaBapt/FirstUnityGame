using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
	public GameObject SettingsWindowUI;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }
    void Paused()
	{
        mouvement_joueur.instance.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume()
    {
        mouvement_joueur.instance.enabled = true;
        pauseMenuUI.SetActive(false);
		SettingsWindowUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void MainMenuButon()
    {
		Resume();
    	SceneManager.LoadScene("main_menu");
	}

}
