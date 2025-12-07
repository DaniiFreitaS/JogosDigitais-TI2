using UnityEngine;

public class LuzController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int r = Random.Range(0, 100);//teste apenas com 2 posicoes, se ficar bom pode ser feito com mais
        Quaternion reset = transform.rotation;
        Debug.Log(r);
        if (r > 20)
        {
            reset.x = 220f;
        }
        
        transform.rotation = reset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
