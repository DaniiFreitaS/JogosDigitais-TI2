using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // só funciona se o Player tiver a tag "Player"
        {
            //PainelG.SetActive(true);
        //    Time.timeScale = 0;
        }
    }
}
