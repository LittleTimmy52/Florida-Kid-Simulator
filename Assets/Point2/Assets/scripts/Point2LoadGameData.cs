using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine.UI;

public class Point2LoadGameData : MonoBehaviour
{
    public string saveName = "SaveData";
    public string directoryName = "Saves";
    public TextMeshProUGUI message;
    public Button importButton;
    public Point2MenuManager mManager;

    public void LoadGame()
    {
        // if the files exist load the data
        if(Directory.Exists(directoryName))
        {
            // sets the formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // choose the file location
            FileStream saveFile = File.Open(directoryName + "/" + saveName + ".dat", FileMode.Open);

            // converts binary back to c#
            Point2GameSaveData loadFile = (Point2GameSaveData) formatter.Deserialize(saveFile);

            // message
            message.SetText("Data loaded");

            #region SET PREFS TO LOADED DATA

            PlayerPrefs.SetInt("Difficulty", loadFile.difficulty);
            PlayerPrefs.SetFloat("Sensitivity", loadFile.sensitivity);
            PlayerPrefs.SetFloat("Volume", loadFile.volume);
            PlayerPrefs.SetInt("High score", loadFile.highScore);
            PlayerPrefs.SetInt("FirstPlay", loadFile.firstPlay);
            PlayerPrefs.SetInt("Money", loadFile.money);
            PlayerPrefs.SetInt("Map2Unlocked", loadFile.map2Unlocked);
            PlayerPrefs.SetInt("Map3Unlocked", loadFile.map3Unlocked);
            PlayerPrefs.SetInt("Map4Unlocked", loadFile.map4Unlocked);
            PlayerPrefs.SetInt("SkinActive", loadFile.skinActive);
            PlayerPrefs.SetInt("SkinTwoUnlocked", loadFile.skinTwoUnlocked);
            PlayerPrefs.SetInt("SkinThreeUnlocked", loadFile.skinThreeUnlocked);
            PlayerPrefs.SetInt("SkinFourUnlocked", loadFile.skinFourUnlocked);

            #endregion

            // loads things from the menu

            mManager.LoadButtonsDifficulty();
            mManager.LoadSliderVal();
            mManager.LoadMapStore();
            mManager.LoadSkin();

            // makes player wait to press the button again
            importButton.interactable = false;
            StartCoroutine(ImportCooldown(5));

            saveFile.Close();
        }

        if(!Directory.Exists(directoryName))
        {
            message.SetText("File not found");
        }
    }

    IEnumerator ImportCooldown(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        importButton.interactable = true;
    }
}
