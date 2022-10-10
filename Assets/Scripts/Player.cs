using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float horizontalAxis, verticalAxis;
    Vector3 moveDirection;
    Vector2 facingDirection;
    bool gunLoaded = true;
    public float speed = 3;
    bool powerShotEnable;
    bool invulnerable;
    CameraController camController;
    //Serializefield sirve para poner una variable editable desde unity
    [SerializeField] Transform aim;
    [SerializeField] Camera camera;
    [SerializeField] Transform bulletPrefab;
    [SerializeField] float fireRate = 1;
    [SerializeField] int health = 10;
    [SerializeField] float invulnerableTime = 3;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float blinkRate = 0.01f;
    


    public int Health 
    {
        get => health;
        set
        {
            health = value;
            UIManager.Instance.UpdateUIHealth(health);
        }
    
    }


    // Start is called before the first frame update
    void Start()
    {
        camController = FindObjectOfType<CameraController>();
        UIManager.Instance.UpdateUIHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        ReadImput();

        //Movimiento del personaje
        transform.position += moveDirection * Time.deltaTime * speed;   //Sumar la posicion que se obtenga del teclado a la posicion del player

        // Movimiento de la mira 

        facingDirection = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;  //la posicion del mouse en la pantalla,
                                                                                                //traducirla a punto en el mundo y asignarselo a la posicion de la
                                                                                                //mira y se le resta la posicion del player para obtener un vector
        aim.position = transform.position + (Vector3)facingDirection.normalized;    //Se interpreta un vector 2 como vector 3


        if (Input.GetMouseButton(0) && gunLoaded)
        {
            Shoot();
        }

       UpdatePlayerGraphics();

    }

    void ReadImput()
    {
        horizontalAxis = Input.GetAxis("Horizontal");    //Extrae la componente del imput horizontal del teclado, teclas a y d o flechas a los lados
        verticalAxis = Input.GetAxis("Vertical");    //Extrae la componente del imput vertical del teclado, teclas w y s o flechas arriba y abajo
        moveDirection.x = horizontalAxis;
        moveDirection.y = verticalAxis;
    }
    
    void Shoot()
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

    void UpdatePlayerGraphics()
    {
        anim.SetFloat("Speed", moveDirection.magnitude);    //Se establece la magnitud del vector movedirection en el float de anim
        if (aim.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;    //Si la posicion de la mira es mayor que la posicion del personaje, quiere decir que se está
                                            //mirando a la derecha y hay que hacer que el personaje voltee en ese sentido
        }
        else if (aim.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
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

        Health--;
        invulnerable = true;
        fireRate = 1;
        powerShotEnable = false;
        camController.Shake();
        StartCoroutine(MakeVulnerableAgain());
        if (Health <= 0)
        {
            GameManager.Instance.gameOver = true;
            UIManager.Instance.ShowGameOverScreen();
        }
    }

    IEnumerator MakeVulnerableAgain()
    {
        StartCoroutine(BlinkRoutine());
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable=false;
    }

    IEnumerator BlinkRoutine()
    {
        int t = 10;
        while(t > 0)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(t * blinkRate);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(t * blinkRate);
            t--;
        }
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
