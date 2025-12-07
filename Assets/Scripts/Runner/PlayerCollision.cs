using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("VFX")]
    [SerializeField] private GameObject vfxMoedas;

    private PlayerMovement movimento;
    private PlayerPowerUps powerups;

    private void Awake()
    {
        movimento = GetComponent<PlayerMovement>();
        powerups = GetComponent<PlayerPowerUps>();
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
        if (other.CompareTag("Enemy") || other.CompareTag("LimiteInf"))
        {
            Debug.Log(other.tag);
            if (powerups.EscudoAtivo)
            {
                // escudo bloqueia dano
                if (other.CompareTag("LimiteInf"))
                    //movimento.ResetPos();

                Debug.Log("Dano bloqueado pelo Escudo!");
                return;
            }

            // VFX opcional
            if (vfxMoedas != null)
                Instantiate(vfxMoedas, transform.position + Vector3.up, Quaternion.identity);

            // aplica dano no sistema
            bool gameOver = MoedasCounter.instance.AplicarDano();

            if (!gameOver)
                //movimento.ResetPos();

            return;
        }

        // Coleta de moedas
        if (other.CompareTag("Coin"))
        {
            MoedasCounter.instance.AumentoDeMoedas(1);
            Destroy(other.gameObject);
        }
    }
    
}

