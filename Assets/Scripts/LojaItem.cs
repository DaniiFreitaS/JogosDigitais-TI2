using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LojaItem : MonoBehaviour
{
    [Header("Configurações do Item")]
    public string itemID = "Item1";
    public int preco = 100;
    public bool desbloqueado = false;     
    public bool equipado = false;        
    public string txtComprado = "OK";     
    public string txtEquipado = "USANDO"; 

    [Header("Referências UI")]
    public TMP_Text precoTxt;
    public GameObject bloqueioIcon;
    public Button botaoComprar;

    private const string EQUIPPED_KEY = "EquippedItem";

private void Start()
{
    // --- CARREGAR DESBLOQUEIO ---
    if (desbloqueado)
    {
        PlayerPrefs.SetInt(itemID, 1);
    }
    else
    {
        desbloqueado = PlayerPrefs.GetInt(itemID, 0) == 1;
    }

    // ----------------------------
    // SISTEMA DE EQUIPAR PRIORITÁRIO
    // ----------------------------

    string equipadoSalvo = PlayerPrefs.GetString(EQUIPPED_KEY, "");

    if (string.IsNullOrEmpty(equipadoSalvo))
    {
        // NINGUÉM FOI SALVO AINDA → USA O DO INSPECTOR

        if (equipado)
        {
            // Este foi marcado no inspetor → vira o equipado inicial
            PlayerPrefs.SetString(EQUIPPED_KEY, itemID);
            PlayerPrefs.Save();
        }
        else
        {
            // Checa se algum outro item está equipado no inspetor
            LojaItem[] todos = FindObjectsOfType<LojaItem>();
            bool algumEquipado = false;

            foreach (LojaItem item in todos)
            {
                if (item.equipado)
                {
                    PlayerPrefs.SetString(EQUIPPED_KEY, item.itemID);
                    PlayerPrefs.Save();
                    algumEquipado = true;
                    break;
                }
            }

            // Se ninguém no inspetor estava equipado, nenhum é equipado
            if (!algumEquipado)
            {
                equipado = false;
            }
        }
    }
    else
    {
        // JÁ EXISTE ALGO SALVO → IGNORA O INSPECTOR
        equipado = (equipadoSalvo == itemID);
    }

    AtualizarUI();

    botaoComprar.onClick.AddListener(AoClicarBotao);
}


    private void AtualizarUI()
    {
        if (bloqueioIcon != null)
            bloqueioIcon.SetActive(!desbloqueado);

        if (precoTxt == null) return;

        if (!desbloqueado)
        {
            precoTxt.text = preco.ToString();
        }
        else
        {
            precoTxt.text = equipado ? txtEquipado : txtComprado;
        }
    }

    private void AoClicarBotao()
    {
        if (!desbloqueado)
        {
            TentarComprar();
        }
        else
        {
            Equipar();
        }
    }

    private void TentarComprar()
    {
        if (Loja.instance == null)
        {
            Debug.LogError("Loja.instance não encontrada!");
            return;
        }

        if (Loja.instance.StoreCoins >= preco)
        {
            Loja.instance.AdicionarStoreCoins(-preco);
            desbloqueado = true;

            PlayerPrefs.SetInt(itemID, 1);
            PlayerPrefs.Save();

            AtualizarUI();
        }
        else
        {
            Debug.Log("Moedas insuficientes!");
        }
    }

    // ---------------- SISTEMA DE EQUIPAR ----------------
    private void Equipar()
    {
        // Salva qual item será o novo equipado
        PlayerPrefs.SetString(EQUIPPED_KEY, itemID);
        PlayerPrefs.Save();

        // Pega todos os itens da loja
        LojaItem[] todosItens = FindObjectsOfType<LojaItem>();

        // Primeiro: limpa o estado de todos
        foreach (LojaItem item in todosItens)
        {
            item.equipado = false;
            item.AtualizarUI();
        }

        // Agora sim aplica o equipado verdadeiro
        equipado = true;
        AtualizarUI();
    }
}
