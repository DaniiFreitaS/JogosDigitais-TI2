using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // --- Configurações de Movimento ---
    public float velocidadeMax = 14f;
    public float aceleracao = 1f;
    public float alturaPulo = 6f;
    public float gravidade = 140f;

    private CharacterController cc;
    private Vector3 movimento;
    private float velocidadeAtual = 0f;

    // mobile
    private bool tapCima = false;
    private bool tapBaixo = false;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        DetectarInputMobile();
        Mover();
    }

    void DetectarInputMobile()
    {
        tapCima = false;
        tapBaixo = false;

        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);

            if (toque.phase == TouchPhase.Began)
            {
                if (toque.position.y > Screen.height / 2)
                    tapCima = true;
                else
                    tapBaixo = true;
            }
        }
    }

    public void Mover()
    {
        if (velocidadeAtual < velocidadeMax)
        {
            velocidadeAtual += aceleracao * Time.deltaTime;
            velocidadeAtual = Mathf.Min(velocidadeAtual, velocidadeMax);
        }

        movimento.x = velocidadeAtual;

        if (cc.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space) || tapCima)
            {
                movimento.y = Mathf.Sqrt(2 * gravidade * alturaPulo);
            }
            else
            {
                movimento.y = -1f;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || tapBaixo)
            {
                movimento.y -= gravidade * 5f * Time.deltaTime;
            }
            else
            {
                movimento.y -= gravidade * Time.deltaTime;
            }
        }

        cc.Move(movimento * Time.deltaTime);
    }

    public void ResetPos()
    {
        cc.enabled = false;
        Vector3 reset = transform.position;
        reset.y = 20f;
        transform.position = reset;
        cc.enabled = true;
    }
}
