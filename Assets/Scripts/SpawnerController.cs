using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject playerprefab;
    public Transform tela;
    void Start()
    {
        playerprefab = Instantiate(playerprefab, transform.position, Quaternion.identity);

        playerprefab.transform.SetParent(tela);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
