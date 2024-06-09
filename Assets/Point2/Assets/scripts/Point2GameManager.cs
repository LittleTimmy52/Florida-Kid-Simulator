using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Point2GameManager : MonoBehaviour
{
    
    #region  PRIVATE

    private Point2FontManager fM;
    private int flagNum;
    private int enemyNum;
    private int score;
    private int oldMoney;
    private Point2PlayerController playerController;

    #endregion

    #region PUBLIC

    [Header("Bools")]
    public bool isGameOver;
    public bool isGameActive;
    [Header("Scores")]
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI flagCount;
    public TextMeshProUGUI enemyCount;
    public TextMeshProUGUI scoreText;
    [Header("Sliders")]
    public Slider sensitivitySlider;
    public Slider volumeSlider;
    public Slider volumeSliderGameOver;
    [Header("Other")]
    public GameObject gameOver;
    public GameObject crosshairs;
    public GameObject gameOverFirstButton;
    public TextMeshProUGUI sensitivityText;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI volumeTextGameOver;
    public AudioMixer mixer;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // sets the script refrences
        fM = GetComponent<Point2FontManager>();
        playerController = GameObject.FindWithTag("Player").GetComponent<Point2PlayerController>();

        // resets the valuesto default
        isGameOver = false;
        isGameActive = true;
        playerController.isGamePaused = false;
        Time.timeScale = 1;

        // calls the loadsliderval function
        LoadSliderVal();

        // loads the volume
        mixer.SetFloat("MusicVol", Mathf.Log10(PlayerPrefs.GetFloat("Volume")) * 20);
    }

    // Update is called once per frame
    void Update()
    {
        if(score >= PlayerPrefs.GetInt("High score"))
        {
            PlayerPrefs.SetInt("High score", score);
        }

        // sets the highscore
        highScore.SetText("High score: " + PlayerPrefs.GetInt("High score"));
    }

    private void LoadSliderVal()
    {
        // loads the sensitivity
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
        sensitivityText.text = "Sensitivity: " + (int) PlayerPrefs.GetFloat("Sensitivity");

        // loads the volume
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        volumeText.text = "Volume: " + (int) (PlayerPrefs.GetFloat("Volume") * 100);
        
        // loads the volume
        volumeSliderGameOver.value = PlayerPrefs.GetFloat("Volume");
        volumeTextGameOver.text = "Volume: " + (int) (PlayerPrefs.GetFloat("Volume") * 100);
    }

    public void SetSensitivity(float newSensitivity)
    {
        // sets the sensitivity value
        PlayerPrefs.SetFloat("Sensitivity", newSensitivity);

        // changes the sensitivity text to display the sensitivity but as a whole number
        sensitivityText.text = "Sensitivity: " + (int) PlayerPrefs.GetFloat("Sensitivity");
    }

    public void SetVolume(float newVolume)
    {
        // sets the volume
        PlayerPrefs.SetFloat("Volume", newVolume);

        // changes the volume text to display the volume but as a whole number between 1 & 100
        volumeText.text = "Volume: " + (int) (PlayerPrefs.GetFloat("Volume") * 100);
        volumeTextGameOver.text = "Volume: " + (int) (PlayerPrefs.GetFloat("Volume") * 100);

        // changes the volume
        mixer.SetFloat("MusicVol", Mathf.Log10(PlayerPrefs.GetFloat("Volume")) * 20);
    }

    public void MainMenu()
    {
        oldMoney = PlayerPrefs.GetInt("Money");
        PlayerPrefs.SetInt("Money", oldMoney += (int) score / 4);

        // loads the main menu
        SceneManager.LoadScene("Main Menu");
    }

    public void Restart()
    {
        oldMoney = PlayerPrefs.GetInt("Money");
        PlayerPrefs.SetInt("Money", oldMoney += (int) score / 4);

        /* sets the time to 1 so the game is unpaused, then it reloads whatever
        active scene the player is in */
        isGameActive = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        oldMoney = PlayerPrefs.GetInt("Money");
        PlayerPrefs.SetInt("Money", oldMoney += (int) score / 4);

        // closes the game
        Application.Quit();
    }
    public void GameOver()
    {
        if (isGameOver == true)
        {
            // pauses the game and opens the game over menu
            Time.timeScale = 0;
            gameOver.SetActive(true);
            crosshairs.SetActive(false);
            isGameActive = false;

            /*// clear selected obj
            EventSystem.current.SetSelectedGameObject(null);

            // set new selected obj
            EventSystem.current.SetSelectedGameObject(gameOverFirstButton);*/
        }
    }

    #region SCORES

    public void UpdateFlagCount(int falgsToAdd)
    {
        // updates the number on the flag count text
        flagNum += falgsToAdd;
        UpdateScore(1);
        flagCount.text = "Flags: " + flagNum;
    }

    public void UpdateEnemyCount(int enemysToAdd)
    {
        // updates the number on the enemy count text
        enemyNum += enemysToAdd;
        UpdateScore(1);
        enemyCount.text = "Enemys: " + enemyNum;
    }

    public void UpdateScore(int scoreToAdd)
    {
        // updates the score displayed
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    #endregion

}
