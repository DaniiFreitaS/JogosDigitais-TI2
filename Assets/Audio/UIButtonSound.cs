using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    public void PlayClickSound()
    {
        AudioManager audio = FindObjectOfType<AudioManager>();
        if (audio != null)
            audio.PlaySFX(audio.button);
    }
}
