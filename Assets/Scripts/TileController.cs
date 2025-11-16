using UnityEngine;

public class TileController : MonoBehaviour
{
    public GameObject[] tiles;
    private float posX = 0;
    private GameObject tileTemp;
    private TileTAG pisoTamanho;

    void Start()
    {
        SpawnTile();
    }

    void SpawnTile()
    {
        int index = Random.Range(0, tiles.Length);
        tileTemp = Instantiate(tiles[index], new Vector3(posX, 0, 0), Quaternion.identity);
        pisoTamanho = tiles[index].GetComponentInChildren<TileTAG>();
        posX += pisoTamanho.GetComponent<Renderer>().bounds.size.x;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("oiii");
        if (collision != null)
        {
            if (collision.gameObject.tag == "Tile")
            {

                SpawnTile();
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("oiaaaaaii");
        if (collision != null)
        {
            if (collision.gameObject.tag == "Tile")
            {
                //Debug.Log("tchau");
                Destroy(collision.gameObject, 2f);

            }
        }
    }
}
