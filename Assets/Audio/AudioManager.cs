using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("--------Audio Source----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--------Audio Clip----------")]
    public AudioClip background;
    public AudioClip menu;
    public AudioClip jump;
    public AudioClip coin;
    public AudioClip coinDrop;
    public AudioClip button;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

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
    // adiciona no AudioManager (única função nova)
    public void PlaySFX(AudioClip clip)
    {
        if (SFXSource == null || clip == null) return;
        SFXSource.PlayOneShot(clip);
    }

    public void PlayUIClick()
    {
        PlaySFX(button);
    }

}
