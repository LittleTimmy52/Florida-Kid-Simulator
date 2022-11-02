using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrJoinRoomCanvas : MonoBehaviour
{
    [SerializeField]
    private CreateRoomMenu _createRoomMenu;

    private RoomCanvases _roomCanvases;

    public void FirstInitialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
        _createRoomMenu.FirstInitialize(canvases);
    }
}
