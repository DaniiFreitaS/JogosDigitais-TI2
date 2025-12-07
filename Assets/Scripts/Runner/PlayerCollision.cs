using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("VFX")]
    [SerializeField] private GameObject vfxMoedas;

    private PlayerMovement movimento;
    private PlayerPowerUps powerups;
    private AudioManager audioManager;

    private void Awake()
    {
        movimento = GetComponent<PlayerMovement>();
        powerups = GetComponent<PlayerPowerUps>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Power-ups
        if (other.CompareTag("PowerUpVelocidade"))
        {
            powerups.AtivarTurbo();
            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("PowerUpEscudo"))
        {
            powerups.AtivarEscudo();
            Destroy(other.gameObject);
            return;
        }

        // Dano / inimigo / cair no buraco
        if (other.CompareTag("Enemy"))
        {
            if (powerups.EscudoAtivo)
            {
                // escudo bloqueia dano
                Debug.Log("Dano bloqueado pelo Escudo!");
                return;
            }

            // VFX opcional
            if (vfxMoedas != null)
                Instantiate(vfxMoedas, transform.position + Vector3.up, Quaternion.identity);

            if (audioManager != null)
                audioManager.PlaySFX(audioManager.coinDrop);

            // aplica dano no sistema
            bool gameOver = MoedasCounter.instance.AplicarDano();

            if (gameOver)
            {
                Debug.Log("TEste Game over Chamado");
            }

            return;
        }

        if (other.CompareTag("LimiteInf"))
        {
            MoedasCounter.instance.AtivarGameOver();
        }

        // Coleta de moedas
        if (other.CompareTag("Coin"))
        {
            MoedasCounter.instance.AumentoDeMoedas(1);

            if (audioManager != null)
                audioManager.PlaySFX(audioManager.coin);

            Destroy(other.gameObject);
        }
    }
}
