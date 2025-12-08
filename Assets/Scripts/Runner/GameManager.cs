using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject painelGameOver;
    public TMP_Text pontuacao;
    public Pontuacao pontos;

    private void Awake()
    {
        instance = this;
    }

    public void MostrarGameOver()
    {
        painelGameOver.SetActive(true);
        pontuacao.text = pontos.score.ToString() + "pts";
        Debug.Log(pontos.score);  
        Time.timeScale = 0f;
    }
}
