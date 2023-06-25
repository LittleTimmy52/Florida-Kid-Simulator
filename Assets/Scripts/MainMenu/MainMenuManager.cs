using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    #region PUBLIC

    [Header("Menues")]
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject infoMenu;

    [Header("Links")]
    public string discordLink;
    public string website;

    #endregion
     
    // Start is called before the first frame update
    void Start()
    {
        // open correct menu
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false); 
        infoMenu.SetActive(false);

        // open in fullscreen
        Screen.fullScreen = true;
    }

    #region BUTTONS

    public void Multiplayer()
    {
        // load scene here
    }

    public void Singleplayer()
    {
        SceneManager.LoadScene("SingleplayerMenu");
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Info()
    {
        infoMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        infoMenu.SetActive(false);
    }

    public void Discord()
    {
        Application.OpenURL(discordLink);
    }

    public void Web()
    {
        Application.OpenURL(website);
    }

    #endregion
}
