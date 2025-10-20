using UnityEngine;
using TMPro;

public class MoedasCounter : MonoBehaviour
{
    public static MoedasCounter instance;
    private TMP_Text Moedatxt;
    public int moedasatuais = 0;
    public GameObject Vitoria;
    public int moedasParaVitoria = 30; //Podemos definir melhor uma condição de vitoria
    private bool vitoriaAtivada = false; 

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return; 
        }

        // Busca o texto de moedas na HUD
        GameObject textoObj = GameObject.Find("TextoMoedas");
        if (textoObj != null)
            Moedatxt = textoObj.GetComponent<TMP_Text>();

        // Busca o objeto de vitória por tag e desativa no início
        Vitoria = GameObject.FindGameObjectWithTag("Vitoria");
        if (Vitoria != null)
            Vitoria.SetActive(false);
    }

    void Start()
    {
        AtualizarTexto();
    }

    void Update()
    {
        // Checagem com segurança para ativar só 1 vez
        if (!vitoriaAtivada && moedasatuais >= moedasParaVitoria)
        {
            vitoriaAtivada = true; // Marca que a vitória já foi ativada

            if (Vitoria != null)
                Vitoria.SetActive(true);

            Time.timeScale = 0; // Pausa o jogo
        }
    }

    public void AumentoDeMoedas(int v)
    {
        moedasatuais += v;
        AtualizarTexto();
    }

    private void AtualizarTexto()
    {
        if (Moedatxt != null)
            Moedatxt.text = moedasatuais.ToString() + "/" + moedasParaVitoria.ToString();
    }
}
