using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Point2FontManager : MonoBehaviour
{
    public bool isFontSGA = false;
    public TextMeshProUGUI flagCount;
    public TextMeshProUGUI enemyCount;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartTextGameOver;
    public TextMeshProUGUI restartTextPauseMenu;
    public TextMeshProUGUI pausedText;
    public TMP_FontAsset normalFont;
    public TMP_FontAsset SGAFont;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isFontSGA = !isFontSGA;
        }

        if (isFontSGA == true)
        {
            flagCount.font = SGAFont;
            enemyCount.font = SGAFont;
            scoreText.font = SGAFont;
            gameOverText.font = SGAFont;
            restartTextGameOver.font = SGAFont;
            restartTextPauseMenu.font = SGAFont;
            pausedText.font = SGAFont;
        }else
        {
            flagCount.font = normalFont;
            enemyCount.font = normalFont;
            scoreText.font = normalFont;
            gameOverText.font = normalFont;
            restartTextGameOver.font = normalFont;
            restartTextPauseMenu.font = normalFont;
            pausedText.font = normalFont;
        }
    }
}
