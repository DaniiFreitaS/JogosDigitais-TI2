using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class MoedasCounter : MonoBehaviour
{
    public static MoedasCounter instance;

    [Header("UI")]
    private TMP_Text Moedatxt;
    //private GameObject gameOverScreen;
    private GameObject Vitoria;

    [Header("Valores do Jogo")]
    public int moedasatuais = 0;
    public int moedasParaVitoria = 100;

    public int vida = 30;
    public int vidaMax = 30;
    private int VidaPerdida;

    private bool vitoriaAtivada = false;

    void Awake()
    {
        Debug.Log("MoedasCounter está no objeto: " + gameObject.name, gameObject);
        // Singleton seguro
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Proteções contra valores inválidos do Inspector
        if (vidaMax <= 0) vidaMax = vida;
        if (vida <= 0) vida = vidaMax;
        if (moedasParaVitoria <= 0) moedasParaVitoria = 10;

        // Define o dano proporcional
        VidaPerdida = Mathf.Max(1, (int)math.round((float)moedasParaVitoria / 10f));

        // Localiza o texto das moedas (opcional)
        GameObject textoObj = GameObject.Find("TextoMoedas");
        if (textoObj != null)
            Moedatxt = textoObj.GetComponent<TMP_Text>();
        else
            Debug.LogWarning("⚠ TextoMoedas não encontrado na cena!");

        // Painel de vitória
        Vitoria = GameObject.FindGameObjectWithTag("Vitoria");
        if (Vitoria != null)
            Vitoria.SetActive(false);
        else
            Debug.LogWarning("⚠ Objeto com tag 'Vitoria' não encontrado!");

        // Game Over
       // gameOverScreen = GameObject.FindGameObjectWithTag("GameOverTAG");
        //if (gameOverScreen == null)
         //   Debug.LogWarning("⚠ GameOverScreen não encontrado na cena!");
    }

    void Start()
    {
        // Garante que o jogo não comece pausado
        Time.timeScale = 1;
        AtualizarTexto();
    }

    void Update()
    {
        ChecarVitoria();
        ChecarDerrota();
    }

    private void ChecarVitoria()
    {
        if (vitoriaAtivada) return;

        if (moedasatuais >= moedasParaVitoria)
        {
            vitoriaAtivada = true;

            if (Vitoria != null)
                Vitoria.SetActive(true);
            else
                Debug.LogWarning("⚠ Vitória ativada mas o painel está ausente!");

            Time.timeScale = 0;
        }
    }

    private void ChecarDerrota()
    {
        if (vida <= 0)
        {
            vida = 0;
            AtivarGameOver();
        }

        if (vida > vidaMax)
            vida = vidaMax;
    }

    public void AumentoDeMoedas(int v)
    {
        moedasatuais += Mathf.Max(0, v);

        if (vida < vidaMax)
            vida += v;

        AtualizarTexto();
    }

    public bool AplicarDano()
    {
        vida -= VidaPerdida;
        moedasatuais -= VidaPerdida;

        if (vida < 0) vida = 0;
        if (moedasatuais < 0) moedasatuais = 0;

        AtualizarTexto();

        if (vida <= 0)
        {
            Debug.Log("Teste vida zerado");
            AtivarGameOver();
            return true;
        }
        return false;
    }

    private void AtualizarTexto()
    {
        if (Moedatxt != null)
        {
            Moedatxt.text =
                $"Moedas: {moedasatuais}/{moedasParaVitoria} | Vidas: {vida}/{vidaMax}";
        }
    }

    public void AtivarGameOver()
    {
        Debug.Log("Gameover script chegou ao final");
        GameManager.instance.MostrarGameOver();
        /*if (gameOverScreen != null && gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Debug.LogWarning("⚠ Não foi possível ativar o Game Over! Objetos ausentes.");
        }
        */
    }
}
