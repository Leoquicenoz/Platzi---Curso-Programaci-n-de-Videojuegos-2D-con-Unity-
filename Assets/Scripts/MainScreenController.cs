using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreenController : MonoBehaviour
{
    [SerializeField] GameObject mainScreen;
    [SerializeField] AudioClip buttonClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    public void PlayGame()
    {
        AudioSource.PlayClipAtPoint(buttonClip, transform.position);    //Se reproduce el clip de audio y se da un vector para el sonido (en 2D no importa)
        Invoke("LoadGame", 0.5f);
        mainScreen.SetActive(false); //desactivamos la pantalla principal
    }

    void LoadGame()
    {
        SceneManager.LoadScene("Game"); //Se carga una scena desde el SceneManager para reiniciar el juego
    }
}
