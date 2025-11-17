using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Loja : MonoBehaviour
{
    public static Loja instance;

    [Header("Saldo")]
    [Tooltip("Valor inicial do saldo (usado apenas na primeira execução, se não houver save).")]
    public int StoreCoins = 0; // saldo permanente

    [Header("UI")]
    public TMP_Text textoStoreCoins; // opcional: arraste no inspector ou use tag "StoreCoinsText"

    private const string PLAYERPREFS_KEY = "StoreCoins";
    private int prevStoreCoins;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Se já existe um save, carrega-o.
        // Caso contrário, mantém o valor do inspector como valor inicial e salva.
        if (PlayerPrefs.HasKey(PLAYERPREFS_KEY))
        {
            StoreCoins = PlayerPrefs.GetInt(PLAYERPREFS_KEY);
        }
        else
        {
            PlayerPrefs.SetInt(PLAYERPREFS_KEY, StoreCoins);
            PlayerPrefs.Save();
        }

        prevStoreCoins = StoreCoins;

        // Atualiza quando cena carregar (procura o texto)
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        BuscarTexto();
        AtualizarHUD();
    }

    private void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        BuscarTexto();
        AtualizarHUD();
    }

    private void BuscarTexto()
    {
        if (textoStoreCoins != null) return;

        GameObject obj = GameObject.FindGameObjectWithTag("StoreCoinsText");
        if (obj != null)
            textoStoreCoins = obj.GetComponent<TMP_Text>();
    }

    // Adicionar positivo ou negativo (compra envia valor negativo)
    public void AdicionarStoreCoins(int quantidade)
    {
        StoreCoins += quantidade;

        if (StoreCoins < 0) StoreCoins = 0;

        Salvar();
        AtualizarHUD();
    }

    // Remover é apenas um wrapper para manter compatibilidade
    public void RemoverStoreCoins(int quantidade)
    {
        if (quantidade <= 0) return; // proteção básica
        AdicionarStoreCoins(-quantidade);
    }

    private void Salvar()
    {
        PlayerPrefs.SetInt(PLAYERPREFS_KEY, StoreCoins);
        PlayerPrefs.Save();
        prevStoreCoins = StoreCoins;
    }

    private void AtualizarHUD()
    {
        if (textoStoreCoins != null)
            textoStoreCoins.text = StoreCoins.ToString();
    }

    private void Update()
    {
        // tenta achar texto se ainda não tiver referência
        if (textoStoreCoins == null)
        {
            BuscarTexto();
            AtualizarHUD();
        }

        // detecta mudança no Inspector (ou qualquer mudança externa) e atualiza/salva
        if (StoreCoins != prevStoreCoins)
        {
            AtualizarHUD();
            Salvar();
        }
    }
}
