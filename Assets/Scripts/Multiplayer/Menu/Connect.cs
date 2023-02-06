using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Connect : MonoBehaviourPunCallbacks
{
    public GameObject loading;
    public GameObject createOrJoin;

    // Start is called before the first frame update
    void Start()
    {
        loading.SetActive(true);
        createOrJoin.SetActive(false);

        // set the version and connect to server
        print("Connecting to server");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = MasterManager.GameSettings.Nickname;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        loading.SetActive(false);
        createOrJoin.SetActive(true);

        print("Connected to server");
        print(PhotonNetwork.LocalPlayer.NickName);

        if (!PhotonNetwork.InLobby)
           PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconected from server, cause: " + cause.ToString());
        SceneManager.LoadScene("MainMenu");
    }
    public override void OnJoinedLobby()
    {
        print("Joined lobby");
    }
}
