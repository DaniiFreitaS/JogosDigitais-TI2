using UnityEngine;
using TMPro;

public class ResultadoFinal : MonoBehaviour
{
    public Pontuacao pontuacao;          //Pega os dados do script Pontuacao / Funciona assim, coloca o script e da um novo nome a ele, como se tivesse criando uma variavel, soq vc pode usar os dados que estão dentro dele
    public MoedasCounter moedasCounter;  // Pega os dados do script MoedasCounter
    public TMP_Text resultadoDerrotaTxt;            //Pega onde q é o texto para colocar os dados
    public TMP_Text resultadoVitoriaTxt;           //Pega onde q é o texto para colocar os dados
    public int total;

    void Update()
    {
        int pontos = Mathf.FloorToInt(pontuacao.time);   //pega o Time do scrpit de Pontuação(por isso "pontuacao.time") para transofrmar em int de pontos
        int moedas = moedasCounter.moedasatuais;         //faz o mesmo (so não tendo que converter) com as moedas

        total = pontos * moedas;                     //multiplica os dois e coloca no total :p


        resultadoDerrotaTxt.text = pontos + "\n x " + moedas + "\n" + "-------------\n" + total.ToString(); //Pega o texto, e coloca a pontuação, em outra linha "x" + o numero de moedas, e depois faz um traço para colocar o resultado :3
        resultadoVitoriaTxt.text = pontos + "\n x " + moedas + "\n" + "-------------\n" + total.ToString(); //Pega o texto, e coloca a pontuação, em outra linha "x" + o numero de moedas, e depois faz um traço para colocar o resultado :3
    }
    
}