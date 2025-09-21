using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float velocidadeMax = 25f;   // velocidade máxima
    public float aceleracao = 5f;       // quanto a velocidade aumenta por segundo
    public float alturaPulo = 15f;      // altura do pulo
    public float gravidade = 30f;       // gravidade aplicada

    private CharacterController cc;
    private Vector3 movimento;
    private float velocidadeAtual = 0f; // começa parado

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Aumenta a velocidade suavemente até a velocidade máxima
        if (velocidadeAtual < velocidadeMax)
        {
            velocidadeAtual += aceleracao * Time.deltaTime;
            velocidadeAtual = Mathf.Min(velocidadeAtual, velocidadeMax); // limita
        }

        // Movimento horizontal
        movimento.x = velocidadeAtual;

        // Movimento vertical (pulo / gravidade)
        if (cc.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Pulo
            {
                movimento.y = Mathf.Sqrt(2 * gravidade * alturaPulo);
            }
            else
            {
                movimento.y = -1f; // mantém contato
            }
        }
        else
        {
            movimento.y -= gravidade * Time.deltaTime;
        }

        // Move o player
        cc.Move(movimento * Time.deltaTime);
    }
}
