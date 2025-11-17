using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private string playSceneName = "GameScene";
    [SerializeField] private GameObject TutorialPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject creditPanel;
    [SerializeField] private GameObject shopPanel;
    
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

    public void OpenCredit()
    {
        creditPanel.SetActive(true);
    }

    public void CloseCredit()
    {
        creditPanel.SetActive(false);
    }
    public void OpenTutorial()
    {
        TutorialPanel.SetActive(true);
    }

    public void CloseTutorial()
    {
        TutorialPanel.SetActive(false);
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
