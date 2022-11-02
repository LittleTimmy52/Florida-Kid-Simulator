using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TextMeshProUGUI _roomName;

    private RoomCanvases _roomCanvases;

    public void FirstInitialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
    }
    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room seccessfully");
        _roomCanvases.CurrentRoomCanvas.Show();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed: " + message);
    }
}
