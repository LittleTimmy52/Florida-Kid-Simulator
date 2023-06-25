using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Point2GameSelect : MonoBehaviour
{
    #region  PUBLIC

    [Header("Buttons")]
    public Button left;
    public Button right;
    [Header("Maps")]
    public GameObject map1;
    public GameObject map2;
    public GameObject map3;
    public GameObject map4;
    [Header("Other")]
    public TMP_Text levelName;
    public int selectedGame;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(selectedGame == 0)
        {
            left.interactable = false;
        }else
        {
            left.interactable = true;
        }

        if(selectedGame == 3)
        {
            right.interactable = false;
        }else
        {
            right.interactable = true;
        }

        if(selectedGame == 0)
        {
            map1.SetActive(true);
            map2.SetActive(false);
            map3.SetActive(false);
            map4.SetActive(false);

            levelName.text = "Map 1";
        }
        if(selectedGame == 1)
        {
            map1.SetActive(false);
            map2.SetActive(true);
            map3.SetActive(false);
            map4.SetActive(false);

            levelName.text = "Map 2";
        }
        if(selectedGame == 2)
        {
            map1.SetActive(false);
            map2.SetActive(false);
            map3.SetActive(true);
            map4.SetActive(false);

            levelName.text = "Map 3";
        }
        if(selectedGame == 3)
        {
            map1.SetActive(false);
            map2.SetActive(false);
            map3.SetActive(false);
            map4.SetActive(true);

            levelName.text = "Map 4";
        }
    }

    public void Left()
    {
        if(selectedGame > 0)
        {
            selectedGame -= 1;
        }
    }

    public void Right()
    {
        if(selectedGame < 3)
        {
            selectedGame += 1;
        }
    }

    public void Play()
    {
        if(selectedGame == 0)
        {
            SceneManager.LoadScene("GameMap1");
        }
        if(selectedGame == 1 && PlayerPrefs.GetInt("Map2Unlocked") == 1)
        {
            SceneManager.LoadScene("GameMap2");
        }
        if(selectedGame == 2 && PlayerPrefs.GetInt("Map3Unlocked") == 1)
        {
            SceneManager.LoadScene("GameMap3");
        }
        if(selectedGame == 3 && PlayerPrefs.GetInt("Map4Unlocked") == 1)
        {
            SceneManager.LoadScene("GameMap4");
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
