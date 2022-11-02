using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListing : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    private Player _player;

    public void SetPlayerInfo(Player player)
    {
        _player = player;
        _text.text = player.NickName;
    }
}
