using UnityEngine;

public class ObstaculosMoveisAvancado : MonoBehaviour
{
    [Header("Configurações de Movimento (0 para desativar eixo)")]
    public float velocidade = 2f;

    public float amplitudeX = 2f;   // pode ser negativo
    public float amplitudeY = 0f;   // pode ser negativo

    private Vector3 posInicial;

    void Start()
    {
        posInicial = transform.position;
    }

    void Update()
    {
        float tempo = Time.time * velocidade;

        float movX = 0f;
        float movY = 0f;

        bool usaX = amplitudeX != 0;
        bool usaY = amplitudeY != 0;

        // Somente X
        if (usaX && !usaY)
        {
            // Valor negativo = sentido oposto automaticamente
            movX = Mathf.Sin(tempo) * amplitudeX;
        }
        // Somente Y
        else if (!usaX && usaY)
        {
            movY = Mathf.Sin(tempo) * amplitudeY;
        }
        // Movimento circular / elíptico
        else if (usaX && usaY)
        {
            // Se amplitude for negativa, automaticamente inverte o sentido
            movX = Mathf.Cos(tempo) * amplitudeX;
            movY = Mathf.Sin(tempo) * amplitudeY;
        }

        transform.position = new Vector3(
            posInicial.x + movX,
            posInicial.y + movY,
            transform.position.z
        );
    }
}