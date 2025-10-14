using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int velocidade;
    void Start()
    {
        if ( velocidade == 0)
        {
            velocidade = 25;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * velocidade * Time.deltaTime);
    }
}