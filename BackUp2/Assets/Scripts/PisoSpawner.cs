using UnityEngine;

public class PisoSpawner : MonoBehaviour
{
    public GameObject[] pisos;   // Arraste Piso1, Piso2... aqui no Inspector
    public float larguraPiso = 10f;
    private float proximaPosicaoX = 0f;
    public Transform cameraRef;

    void Start()
    {

        for (int i = 0; i < 5; i++)
        {
            SpawnPiso();
        }
    }
    void Update()
    {
        if (cameraRef.position.x + (larguraPiso * 2) > proximaPosicaoX)
        {
            SpawnPiso();
        }
    }

    void SpawnPiso()
    {
        // Sorteia um prefab aleatório
        int index = Random.Range(0, pisos.Length);

        // Instancia na posição
        GameObject novo = Instantiate(pisos[index], new Vector3(proximaPosicaoX, 0, 0), Quaternion.identity);

        // Destroi depois de 1 segundo
        Destroy(novo, 10f);

        // Atualiza posição pro próximo spawn
        proximaPosicaoX += larguraPiso;
    }
}
