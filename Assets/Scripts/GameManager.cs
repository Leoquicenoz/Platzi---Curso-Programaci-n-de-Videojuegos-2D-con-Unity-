using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //Creamos una variable de tipo Gamemanager llamada Instance (no es obligatorio ese nombre, pero es lo que se utiliza normalmente)
    public int time = 30;
    public int difficulty = 1;

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
        }
    }
}
