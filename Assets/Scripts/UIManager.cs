using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] Text healthText;
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Text finalScore;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUIScore (int newScore)
    {
        scoreText.text = newScore.ToString();   //pasamos de entero a string
    }

    public void UpdateUIHealth (int newHealth)
    {
        healthText.text = newHealth.ToString();
    }

    public void UpdateUIHTime (int newTime)
    {
        timeText.text = newTime.ToString();
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true); //Activamos la pantalla de Gameover
        finalScore.text = "SCORE: " + GameManager.Instance.Score;
    }
}
