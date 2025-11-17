using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class MoedasCounter : MonoBehaviour
{
    public static MoedasCounter instance;
    private TMP_Text Moedatxt;

    public int moedasatuais = 0;
    public int vida = 30, vidaMax = 30; // vidaMax inicializada para segurança
    public int VidaPerdida;
    public GameObject Vitoria;
    public int moedasParaVitoria = 100; // Condição de vitória
    private bool vitoriaAtivada = false;

    private GameOverScreen gameOverScreen;

    void Awake()
    {
        // Padrão Singleton
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Cálculo do dano (corrigido para usar float)
        VidaPerdida = (int)math.round((float)moedasParaVitoria / 10f);
        if (vidaMax == 0) vidaMax = vida;

        GameObject textoObj = GameObject.Find("TextoMoedas");
        if (textoObj != null)
            Moedatxt = textoObj.GetComponent<TMP_Text>();

        Vitoria = GameObject.FindGameObjectWithTag("Vitoria");
        if (Vitoria != null)
            Vitoria.SetActive(false);

        gameOverScreen = FindObjectOfType<GameOverScreen>();
    }

    void Start()
    {
        AtualizarTexto();
    }

    void Update()
    {
        // Condição de vitória
        if (!vitoriaAtivada && moedasatuais >= moedasParaVitoria)
        {
            vitoriaAtivada = true;
            if (Vitoria != null) Vitoria.SetActive(true);
            Time.timeScale = 0;
        }

        // Condição de derrota / Limite de vida
        if (vida <= 0)
        {
            vida = 0;
            AtivarGameOver();
        }
        if (vida > vidaMax)
        {
            vida = vidaMax;
        }
    }

    public void AumentoDeMoedas(int v)
    {
        moedasatuais += v;
        if (vida < vidaMax)
            vida += v;

        AtualizarTexto();
    }

    // MÉTODO PÚBLICO CHAMADO PELO PLAYERCONTROLLER AO TOMAR DANO
    // Retorna true se o jogo terminar (Game Over)
    public bool AplicarDano()
    {
        vida -= VidaPerdida;
        moedasatuais -= VidaPerdida;

        if (vida < 0) vida = 0;
        if (moedasatuais < 0) moedasatuais = 0;

        AtualizarTexto();

        if (vida <= 0)
        {
            AtivarGameOver();
            return true; // É Game Over
        }
        return false; // Não é Game Over
    }

    private void AtualizarTexto()
    {
        if (Moedatxt != null)
            // Formato corrigido para exibir todas as métricas sem conflito
            Moedatxt.text = "Moedas: " + moedasatuais + "/" + moedasParaVitoria + " | Vidas: " + vida + "/" + vidaMax;
    }

    private void AtivarGameOver()
    {
        if (gameOverScreen != null && gameOverScreen.PainelG != null)
        {
            gameOverScreen.PainelG.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Debug.LogWarning("⚠️ GameOverScreen ou PainelG não encontrados!");
        }
    }
}