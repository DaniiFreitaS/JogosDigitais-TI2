using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float velocidade = 25f;       // corrida constante
    public float alturaPulo = 15f;       // altura do pulo
    public float gravidade = 30f;        // gravidade aplicada

    private CharacterController cc;
    private Vector3 movimento;           // vetor de movimento

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movimento do player correndo separado do movimento da camera para conseguir ter colisoes
        movimento.x = velocidade;

        //Verificar se o player esta no chao ou no meio do pulo
        if (cc.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))//Pulo do player
            {
                // Calcula a velocidade inicial do pulo
                movimento.y = Mathf.Sqrt(2 * gravidade * alturaPulo);
            }
            else
            {
                movimento.y = -1f; // força mínima para manter contato com chão
            }
        }
        else
        {
            movimento.y -= gravidade * Time.deltaTime; // aplica gravidade quando o player esta pulando
        }

        // Move o player
        cc.Move(movimento * Time.deltaTime);
    }
}
