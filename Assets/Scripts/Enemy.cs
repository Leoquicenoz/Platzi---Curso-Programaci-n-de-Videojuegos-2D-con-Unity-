using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 1;
    Transform player;
    [SerializeField] float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().transform;  //Busca al Player y obtiene su transform, luego se almacena en la variable
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = player.position - transform.position;   //Se obtiene la dirección hacia donde se debe mover el enemigo para encontrarse con el player
        transform.position += (Vector3)direction * Time.deltaTime * speed;
    }

    public void TakeDamage()    //Metodo para reducir la vida del enemigo
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);    //Si la salud llega a 0, destruir el enemigo
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage();  //Si collisiona con player, llamar el metodo takedamage del script player
        }
    }
}
