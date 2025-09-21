using UnityEngine;
using TMPro;

public class MoedasCounter : MonoBehaviour
{
    public static MoedasCounter instance;
    public TMP_Text Moedatxt;
    public int moedasatuais = 0;
    public GameObject Vitoria;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Moedatxt.text = "Moedas: " + moedasatuais.ToString() + "/10";
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
        Moedatxt.text = "Moedas: " + moedasatuais.ToString() + "/10";
    }
}
