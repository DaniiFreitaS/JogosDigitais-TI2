using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [Header("Modelos de Player")]
    public GameObject playerP1;
    public GameObject playerP2;
    public GameObject playerP3;

    public Transform tela;

    private const string EQUIPPED_KEY = "EquippedItem";

    void Start()
    {
        // Pega o item equipado salvo na loja
        string equipado = PlayerPrefs.GetString(EQUIPPED_KEY, "P1");

        GameObject prefabEscolhido = playerP1; // padrão

        // Decide qual prefab usar
        switch (equipado)
        {
            case "P1":
                prefabEscolhido = playerP1;
                break;

            case "P2":
                prefabEscolhido = playerP2;
                break;

            case "P3":
                prefabEscolhido = playerP3;
                break;
        }

        // Instancia o player correto
        GameObject player = Instantiate(prefabEscolhido, transform.position, Quaternion.identity);

        // Caso você precise colocar o player dentro da tela:
        // player.transform.SetParent(tela);
    }
}
