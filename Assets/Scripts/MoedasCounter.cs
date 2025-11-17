using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class MoedasCounter : MonoBehaviour
{
    public static MoedasCounter instance;
    private TMP_Text Moedatxt;

    [Header("Moedas da Fase")]
    public int moedasatuais = 0;         

    [Header("Vida do Jogador")]
    public int vida = 30;                
    public int vidaMax = 30;             
    public int VidaPerdida;              

    [Header("Sistema de Vitória")]
    public GameObject Vitoria;
    public int moedasParaVitoria = 100;
    private bool vitoriaAtivada = false;

    private GameOverScreen gameOverScreen;

    void Awake()
    {
        // Singleton
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Vida Máxima não pode ser 0
        if (vidaMax <= 0)
            vidaMax = vida;

        // Cálculo correto da perca de vida
        VidaPerdida = (int)math.round((float)moedasParaVitoria / 10f);

        // Texto
        GameObject textoObj = GameObject.Find("TextoMoedas");
        if (textoObj != null)
            Moedatxt = textoObj.GetComponent<TMP_Text>();

        // Vitória
        Vitoria = GameObject.FindGameObjectWithTag("Vitoria");
        if (Vitoria != null)
            Vitoria.SetActive(false);

        // Game Over
        gameOverScreen = FindObjectOfType<GameOverScreen>();
    }

    void Start()
    {
        AtualizarTexto();
    }

    void Update()
    {
        // Vitória
        if (!vitoriaAtivada && moedasatuais >= moedasParaVitoria)
        {
            vitoriaAtivada = true;
            if (Vitoria != null) Vitoria.SetActive(true);
            Time.timeScale = 0;
        }

        // Derrota
        if (vida <= 0)
        {
            vida = 0;
            AtivarGameOver();
        }

        if (vida > vidaMax)
            vida = vidaMax;
    }

    // GANHA MOEDAS
    public void AumentoDeMoedas(int v)
    {
        // Adiciona moedas permanentes da Loja (mantido do seu sistema)
        if (Loja.instance != null)
            Loja.instance.AdicionarStoreCoins(v);

        // Moedas da fase
        moedasatuais += v;

        if (vida < vidaMax)
            vida += v;

        AtualizarTexto();
    }

    // APLICA DANO (chamado por inimigos / eventos / PlayerController)
    public void AplicarDano()
    {
        moedasatuais -= VidaPerdida;
        if (moedasatuais < 0) moedasatuais = 0;

        vida -= VidaPerdida;
        if (vida < 0) vida = 0;

        AtualizarTexto();

        if (vida <= 0)
            AtivarGameOver();
    }

    private void AtualizarTexto()
    {
        if (Moedatxt != null)
            Moedatxt.text = "Moedas: " + moedasatuais + "/" + moedasParaVitoria +
                            " | Vida: " + vida + "/" + vidaMax;
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
