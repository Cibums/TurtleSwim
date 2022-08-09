using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour {

    public Text hsText;
    public Dropdown DifficultyDropdown;
    public GameObject MainPanel;
    public GameObject SettingsPanel;
    public GameObject CreditsPanel;
    public Slider Slider;
    public Text Mastervolumetext;

    private GameController gameController;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    private void Update()
    {
        gameController.health = gameController.maxHealth;
        hsText.text = "HIGHSCORE: " + (System.Math.Round(gameController.highScore, 2)).ToString();
    }

    public void Play()
    {
        SceneManager.LoadScene("scene");
    }

    public void DifficultyDropdownOnClick()
    {
        switch (DifficultyDropdown.value)
        {
            case 0:
                gameController.difficulty = 0;
                break;
            case 1:
                gameController.difficulty = 1;
                break;
            case 2:
                gameController.difficulty = 2;
                break;
        }
        Debug.Log(DifficultyDropdown.value);
    }

    public void Goback()
    {
        CreditsPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        MainPanel.SetActive(true);
    }
    public void Credits()
    {
        CreditsPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        MainPanel.SetActive(false);
    }

    public void settings()
    {
        if (MainPanel.activeSelf == true)
        {

            MainPanel.SetActive(false);
            SettingsPanel.SetActive(true);
        }
    }
    public void Mastervolume()
    {
        gameController.Musicsource.volume = gameController.mastervolume / 100;
        gameController.mastervolume = Slider.value;
        Mastervolumetext.text = "MASTER VOLUME   " + gameController.mastervolume.ToString() + "%";

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
