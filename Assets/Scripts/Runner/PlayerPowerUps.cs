using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    public float duracaoTurbo = 5f;
    public float duracaoEscudo = 7f;

    private PlayerMovement movimento;
    private float velocidadeOriginal;
    private float aceleracaoOriginal;

    private bool turboAtivo = false;
    private float tempoTurboRestante = 0f;

    private bool escudoAtivo = false;
    private float tempoEscudoRestante = 0f;

    public bool EscudoAtivo => escudoAtivo;

    private void Awake()
    {
        movimento = GetComponent<PlayerMovement>();
        velocidadeOriginal = movimento.velocidadeMax;
        aceleracaoOriginal = movimento.aceleracao;
    }

    private void Update()
    {
        GerenciarPowerUps();
    }

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
            movimento.velocidadeMax *= 1.5f;
            movimento.aceleracao *= 2f;
        }

        turboAtivo = true;
        tempoTurboRestante = duracaoTurbo;
    }

    private void DesativarTurbo()
    {
        movimento.velocidadeMax = velocidadeOriginal;
        movimento.aceleracao = aceleracaoOriginal;
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
}
