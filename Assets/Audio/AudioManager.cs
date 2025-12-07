using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("--------Audio Source----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--------Audio Clip----------")]
    public AudioClip background;
    public AudioClip menu;
    public AudioClip jump;
    public AudioClip coin;

    private void Awake()
    {
        // Evita duplicar
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Escuta as mudanças de cena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        if (cena.name == "MainMenu")
        {
            musicSource.clip = menu;
        }
        else if (cena.name == "RunnerTeste")
        {
            musicSource.clip = background;
        }

        musicSource.Play();
    }
    
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
