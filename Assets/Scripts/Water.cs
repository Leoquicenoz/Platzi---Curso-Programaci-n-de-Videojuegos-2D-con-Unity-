using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    float originalSpeed;
    Player player;
    [SerializeField] float speedReductionRatio = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();    //Se obtiene la referencia del script Player
        originalSpeed = player.speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.speed *= speedReductionRatio;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.speed = originalSpeed;
        }
    }
}
