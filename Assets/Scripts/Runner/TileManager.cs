using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] pisos;
    public Transform spawnPoint;  // ponto onde o próximo tile nasce
    public float tileLength = 1000f;
    private Vector3 pos = new (1450, 0, 0);

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("teste");
        if (other.CompareTag("Player"))
        {
            SpawnPiso();
        }
    }

    void SpawnPiso()
    {
        int index = Random.Range(0, pisos.Length);
        //Vector3 pos = new Vector3(1450, 0, 0);
        GameObject novoPiso = Instantiate(
            pisos[index],
            pos,
            Quaternion.identity);

        Destroy(novoPiso, 400f);

        pos.x += 1000;
        // avança o ponto de spawn para o próximo tile
        spawnPoint.position += new Vector3(1000, 0, 0);
    }
}
