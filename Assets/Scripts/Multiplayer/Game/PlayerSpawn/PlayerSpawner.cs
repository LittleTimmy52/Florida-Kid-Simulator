using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    private float rangeX = 50f;
    private float rangeZ = 50f;

    public GameObject playerPrefab;
    public float rayDist = 100f;
    public float overlapTestBoxSize = 1f;
    public LayerMask dontSpawn;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-rangeX, rangeX), 20f, Random.Range(-rangeZ, rangeZ));
        PositionRaycast();
    }

    void PositionRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayDist))
        {
            Quaternion spawnRot = Quaternion.Euler(0, Random.Range(-0, 360), 0);

            Vector3 overlapTestBoxScale = new Vector3(overlapTestBoxSize, overlapTestBoxSize, overlapTestBoxSize);
            Collider[] collidersInOverlapBox = new Collider[1];
            int numOfCollidersFound = Physics.OverlapBoxNonAlloc(hit.point, overlapTestBoxScale, collidersInOverlapBox, spawnRot, dontSpawn);

            if (numOfCollidersFound == 0)
            {
                GameObject clone = PhotonNetwork.Instantiate(playerPrefab.name, hit.point, spawnRot);
            }
        }
    }
}
