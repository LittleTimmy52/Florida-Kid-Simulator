using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SpMenuManager : MonoBehaviour
{

    #region PUBLIC

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject specialMenu;

    [Header("Buttons")]
    public Button leftSkins;
    public Button rightSkins;
    public Button leftMaps;
    public Button rightMaps;
    public Button buySkins;
    public Button buyMaps;
    public Button selectSkins;
    public Button selectMaps;

    [Header("Skins")]
    public GameObject[] skins = new GameObject[8];

    [Header("Maps")]
    public GameObject[] maps = new GameObject[4];

    [Header("Other")]
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI moneyText;

    #endregion

    #region PRIVATE

    private int selectedSkin;
    private int selectedMap;
    private int money;
    private int highScore;
    private bool[] skinsOwned = new bool[8];
    private bool[] mapsOwned = new bool[4];
    private bool special = false;

    #endregion

    // Start is called before the first frame update
    public void Start()
    {
        LoadValues();

        // Show correct skin and map
        foreach(GameObject obj in skins)
        {
            obj.SetActive(false);
        }

        foreach(GameObject obj in maps)
        {
            obj.SetActive(false);
        }

        skins[selectedSkin].SetActive(true);
        maps[selectedMap].SetActive(true);

        if (System.Environment.MachineName == "LittleTimmy52-Ubuntu")
        {
            special = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Change the button state in the shop sections
        ButtonUpdate();

        // For special menu
        if (special)
        {
            if (Input.GetButtonDown("Shift"))
            {
                specialMenu.SetActive(!specialMenu.activeSelf);
            }
        }
    }

    private void LoadValues()
    {
        selectedSkin = PlayerPrefs.GetInt("SingleplayerSelectedSkin", 0);
        selectedMap = PlayerPrefs.GetInt("SingleplayerSelectedMap", 0);
        money = PlayerPrefs.GetInt("SingleplayerMoney", 0);
        highScore = PlayerPrefs.GetInt("SingleplayerHighScore", 0);
        skinsOwned = ConvertStringToBool(PlayerPrefs.GetString("SingleplayerSkinsOwned", "[1, 0, 0, 0, 0, 0, 0, 0]"));
        mapsOwned = ConvertStringToBool(PlayerPrefs.GetString("SingleplayerMapsOwned", "[1, 0, 0, 0]"));
    }

    private void ButtonUpdate()
    {
        if (selectedSkin == 0)
        {
            leftSkins.interactable = false;
            rightSkins.interactable = true;
        }else if (selectedSkin == skins.Length)
        {
            leftSkins.interactable = true;
            rightSkins.interactable = false;
        }else if (selectedSkin > 0 && selectedSkin < skins.Length)
        {
            leftSkins.interactable = true;
            rightSkins.interactable = true;
        }else
        {
            Debug.LogError("Some how the selected skin is " + selectedSkin + " which is either less than 0 or greater than the length of the array. Setting to default");
            selectedSkin = 0;
        }

        if (selectedMap == 0)
        {
            leftMaps.interactable = false;
            rightMaps.interactable = true;
        }else if (selectedMap == maps.Length)
        {
            leftMaps.interactable = true;
            rightMaps.interactable = false;
        }else if (selectedMap > 0 && selectedMap < maps.Length)
        {
            leftMaps.interactable = true;
            rightMaps.interactable = true;
        }else
        {
            Debug.LogError("Some how the selected map is " + selectedMap + " which is either less than 0 or greater than the length of the array. Setting to default");
            selectedMap = 0;
        }

        if (skinsOwned[selectedSkin] == true)
        {
            buySkins.interactable = false;
            selectSkins.interactable = true;
        }else
        {
            buySkins.interactable = true;
            selectSkins.interactable = false;
        }

        if (mapsOwned[selectedMap] == true)
        {
            buyMaps.interactable = false;
            selectMaps.interactable = true;
        }else
        {
            buyMaps.interactable = true;
            selectMaps.interactable = false;
        }

        // Show correct High Score and Money. I don't care that this isn't a button, it goes here.
        highScoreText.SetText("High Score: " + highScore);
        moneyText.SetText("Money: " + money);
    }

    private string ConvertBoolToString(bool[] TF)
	{
		int[] boolAsInt = new int[TF.Length];
		for (int i = 0; i < TF.Length; i++)
		{
			boolAsInt[i] = Convert.ToInt32(TF[i]);
		}
		
		return "[" + string.Join(", ", boolAsInt) + "]";
	}
	
	private bool[] ConvertStringToBool(string str)
	{
		int[] boolAsInt = str.Substring(1, str.Length - 2)
	                         .Split(',')
	                         .Select(s => int.Parse(s))
	                         .ToArray();
		
		bool[] tmp = new bool[boolAsInt.Length];
		for (int i = 0; i < boolAsInt.Length; i++)
		{
			tmp[i] = Convert.ToBoolean(boolAsInt[i]);
		}
		
		return tmp;
	}

    #region BUTTONS

    public void Play()
    {
        SceneManager.LoadScene("SingleplayerMap" + selectedMap);
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Back(bool mainOrOptions)
    {
        if (mainOrOptions)
        {
            SceneManager.LoadScene("MainMenu");
        }else
        {
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

    public void Left(bool skinOrMap)
    {
        if (skinOrMap)
        {
            skins[selectedSkin].SetActive(false);
            selectedSkin--;
            skins[selectedSkin].SetActive(true);
        }else
        {
            maps[selectedMap].SetActive(false);
            selectedMap--;
            maps[selectedMap].SetActive(true);
        }
    }

    public void Right(bool skinOrMap)
    {
        if (skinOrMap)
        {
            skins[selectedSkin].SetActive(false);
            selectedSkin++;
            skins[selectedSkin].SetActive(true);
        }else
        {
            maps[selectedMap].SetActive(false);
            selectedMap++;
            maps[selectedMap].SetActive(true);
        }
    }

    public void Buy(bool skinOrMap)
    {
        if (skinOrMap)
        {
            if (money == selectedSkin * 100)
            {
                money -= selectedSkin * 100;
                skinsOwned[selectedSkin] = true;

                PlayerPrefs.SetInt("SingleplayerMoney", money);
                PlayerPrefs.SetString("SingleplayerSkinsOwned", ConvertBoolToString(skinsOwned));
            }
        }else
        {
            if (money == selectedMap * 1000)
            {
                money -= selectedMap * 1000;
                mapsOwned[selectedMap] = true;

                PlayerPrefs.SetInt("SingleplayerMoney", money);
                PlayerPrefs.SetString("SingleplayerMapsOwned", ConvertBoolToString(mapsOwned));
            }
        }
    }

    public void Select(bool skinOrMap)
    {
        if (skinOrMap)
        {
            PlayerPrefs.SetInt("SingleplayerSelectedSkin", selectedSkin);
        }else
        {
            PlayerPrefs.SetInt("SingleplayerSelectedMap", selectedMap);
        }
    }

    #endregion
}
