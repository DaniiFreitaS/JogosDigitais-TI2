using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Mathematics;
public class Pontuacao : MonoBehaviour
{
    public float time, PontosPorSeg;
    public ResultadoFinal PontuacaoTotal;
    public TextMeshProUGUI highscoreText;
    int highscore = 0;
    public TextMeshProUGUI timertxt; //Texto da UI
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0); //Pega o score salvo, mas se não tiver fica como zero
        highscoreText.text = highscore.ToString() + " pts";
    }

    // Update is called once per frame
    void Update()
    {
        time += PontosPorSeg * Time.deltaTime; //Adiciona tempo ao tempo
        timertxt.text = Mathf.FloorToInt(time).ToString() + " m"; //Converte o numero para string, para aparecer na tela
        if (PontuacaoTotal != null && PontuacaoTotal.total > highscore) //Compara o tempo atual com o antigo
        {
            highscore = PontuacaoTotal.total; //Adapta o time de float pra int
            PlayerPrefs.SetInt("highscore", highscore); //salva o highscore
            highscoreText.text = highscore.ToString() + " pts"; //Converte o int para texto e coloca um "m" no final pra indicar os metros :p
        }

    }
}

//https://www.youtube.com/watch?v=NdRneaQUkA4
//https://www.youtube.com/watch?v=YUcvy9PHeXs&t
