using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpSpecial : MonoBehaviour
{
    public SpMenuManager mM;
    
    [Header("Inputs")]
    public TMP_InputField selectedSkinInp;
    public TMP_InputField selectedMapInp;
    public TMP_InputField moneyInp;
    public TMP_InputField highScoreInp;
    public TMP_InputField skinsOwnedInp;
    public TMP_InputField mapsOwnedInp;

    private int selectedSkin;
    private int selectedMap;
    private int money;
    private int highScore;
    private bool[] skinsOwned = new bool[8];
    private bool[] mapsOwned = new bool[4];

    // Start is called before the first frame update
    void Start()
    {
        LoadValues();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    #region UI

    private void UpdateUI()
    {
        selectedSkinInp.text = selectedSkin.ToString();
        selectedMapInp.text = selectedMap.ToString();
        moneyInp.text = money.ToString();
        highScoreInp.text = highScore.ToString();
        skinsOwnedInp.text = PlayerPrefs.GetString("SingleplayerSkinsOwned", "[1, 0, 0, 0, 0, 0, 0, 0]");
        mapsOwnedInp.text = PlayerPrefs.GetString("SingleplayerMapsOwned", "[1, 0, 0, 0]");
    }

    public void SelectedSkin()
    {
        string tmp = selectedSkinInp.text;
        int i;
        if (int.TryParse(tmp, out i))
        {
            if (i > skinsOwned.Length)
            {
                i = skinsOwned.Length;
            }else if (i < 0)
            {
                i = 0;
            }

            selectedSkin = i;
        }
    }

    public void SelectedMap()
    {
        string tmp = selectedMapInp.text;
        int i;
        if (int.TryParse(tmp, out i))
        {
            if (i > mapsOwned.Length)
            {
                i = mapsOwned.Length;
            }else if (i < 0)
            {
                i = 0;
            }

            selectedMap = i;
        }
    }

    public void Money()
    {
        string tmp = moneyInp.text;
        int i;
        if (int.TryParse(tmp, out i))
        {
            money = i;
        }
    }

    public void HighScore()
    {
        string tmp = highScoreInp.text;
        int i;
        if (int.TryParse(tmp, out i))
        {
            highScore = i;
        }
    }

    public void SkinsOwned()
    {
        string tmp = skinsOwnedInp.text;

        // regex pattern, then check if string is formatted correctly
        string pattern = @"^\[\s*(0|1)\s*,\s*(0|1)\s*,\s*(0|1)\s*,\s*(0|1)\s*\,\s*(0|1)\s*\,\s*(0|1)\s*\,\s*(0|1)\s*\,\s*(0|1)\s*\]$";
        if (new Regex(pattern).IsMatch(tmp))
        {
            skinsOwned = ConvertStringToBool(tmp);
        }
    }

    public void MapsOwned()
    {
        string tmp = mapsOwnedInp.text;

        // regex pattern, then check if string is formatted correctly
        string pattern = @"^\[\s*(0|1)\s*,\s*(0|1)\s*,\s*(0|1)\s*,\s*(0|1)\s*\]$";
        if (new Regex(pattern).IsMatch(tmp))
        {
            mapsOwned = ConvertStringToBool(tmp);
        }
    }

    public void Apply()
    {
        PlayerPrefs.SetInt("SingleplayerSelectedSkin", selectedSkin);
        PlayerPrefs.SetInt("SingleplayerSelectedMap", selectedMap);
        PlayerPrefs.SetInt("SingleplayerMoney", money);
        PlayerPrefs.SetInt("SingleplayerHighScore", highScore);
        PlayerPrefs.SetString("SingleplayerSkinsOwned", ConvertBoolToString(skinsOwned));
        PlayerPrefs.SetString("SingleplayerMapsOwned", ConvertBoolToString(mapsOwned));

        UpdateUI();
        mM.Start();
    }

    #endregion
}
