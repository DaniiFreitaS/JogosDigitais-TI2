using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private string playSceneName = "GameScene"; 
    [SerializeField] private GameObject optionsPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene(playSceneName);
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
