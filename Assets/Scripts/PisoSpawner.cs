using UnityEngine;

public class PisoSpawner : MonoBehaviour
{
    public GameObject[] pisos;          // prefabs dos pisos
    public GameObject[] obstaculos;     // prefabs de obstáculos
    public float larguraPiso = 10f;
    private float proximaPosicaoX = 0f;
    public Transform cameraRef;
    [Range(0f, 1f)] public float chanceObstaculo = 0.3f;

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
        // Sorteia um piso
        int index = Random.Range(0, pisos.Length);
        GameObject novoPiso = Instantiate(pisos[index], new Vector3(proximaPosicaoX, 0, 0), Quaternion.identity);

        Destroy(novoPiso, 10f);

        // Chance de spawnar obstáculo
        if (Random.value < chanceObstaculo && obstaculos.Length > 0)
        {
            // Procura o ponto de spawn dentro do piso
            Transform pontoSpawn = novoPiso.transform.Find("PontoObstaculo");

            if (pontoSpawn != null)
            {
                int indexObs = Random.Range(0, obstaculos.Length);
                GameObject obstaculo = Instantiate(obstaculos[indexObs], pontoSpawn.position, Quaternion.identity, novoPiso.transform);
                Destroy(obstaculo, 10f);
            }
            else
            {
                Debug.LogWarning("PontoObstaculo não encontrado no prefab: " + novoPiso.name);
            }
        }

        proximaPosicaoX += larguraPiso;
    }
}
