using UnityEngine;

// Coloque este script em um GameObject da cena Runner.
// Ele observa MoedasCounter.instance.moedasatuais e envia apenas a diferença (delta)
// para Loja.instance, sem alterar o MoedasCounter.
// Também garante que o saldo da loja não seja reduzido abaixo do valor que tinha
// ao entrar na fase (storeBaseline).
public class StoreSync : MonoBehaviour
{
    int prevMoedasDaFase = 0;
    int storeBaseline = 0;
    bool initialized = false;

    // aguarda até que MoedasCounter e Loja existam
    System.Collections.IEnumerator Start()
    {
        // espera até que MoedasCounter.instance exista (ou times out)
        float timeout = 3f;
        float t = 0f;
        while (MoedasCounter.instance == null && t < timeout)
        {
            t += Time.deltaTime;
            yield return null;
        }

        // espera até que Loja.instance exista (ou times out)
        t = 0f;
        while (Loja.instance == null && t < timeout)
        {
            t += Time.deltaTime;
            yield return null;
        }

        // Se MoedasCounter não existir, nada a fazer
        if (MoedasCounter.instance == null)
        {
            Debug.LogWarning("StoreSync: MoedasCounter.instance não encontrado.");
            yield break;
        }

        // Se Loja não existir, warn e sair (Loja é necessária)
        if (Loja.instance == null)
        {
            Debug.LogWarning("StoreSync: Loja.instance não encontrado.");
            yield break;
        }

        // inicializa valores
        prevMoedasDaFase = MoedasCounter.instance.moedasatuais;
        storeBaseline = Loja.instance.StoreCoins; // valor mínimo permitido para a loja durante esta fase

        initialized = true;
        yield break;
    }

    void Update()
    {
        if (!initialized) return;

        int curr = MoedasCounter.instance.moedasatuais;
        int delta = curr - prevMoedasDaFase;

        if (delta > 0)
        {
            // ganhou moedas na fase -> adiciona à loja
            Loja.instance.AdicionarStoreCoins(delta);
        }
        else if (delta < 0)
        {
            int toRemove = -delta;

            // garantir que a loja não caia abaixo do baseline
            int allowedRemoval = Mathf.Max(0, Loja.instance.StoreCoins - storeBaseline);

            // só remova até o máximo permitido
            int removeNow = Mathf.Min(toRemove, allowedRemoval);

            if (removeNow > 0)
                Loja.instance.RemoverStoreCoins(removeNow);
            // se removeNow == 0, a loja não será reduzida abaixo do baseline
        }

        prevMoedasDaFase = curr;
    }
}
