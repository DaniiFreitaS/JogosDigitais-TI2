using UnityEngine;
using UnityEngine.EventSystems;

public class SliderSound : MonoBehaviour, IPointerUpHandler
{
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance; // Certo porque você já tem o Singleton
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (audioManager != null)
            audioManager.PlaySFX(audioManager.slider); 
    }
}

