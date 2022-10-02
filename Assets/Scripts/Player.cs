using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float horizontalAxis, verticalAxis;
    Vector3 moveDirection;

    [SerializeField] float speed = 3;   //Serializefield sirve para poner una variable editable desde unity
    [SerializeField] Transform aim;
    [SerializeField] Camera camera;
    Vector2 facingDirection;
    [SerializeField] Transform bulletPrefab;
    bool gunLoaded = true;
    [SerializeField] float fireRate = 1;
    [SerializeField] int health = 10;
    bool powerShotEnable;
    bool invulnerable;
    [SerializeField] float invulnerableTime = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");    //Extrae la componente del imput horizontal del teclado, teclas a y d o flechas a los lados
        verticalAxis = Input.GetAxis("Vertical");    //Extrae la componente del imput vertical del teclado, teclas w y s o flechas arriba y abajo
        moveDirection.x = horizontalAxis;
        moveDirection.y = verticalAxis;

        transform.position += moveDirection * Time.deltaTime * speed;   //Sumar la posicion que se obtenga del teclado a la posicion del player

        // Movimiento de la mira 

        facingDirection = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;  //la posicion del mouse en la pantalla,
                                                                                                //traducirla a punto en el mundo y asignarselo a la posicion de la
                                                                                                //mira y se le resta la posicion del player para obtener un vector
        aim.position = transform.position + (Vector3)facingDirection.normalized;    //Se interpreta un vector 2 como vector 3


        if (Input.GetMouseButton(0) && gunLoaded)
        {
            gunLoaded = false;
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;    //creamos una variable en la que se guardara el angulo de la mira
                                                                                                //y que la bala salga dirigida en ese sentido (se pasa a grados con Mathf.Rad2Deg
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Transform bulletClone = Instantiate(bulletPrefab, transform.position, targetRotation); //Crea la bala a partir del prefab que tenemos en los assets

            if (powerShotEnable)
            {
                bulletClone.GetComponent<Bullet>().powerShot = true;
            }

            StartCoroutine(ReloadGun());
        }

    }

    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1 / fireRate);
        gunLoaded = true;
    }

    public void TakeDamage()
    {
        if (invulnerable)
            return;

        health--;
        invulnerable = true;
        StartCoroutine(MakeVulnerableAgain());
        if (health <= 0)
        {
            // Game Over
        }
    }

    IEnumerator MakeVulnerableAgain()
    {
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable=false;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            switch (collision.GetComponent<PowerUp>().powerUpType)
            {
                case PowerUp.PowerUpType.FireRateIncrease:
                    fireRate++;
                    break;

                case PowerUp.PowerUpType.PowerShot:
                    powerShotEnable = true;
                    break;
            }
            Destroy(collision.gameObject, 0.1f);
        }

    }
}
