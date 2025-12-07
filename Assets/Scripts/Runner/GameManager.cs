using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject painelGameOver;

    private void Awake()
    {
        instance = this;
    }

    public void MostrarGameOver()
    {
        painelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }
}
