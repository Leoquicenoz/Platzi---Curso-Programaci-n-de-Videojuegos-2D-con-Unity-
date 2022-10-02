using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;    //Creo una variable de tipo Gameobject
    [Range(1,10)][SerializeField] float spawnRate = 1;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNewEnemy());    //Se inicializa la Corutina, se hace en Start, para que inicie una vez inicie el juego

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnNewEnemy()
    {
        while (true)    //Se crea una Corutina la cual va a crear un enemigo nuevo cada cierto tiempo
                        //Se utiliza un while ya que queremos que se generen enemigos durante todo el tiempo que dure el juego
        {
            yield return new WaitForSeconds(1/spawnRate);
            float random = Random.Range(0.0f, 1.0f);    //se tiene un numero random entre 0 y 1

            if (random > GameManager.Instance.difficulty*0.1f)  //se creara un enemigo diferente, dependiendo del valor de random y de la dificultad
            {
                Instantiate(enemyPrefab[0]);
            }
            else
            {
                Instantiate(enemyPrefab[1]);
            }
            
        }
    }
}
