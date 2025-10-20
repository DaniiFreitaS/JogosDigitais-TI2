using UnityEngine;

public class Moedas : MonoBehaviour
{
    public int value;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MoedasCounter.instance.AumentoDeMoedas(value);
            Destroy(gameObject);
        }
    }
}

//https://www.youtube.com/watch?v=GG0NYcOQd0k