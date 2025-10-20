using UnityEngine;
using TMPro;

public class MoedasCounter : MonoBehaviour
{
    public static MoedasCounter instance;
    private TMP_Text Moedatxt;   // agora é privado
    public int moedasatuais = 0;
    public GameObject Vitoria;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // procura o objeto pelo nome e pega o TMP_Text
        GameObject textoObj = GameObject.Find("TextoMoedas");
        if (textoObj != null)
            Moedatxt = textoObj.GetComponent<TMP_Text>();

        Vitoria = GameObject.Find("Vitoria");
        Vitoria.SetActive(false);

    }

    void Start()
    {
        AtualizarTexto();
    }

    void Update()
    {
        if (moedasatuais == 10)
        {
            Vitoria.gameObject.SetActive(true);
            Time.timeScale = 0;
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
            Moedatxt.text = "Moedas: " + moedasatuais.ToString() + "/10";
    }
}
