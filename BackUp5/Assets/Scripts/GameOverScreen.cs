using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public GameObject PainelG; //Painel Game Over
    public void Restart()
    {
        SceneManager.LoadScene("Runner");
    }

    void Start()
    {
        PainelG.SetActive(false);
        Time.timeScale = 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // só funciona se o Player tiver a tag "Player"
        {
            PainelG.SetActive(true);
            Time.timeScale = 0;
        }
    }
}

