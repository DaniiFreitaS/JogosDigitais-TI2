using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    public float offsetX = 5f;
    public float suavidade = 5f;

    void Update()
    {
        // Se ainda não encontrou o player, procura pela tag
        if (player == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null)
                player = obj.transform;
            return; // evita erro antes de achar
        }

        // Posição da câmera
        Vector3 pos = transform.position;

        // Alvo no eixo X
        float alvoX = player.position.x + offsetX;

        // Suavização
        pos.x = Mathf.Lerp(pos.x, alvoX, Time.deltaTime * suavidade);

        // Aplica
        transform.position = pos;
    }
}
