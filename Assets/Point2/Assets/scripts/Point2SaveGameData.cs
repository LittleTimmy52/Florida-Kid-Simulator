using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine.UI;

public class Point2SaveGameData : MonoBehaviour
{
    public string saveName = "SaveData";
    public string directoryName = "Saves";
    public Point2GameSaveData saveGameData;
    public TextMeshProUGUI message;
    public Button exportButton;

    public void SaveGame()
    {
        #region SAVE DATA FOR EXPORT

        saveGameData.difficulty = PlayerPrefs.GetInt("Difficulty");
        saveGameData.sensitivity = PlayerPrefs.GetFloat("Sensitivity");
        saveGameData.volume = PlayerPrefs.GetFloat("Volume");
        saveGameData.highScore = PlayerPrefs.GetInt("High score");
        saveGameData.money = PlayerPrefs.GetInt("Money");
        saveGameData.map2Unlocked = PlayerPrefs.GetInt("Map2Unlocked");
        saveGameData.map3Unlocked = PlayerPrefs.GetInt("Map3Unlocked");
        saveGameData.map4Unlocked = PlayerPrefs.GetInt("Map4Unlocked");
        saveGameData.skinActive = PlayerPrefs.GetInt("SkinActive");
        saveGameData.skinTwoUnlocked = PlayerPrefs.GetInt("SkinTwoUnlocked");
        saveGameData.skinThreeUnlocked = PlayerPrefs.GetInt("SkinThreeUnlocked");
        saveGameData.skinFourUnlocked = PlayerPrefs.GetInt("SkinFourUnlocked");
        saveGameData.firstPlay = PlayerPrefs.GetInt("FirstPlay");

        #endregion

        // create directory if it dosent exist
        if(!Directory.Exists(directoryName))
        {
            Directory.CreateDirectory(directoryName);
        }

        // sets the formatter
        BinaryFormatter formatter = new BinaryFormatter();

        // choose the file location
        FileStream saveFile = File.Create(directoryName + "/" + saveName + ".dat");

        // write c# to binary
        formatter.Serialize(saveFile, saveGameData);

        saveFile.Close();

        // message
        message.SetText("Saved to " + Directory.GetCurrentDirectory().ToString() + "/Saves/" + saveName + ".dat");

        // makes player wait to press the button again
        exportButton.interactable = false;
        StartCoroutine(ExportCooldown(5));
    }
    
    IEnumerator ExportCooldown(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        exportButton.interactable = true;
    }
}
