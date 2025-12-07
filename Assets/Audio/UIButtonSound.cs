using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    public void PlayClickSound()
    {
        if (AudioManager.instance != null)
            AudioManager.instance.PlayUIClick();
    }
}
 