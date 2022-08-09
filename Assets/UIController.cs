using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public float startTime;
    public float speed;
    public GameObject fadePanel;
    public Color startColor;
    public Color endColor;
    public Text scoreText;

    GameController gameController;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        
    }

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        fadePanel.GetComponent<Image>().color = Color.Lerp(startColor,endColor, (Time.time - startTime) * speed);
        scoreText.text = "SCORE: " + System.Math.Round(gameController.score, 2);
    }

    public void PlayAgain()
    {
        gameController.ResetGame();
        SceneManager.LoadScene("scene");
    }

    public void ToMenu()
    {
        Destroy(gameController.gameObject);
        SceneManager.LoadScene("Startscreen");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
