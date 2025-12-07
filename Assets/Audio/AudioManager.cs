using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // necessário para coroutines

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
    public AudioClip slider;

    // NOVO → Som ambiente de pássaros
    [Header("--------Ambient SFX----------")]
    public AudioClip birdChirp;

    private Coroutine birdsRoutine; // guarda a rotina p/ parar quando trocar de cena


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
        // troca música conforme a cena
        if (cena.name == "MainMenu")
        {
            musicSource.clip = menu;
        }
        else if (cena.name == "RunnerTeste")
        {
            musicSource.clip = background;
        }

        musicSource.Play();

        // controla o SFX ambiente
        HandleAmbientSFX(cena.name);
    }


    // NOVO → inicia ou para pássaros conforme a cena
    private void HandleAmbientSFX(string sceneName)
    {
        // para rotina anterior (caso estivesse tocando)
        if (birdsRoutine != null)
        {
            StopCoroutine(birdsRoutine);
            birdsRoutine = null;
        }

        // inicia apenas no Runner
        if (sceneName == "RunnerTeste" && birdChirp != null)
        {
            birdsRoutine = StartCoroutine(PlayBirdsLoop());
        }
    }


    // NOVO → rotina que toca som de pássaros aleatoriamente
    private IEnumerator PlayBirdsLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(8f, 15f));
            PlaySFX(birdChirp);
        }
    }


    // já existia
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
