using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class Point2MenuManager : MonoBehaviour
{
    #region PRIVATE

    private int oldDifficulty;
    private int moneyAvalible;
    private int skinSelected;

    #endregion

    #region PUBLIC

    [Header("Menues")]
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject info;
    public GameObject skinsMenu;
    public GameObject mapMenu;
    public GameObject updateMenu;

    [Header("Menu first buttons")]
    public GameObject mainMenuFirstButton;
    public GameObject optionsFirstButton;
    public GameObject infoFirstButton;

    [Header("Difficulty buttons (in order Easy-Nightmare)")]
    public Button[] buttons;

    [Header("Sliders")]
    public Slider sensitivitySlider;
    public Slider volumeSlider;

    [Header("Text")]
    public TextMeshProUGUI sensitivityText;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI moneyMapMenu;
    public TextMeshProUGUI moneySkinsMenu;
    public TextMeshProUGUI moneyMainMenu;
    public Text priceSkin2;
    public Text priceSkin3;
    public Text priceSkin4;
    public Text priceMap2;
    public Text priceMap3;
    public Text priceMap4;

    [Header("Map shop buttons")]
    public Button map2Button;
    public Button map3Button;
    public Button map4Button;

    [Header("Skins shop")]
    public Button skin1Button;
    public Button skin2Button;
    public Button skin3Button;
    public Button skin4Button;

    [Header("Skins")]
    public GameObject skin1;
    public GameObject skin2;
    public GameObject skin3;
    public GameObject skin4;

    [Header("Other")]
    public AudioMixer mixer;
    public string currentVersion;
    public string URL;
    public string latestVersion;
        
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region SET DEFUALT VALUES

        // sets the default values for the game when it first opens
        if (PlayerPrefs.GetInt("FirstPlay") == 0)
        {
            PlayerPrefs.SetInt("Difficulty", 5);
            PlayerPrefs.SetFloat("Sensitivity", 12);
            PlayerPrefs.SetFloat("Volume", 0.3f);
            PlayerPrefs.SetInt("High score", 0);
            PlayerPrefs.SetInt("FirstPlay", 1);
            PlayerPrefs.SetInt("Money", 0);
            PlayerPrefs.SetInt("Map2Unlocked", 0);
            PlayerPrefs.SetInt("Map3Unlocked", 0);
            PlayerPrefs.SetInt("Map4Unlocked", 0);
            PlayerPrefs.SetInt("SkinActive", 1);
            PlayerPrefs.SetInt("SkinTwoUnlocked", 0);
            PlayerPrefs.SetInt("SkinThreeUnlocked", 0);
            PlayerPrefs.SetInt("SkinFourUnlocked", 0);
        }

        #endregion

        // sets values
        mixer.SetFloat("MusicVol", Mathf.Log10(PlayerPrefs.GetFloat("Volume")) * 20);
        moneyAvalible = PlayerPrefs.GetInt("Money");
        skinSelected = PlayerPrefs.GetInt("SkinActive");
        Time.timeScale = 1;

        // checks update version
        StartCoroutine(GetVersionData(URL));

        // loads tvalues
        LoadSliderVal();
        LoadSkin();
        LoadMapStore();
    }

    // Update is called once per frame
    void Update()
    {
        // allows player to use the input axis to go back
        // this is really for options because the menu is scuffed
        /*if (Input.GetButtonDown("Cancel"))
        {
            Back();
        }*/

        // sets the highscore text
        highScore.SetText("High score: " + PlayerPrefs.GetInt("High score"));

        // sets the diffrent texts that is for how much money the player has
        moneyMainMenu.SetText("Money: $" + PlayerPrefs.GetInt("Money"));
        moneySkinsMenu.SetText("Money: $" + PlayerPrefs.GetInt("Money"));
        moneyMapMenu.SetText("Money: $" + PlayerPrefs.GetInt("Money"));

        // updates the skin thats selected
        skinSelected = PlayerPrefs.GetInt("SkinActive");
    }

    public void CheckVersion()
    {
        // if there is no new versions dont show update panel else show panel
        if(currentVersion == latestVersion)
        {
            updateMenu.SetActive(false);
        }else
        {
            updateMenu.SetActive(true);
        }
    }

    IEnumerator GetVersionData(string url)
    {
        // set the www to the url
        WWW www = new WWW(url);
        yield return www;

        // set the latest version to the text
        latestVersion = www.text;

        /* if there is no data set the laltest version to the current version so there
        is no update panel shown when the value is null */
        if(www.text == "")
        {
            latestVersion = currentVersion;
        }

        CheckVersion();
    }

    #region LOADING

    public void LoadMapStore()
    {
        // if a skin is unlocked make the button not interactable and the text to nothing
        if(PlayerPrefs.GetInt("SkinTwoUnlocked") == 1)
        {
            map2Button.interactable = false;
            priceMap2.text = " ";
        }

        if(PlayerPrefs.GetInt("SkinThreeUnlocked") == 1)
        {
            map3Button.interactable = false;
            priceMap3.text = " ";
        }

        if(PlayerPrefs.GetInt("SkinFourUnlocked") == 1)
        {
            map4Button.interactable = false;
            priceMap4.text = " ";
        }
    }

    public void LoadSkin()
    {
        // updates the skin thats selected
        skinSelected = PlayerPrefs.GetInt("SkinActive");

        // if the skin is unlocked it sets the price to nothing
        if(PlayerPrefs.GetInt("SkinTwoUnlocked") == 1)
        {
            priceSkin2.text = " ";
        }

        if(PlayerPrefs.GetInt("SkinThreeUnlocked") == 1)
        {
            priceSkin3.text = " ";
        }

        if(PlayerPrefs.GetInt("SkinFourUnlocked") == 1)
        {
            priceSkin4.text = " ";
        }
        
        // sets the selected skin
        if(skinSelected == 1)
        {
            skin1Button.interactable = false;
            skin2Button.interactable = true;
            skin3Button.interactable = true;
            skin4Button.interactable = true;
            skin1.SetActive(true);
            skin2.SetActive(false);
            skin3.SetActive(false);
            skin4.SetActive(false);
        }

        if(skinSelected == 2)
        {
            skin1Button.interactable = true;
            skin2Button.interactable = false;
            skin3Button.interactable = true;
            skin4Button.interactable = true;
            skin1.SetActive(false);
            skin2.SetActive(true);
            skin3.SetActive(false);
            skin4.SetActive(false);
        }

        if(skinSelected == 3)
        {
            skin1Button.interactable = true;
            skin2Button.interactable = true;
            skin3Button.interactable = false;
            skin4Button.interactable = true;
            skin1.SetActive(false);
            skin2.SetActive(false);
            skin3.SetActive(true);
            skin4.SetActive(false);
        }

        if(skinSelected == 4)
        {
            skin1Button.interactable = true;
            skin2Button.interactable = true;
            skin3Button.interactable = true;
            skin4Button.interactable = false;
            skin1.SetActive(false);
            skin2.SetActive(false);
            skin3.SetActive(false);
            skin4.SetActive(true);
        }
    }

    public void LoadSliderVal()
    {
        // loads the sensitivity
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
        sensitivityText.text = "Sensitivity: " + (int) PlayerPrefs.GetFloat("Sensitivity");

        // loads the volume
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        volumeText.text = "Volume: " + (int) (PlayerPrefs.GetFloat("Volume") * 100);
    }

    public void LoadButtonsDifficulty()
    {
        // sets the old difficulty to the difficulty thats stored
        oldDifficulty = PlayerPrefs.GetInt("Difficulty");

        // runs the function for what difficulty is saved
        //depending on what difficulty is saved
        if (oldDifficulty == 5)
        {
            Easy();
        }

        if (oldDifficulty == 4)
        {
            Normal();
        }

        if (oldDifficulty == 3)
        {
            Hard();
        }

        if (oldDifficulty == 2)
        {
            Extreme();
        }

        if (oldDifficulty == 1)
        {
            Nightmare();
        }
    }

    #endregion

    public static void StoreDifficulty(int difficultyVal)
    {
        // stores the difficulty
        PlayerPrefs.SetInt("Difficulty", difficultyVal);
    }

    #region BUTTON FUNCTIONS

    #region MENU BUTTONS

    public void Play()
    {
        SceneManager.LoadScene("Game select");
    }

    public void Options()
    {
        // closes the menu, opens the options
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);

        // clear selected obj
        /*EventSystem.current.SetSelectedGameObject(null);

        // set new selected obj
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);*/

        LoadButtonsDifficulty();
    }

    public void Info()
    {
        // closes the menu opens the info
        mainMenu.SetActive(false);
        info.SetActive(true);

        // clear selected obj
        /*EventSystem.current.SetSelectedGameObject(null);

        // set new selected obj
        EventSystem.current.SetSelectedGameObject(infoFirstButton);*/
    }

    public void Exit()
    {
        // closes the application
        Application.Quit();
    }

    public void Skins()
    {
        // opens skin menu
        mainMenu.SetActive(false);
        skinsMenu.SetActive(true);
    }

    public void Maps()
    {
        // opens map menu
        mainMenu.SetActive(false);
        mapMenu.SetActive(true);
    }

    public void Back()
    {
        // closes options & info, opens the menu
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        info.SetActive(false);
        skinsMenu.SetActive(false);
        mapMenu.SetActive(false);

        // clear selected obj
        /*EventSystem.current.SetSelectedGameObject(null);

        // set new selected obj
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);*/
    }

    public void NotNow()
    {
        updateMenu.SetActive(false);
    }

    public void UpdateGame()
    {
        Application.OpenURL(URL);
    }

    #endregion

    #region  SKIN BUTTONS

    public void SkinOne()
    {
        // sets the skin to the first one
        PlayerPrefs.SetInt("SkinActive", 1);
        LoadSkin();
    }

    public void SkinTwo()
    {
        // if skin not unlocked buy it else select it
        if(PlayerPrefs.GetInt("SkinTwoUnlocked") == 0)
        {
            if(moneyAvalible >= 100)
            {
                moneyAvalible -= 100;
                PlayerPrefs.SetInt("Money", moneyAvalible);
                Debug.Log("My laptop is wack and this needs to be here for this to function 3");

                PlayerPrefs.SetInt("SkinTwoUnlocked", 1);
                priceSkin2.text = " ";
            }
        }
        else if(PlayerPrefs.GetInt("SkinTwoUnlocked") == 1)
        {
            PlayerPrefs.SetInt("SkinActive", 2);

            LoadSkin();
        }
    }

    public void SkinThree()
    {
        // if skin not unlocked buy it else select it
        if(PlayerPrefs.GetInt("SkinThreeUnlocked") == 0)
        {
            if(moneyAvalible >= 200)
            {
                moneyAvalible -= 200;
                PlayerPrefs.SetInt("Money", moneyAvalible);
                Debug.Log("My laptop is wack and this needs to be here for this to function 3");

                PlayerPrefs.SetInt("SkinThreeUnlocked", 1);
                priceSkin3.text = " ";
            }
        }else if(PlayerPrefs.GetInt("SkinThreeUnlocked") == 1)
        {
            PlayerPrefs.SetInt("SkinActive", 3);

            LoadSkin();
        }
    }

    public void SkinFour()
    {
        // if skin not unlocked buy it else select it
        if(PlayerPrefs.GetInt("SkinFourUnlocked") == 0)
        {
            if(moneyAvalible >= 300)
            {
                moneyAvalible -= 300;
                PlayerPrefs.SetInt("Money", moneyAvalible);
                Debug.Log("My laptop is wack and this needs to be here for this to function 3");

                PlayerPrefs.SetInt("SkinFourUnlocked", 1);
                priceSkin4.text = " ";
            }
        }else if(PlayerPrefs.GetInt("SkinFourUnlocked") == 1)
        {
            PlayerPrefs.SetInt("SkinActive", 4);

            LoadSkin();
        }
    }

    #endregion

    #region MAP BUTTONS

    public void Map2()
    {
        if(moneyAvalible >= 500 && PlayerPrefs.GetInt("Map2Unlocked") == 0)
        {
        // if map not unlocked buy
            moneyAvalible -= 500;
            priceMap2.text = " ";
            PlayerPrefs.SetInt("Money", moneyAvalible);
            Debug.Log("My laptop is wack and this needs to be here for this to function 1");

            PlayerPrefs.SetInt("Map2Unlocked", 1);

            LoadMapStore();
        }

        Debug.Log("My laptop is wack and this needs to be here for this to function 2");
    }

    public void Map3()
    {
        // if map not unlocked buy
        if(moneyAvalible >= 1000 && PlayerPrefs.GetInt("Map3Unlocked") == 0)
        {
            moneyAvalible -= 1000;
            priceMap3.text = " ";
            PlayerPrefs.SetInt("Money", moneyAvalible);
            Debug.Log("My laptop is wack and this needs to be here for this to function 3");

            PlayerPrefs.SetInt("Map3Unlocked", 1);

            LoadMapStore();
        }

        Debug.Log("My laptop is wack and this needs to be here for this to function 4");
    }

    public void Map4()
    {
        // if map not unlocked buy
        if(moneyAvalible >= 1500 && PlayerPrefs.GetInt("Map4Unlocked") == 0)
        {
            moneyAvalible -= 1500;
            priceMap4.text = " ";
            PlayerPrefs.SetInt("Money", moneyAvalible);
            Debug.Log("My laptop is wack and this needs to be here for this to function 5");

            PlayerPrefs.SetInt("Map4Unlocked", 1);

            LoadMapStore();
        }

        Debug.Log("My laptop is wack and this needs to be here for this to function 6");
    }

    #endregion

    #region DIFFICULTY BUTTONS

    public void Easy()
    {
        // stores the difficulty
        StoreDifficulty(5);

        // sets all buttions to be interactable
        // sets the buttion clicked to be uninteractable
        buttons[0].interactable = false;
        buttons[1].interactable = true;
        buttons[2].interactable = true;
        buttons[3].interactable = true;
        buttons[4].interactable = true;

        // clear selected obj
        /*EventSystem.current.SetSelectedGameObject(null);

        // set new selected obj
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);*/
    }

    public void Normal()
    {
        // stores the difficulty
        StoreDifficulty(4);

        // sets all buttions to be interactable
        // sets the buttion clicked to be uninteractable
        buttons[1].interactable = false;
        buttons[0].interactable = true;
        buttons[2].interactable = true;
        buttons[3].interactable = true;
        buttons[4].interactable = true;

        // clear selected obj
        /*EventSystem.current.SetSelectedGameObject(null);

        // set new selected obj
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);*/
    }

    public void Hard()
    {
        // stores the difficulty
        StoreDifficulty(3);

        // sets all buttions to be interactable
        // sets the buttion clicked to be uninteractable
        buttons[2].interactable = false;
        buttons[0].interactable = true;
        buttons[1].interactable = true;
        buttons[3].interactable = true;
        buttons[4].interactable = true;

        // clear selected obj
        /*EventSystem.current.SetSelectedGameObject(null);

        // set new selected obj
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);*/
    }

    public void Extreme()
    {
        // stores the difficulty
        StoreDifficulty(2);
            
        // sets all buttions to be interactable
        // sets the buttion clicked to be uninteractable
        buttons[3].interactable = false;
        buttons[0].interactable = true;
        buttons[1].interactable = true;
        buttons[2].interactable = true;
        buttons[4].interactable = true;

        // clear selected obj
        /*EventSystem.current.SetSelectedGameObject(null);

        // set new selected obj
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);*/
    }

    public void Nightmare()
    {
        // stores the difficulty
        StoreDifficulty(1);

        // sets all buttions to be interactable
        // sets the buttion clicked to be uninteractable
        buttons[4].interactable = false;
        buttons[0].interactable = true;
        buttons[1].interactable = true;
        buttons[2].interactable = true;
        buttons[3].interactable = true;

        // clear selected obj
        /*EventSystem.current.SetSelectedGameObject(null);

        // set new selected obj
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);*/
    }

    public void SetSensitivityOptions(float newSensitivity)
    {
        // sets the sensitivity
        PlayerPrefs.SetFloat("Sensitivity", newSensitivity);

        // changes the sensitivity text to display the sensitivity but as a whole number
        sensitivityText.text = "Sensitivity: " + (int) PlayerPrefs.GetFloat("Sensitivity");
    }

    public void SetVolume(float newVolume)
    {
        // sets the volume
        PlayerPrefs.SetFloat("Volume", newVolume);

        // changes the volume text to display the volume but as a whole number
        volumeText.text = "Volume: " + (int) (PlayerPrefs.GetFloat("Volume") * 100);

        // changes the volume
        mixer.SetFloat("MusicVol", Mathf.Log10(PlayerPrefs.GetFloat("Volume")) * 20);
    }

    #endregion

    #endregion
}
