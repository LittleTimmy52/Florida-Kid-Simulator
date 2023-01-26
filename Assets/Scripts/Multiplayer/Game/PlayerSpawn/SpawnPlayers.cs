using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    private float rangeX = 50f;
    private float rangeZ = 50f;

    public GameObject spawnerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 randPos = new Vector3(Random.Range(-rangeX, rangeX), 20f, Random.Range(-rangeZ, rangeZ));
        PhotonNetwork.Instantiate(spawnerPrefab.name, randPos, Quaternion.identity);
    }
}
