using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //Creamos una variable de tipo Gamemanager llamada Instance (no es obligatorio ese nombre, pero es lo que se utiliza normalmente)
    public int time = 30;
    public int difficulty = 1;
    [SerializeField] int score;
    public bool gameOver;

    public int Score
    {
        get => score; //Se crea una propiedad Score 
        set
        {
            score = value;  //Se le asigna a la variable el valor de la propiedad 
            UIManager.Instance.UpdateUIScore(score);    // Se envía el valor de score a la función UpdateUIScore
            
            if (score % 1000 == 0)
            {
                difficulty++;
            }
        
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;    //Inicializamos el Singleton
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownRoutine());    //Llamamos la rutina paralela
        UIManager.Instance.UpdateUIScore(score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CountDownRoutine()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1); //mientras el tiempo sea mayor a cero, se espera un segundo para disminuir la variable tiempo
            time--;
            UIManager.Instance.UpdateUITime(time);  // Se envía el valor de time a la función UpdateUITime
        }
        gameOver = true;
        UIManager.Instance.ShowGameOverScreen();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game"); //Se carga una scena desde el SceneManager para reiniciar el juego
        
    }
}
