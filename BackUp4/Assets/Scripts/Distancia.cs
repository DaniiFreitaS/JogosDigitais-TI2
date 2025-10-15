using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Distancia : MonoBehaviour
{
    public Slider DistanceSlider;
    public float gameTime;
    public GameObject Vitoria;
    private bool stopTimer;
    private float currentTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stopTimer = false;
        currentTime = gameTime; //É o que faz o timer reiniciar
        DistanceSlider.maxValue = gameTime;
        DistanceSlider.value = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopTimer) return;
        
        currentTime -= Time.deltaTime; //tira um segundo a cada update
        int minutes = Mathf.FloorToInt(currentTime / 60);//Calcula os minutos
        int seconds = Mathf.FloorToInt(currentTime - minutes * 60);//Calcula os segundos

        if (currentTime<= 0)
        {
            stopTimer = true; //Para o Timer
            currentTime = 0; //Previne de ficar negativo
            Vitoria.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        DistanceSlider.value = currentTime;
    }
}

//https://www.youtube.com/watch?v=S12x7txHS1c
//Ref Timer