using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;

    // Update is called once per frame
    void Update()
    {

    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("RunnerTeste");
    }
        public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
