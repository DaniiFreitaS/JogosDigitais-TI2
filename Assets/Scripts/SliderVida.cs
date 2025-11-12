using UnityEngine;
using UnityEngine.UI;

public class SliderVida : MonoBehaviour
{
    [Header("Referências")]
    public Slider slider; // Escolha o Slider no Inspector
    public MoedasCounter moedasCounter; // Pode ser arrastado manualmente ou detectado em runtime

    void Start()
    {
        // Verificações básicas
        if (slider == null)
        {
            Debug.LogError("⚠️ Nenhum Slider atribuído no SliderVida!");
            enabled = false;
            return;
        }

        // Inicia a checagem para encontrar o MoedasCounter quando ele surgir na cena
        StartCoroutine(EsperarMoedasCounter());
    }

    System.Collections.IEnumerator EsperarMoedasCounter()
    {
        // Espera até o MoedasCounter (do prefab) ser instanciado na cena
        while (moedasCounter == null)
        {
            moedasCounter = MoedasCounter.instance; // 👈 acessa a instância estática (já existe no seu script)
            yield return null; // espera um frame
        }

        // Quando encontrar, define o máximo e o valor inicial
        slider.maxValue = moedasCounter.vidaMax;
        slider.value = moedasCounter.vida;
    }

    void Update()
    {
        if (moedasCounter != null && slider != null)
        {
            slider.value = moedasCounter.vida;
        }
    }
}
