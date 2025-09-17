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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stopTimer = false;
        DistanceSlider.maxValue = gameTime;
        DistanceSlider.value = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        float time = gameTime - Time.time; //tira um segundo a cada update
        int minutes = Mathf.FloorToInt(time / 60);//Calcula os minutos
        int seconds = Mathf.FloorToInt(time - minutes * 60);//Calcula os segundos

        if (time <= 0)
        {
            stopTimer = true; //Para o Timer
            Vitoria.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        if (stopTimer == false)
        {
            DistanceSlider.value = time;//Passa o valor do tempo pro timer
        }
    }
}

//https://www.youtube.com/watch?v=S12x7txHS1c
//Ref Timer