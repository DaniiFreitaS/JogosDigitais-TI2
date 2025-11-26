using UnityEngine;
// Não precisa mais de TMPro e Unity.Mathematics, a não ser que sejam usados para power-ups.

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
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

    // --- Configurações de Power-Ups ---
    public float duracaoTurbo = 5f;
    public float duracaoEscudo = 7f;

    private bool turboAtivo = false;
    private float tempoTurboRestante = 0f;
    private float velocidadeMaximaOriginal;

    private bool escudoAtivo = false;
    private float tempoEscudoRestante = 0f;


    void Awake()
    {
        Debug.Log("oiiiiiiiiiiiiiiiiiiiiiiii");
        cc = GetComponent<CharacterController>();
        velocidadeMaximaOriginal = velocidadeMax;
    }

    void Update()
    {
        DetectarInputMobile();
        GerenciarPowerUps();
        Mover();
        // NOTA: A lógica de vitória/derrota está no MoedasCounter.Update()
    }

    // ... [Funções GerenciarPowerUps, AtivarTurbo, DesativarTurbo, AtivarEscudo, DesativarEscudo,
    // DetectarInputMobile e Mover - permanecem as mesmas] ...

    // --- Funções Auxiliares (mantidas para funcionalidade completa) ---

    void GerenciarPowerUps()
    {
        if (turboAtivo)
        {
            tempoTurboRestante -= Time.deltaTime;
            if (tempoTurboRestante <= 0) DesativarTurbo();
        }
        if (escudoAtivo)
        {
            tempoEscudoRestante -= Time.deltaTime;
            if (tempoEscudoRestante <= 0) DesativarEscudo();
        }
    }

    public void AtivarTurbo()
    {
        if (!turboAtivo)
        {
            velocidadeMax *= 1.5f;
            aceleracao *= 2f;
        }
        turboAtivo = true;
        tempoTurboRestante = duracaoTurbo;
    }

    private void DesativarTurbo()
    {
        velocidadeMax = velocidadeMaximaOriginal;
        aceleracao = 1f;
        turboAtivo = false;
    }

    public void AtivarEscudo()
    {
        escudoAtivo = true;
        tempoEscudoRestante = duracaoEscudo;
    }

    private void DesativarEscudo()
    {
        escudoAtivo = false;
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

    void Mover()
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

    // ResetPos permanece aqui, pois é uma ação do PlayerController
    public void ResetPos()
    {
        cc.enabled = false;
        Vector3 reset = transform.position;
        reset.y = 20f;
        transform.position = reset;
        cc.enabled = true;
    }

    // ---------------------------------
    // Detecção de Colisão (Trigger) - DELEGA RESPONSABILIDADE
    // ---------------------------------

    private void OnTriggerEnter(Collider other)
    {
        // Lógica de Power-Ups
        if (other.CompareTag("PowerUpVelocidade") || other.CompareTag("PowerUpEscudo"))
        {
            if (other.CompareTag("PowerUpVelocidade")) AtivarTurbo();
            if (other.CompareTag("PowerUpEscudo")) AtivarEscudo();
            Destroy(other.gameObject);
            return;
        }

        // Lógica de Dano (Enemy ou LimiteInferior)
        if (other.CompareTag("Enemy") || other.CompareTag("LimiteInf"))
        {
            // O escudo só precisa ser verificado aqui, pois impede a chamada ao AplicarDano
            if (escudoAtivo)
            {
                if (other.CompareTag("LimiteInf"))
                {
                    ResetPos();
                }
                Debug.Log("Dano bloqueado pelo Escudo!");
                return;
            }

            // 1. Chama o MoedasCounter para aplicar o dano e verificar Game Over
            bool isGameOver = MoedasCounter.instance.AplicarDano();

            // 2. Se não for Game Over, reseta a posição (parte da lógica do PlayerController)
            if (!isGameOver)
            {
                ResetPos();
            }
        }

        // Lógica de Coleta (Moedas)
        else if (other.CompareTag("Coin"))
        {
            MoedasCounter.instance.AumentoDeMoedas(1);
            Destroy(other.gameObject);
        }
    }
}