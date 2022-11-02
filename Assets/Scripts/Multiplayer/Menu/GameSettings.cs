using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Manager/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField]
    private string _gameVersion = "1.4";
    public string GameVersion { get { return _gameVersion; } }

    [SerializeField]
    private string _nickName = "LittleTimmy52";
    public string Nickname
    {
        get
        {
            int value = Random.Range(0, 9999);
            return _nickName + value.ToString();
        }
    }
}
