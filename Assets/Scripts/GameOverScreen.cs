using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public GameObject PainelG; //Painel Game Over
    public void Restart()
    {
        SceneManager.LoadScene("Teste");
    }

    void Start()
    {
        PainelG.SetActive(false);
        Time.timeScale = 1;
    }
private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        PainelG.SetActive(true);
        Time.timeScale = 0;
    }
}
}

