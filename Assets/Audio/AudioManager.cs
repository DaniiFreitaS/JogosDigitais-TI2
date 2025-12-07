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
    public AudioClip coinDrop;

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
        else if (cena.name == "Runner")
        {
            musicSource.clip = background;
        }

        musicSource.Play();
    }

        // adiciona no AudioManager (única função nova)
        public void PlaySFX(AudioClip clip)
        {
            if (SFXSource == null || clip == null) return;
            SFXSource.PlayOneShot(clip);
        }

}
