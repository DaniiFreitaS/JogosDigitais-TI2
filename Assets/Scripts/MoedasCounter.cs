using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class MoedasCounter : MonoBehaviour
{
    public static MoedasCounter instance;
    private TMP_Text Moedatxt;

    public int moedasatuais = 0;

    public int vida = 30, vidaMax, VidaPerdida;
    public GameObject Vitoria;
    public int moedasParaVitoria = 100; // Condição de vitória
    private bool vitoriaAtivada = false;

    private GameOverScreen gameOverScreen; // 👈 referência para o script GameOverScreen

    void Awake()
    {
        vidaMax = moedasParaVitoria;
        VidaPerdida = (int)math.round(moedasParaVitoria / 10);

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        GameObject textoObj = GameObject.Find("TextoMoedas");
        if (textoObj != null)
            Moedatxt = textoObj.GetComponent<TMP_Text>();

        Vitoria = GameObject.FindGameObjectWithTag("Vitoria");
        if (Vitoria != null)
            Vitoria.SetActive(false);

        // 👇 tenta achar o GameOverScreen na cena
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

            if (Vitoria != null)
                Vitoria.SetActive(true);

            Time.timeScale = 0;
        }

        // 👇 Condição de derrota
        if (vida <= 0)
        {
            vida = 0;
            AtivarGameOver();
        }
    }

    public void AumentoDeMoedas(int v)
    {
        moedasatuais += v;
        if (vida < vidaMax)
            vida += v;

        AtualizarTexto();
    }

    private void AtualizarTexto()
    {
        if (Moedatxt != null)
            Moedatxt.text = moedasatuais + "/" + moedasParaVitoria;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            vida -= VidaPerdida;
            moedasatuais -= VidaPerdida;

            if (vida < 0) vida = 0;
            if (moedasatuais < 0) moedasatuais = 0;

            AtualizarTexto();

            if (vida <= 0)
                AtivarGameOver();
        }
    }

    // 👇 Função para ativar o painel de Game Over
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
