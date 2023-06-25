using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestGameManager : MonoBehaviour
{
    [Header("Stats texts")]
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI flags;
    public TextMeshProUGUI cops;
    public TextMeshProUGUI score;

    [Header("Other")]
    public int fCount = 0;
    public int difficulty = 4;
    public GameObject copPrefab;
    public LayerMask noSpawn;


    private int hCount;
    private int cCount;
    private int sCount;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCops();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCops()
    {
        // Spawning cops
        if (fCount % difficulty == 0)
        {
            Vector3 spawnPos()
            {
                RaycastHit hit;
                float boundBox = 49f;
                Vector3 rand = new Vector3(Random.Range(-boundBox, boundBox), 100, Random.Range(-boundBox, boundBox));
                Vector3 pos = Vector3.zero;
                
                if (Physics.Raycast(rand, Vector3.down, out hit, 120, ~noSpawn))
                {
                    pos = hit.point;
                }else
                {
                    spawnPos();
                }

                return pos;
            }

            Quaternion spawnRot = Quaternion.Euler(0, Random.Range(-360, 360), 0);

            GameObject.Instantiate(copPrefab, spawnPos(), spawnRot);
        }
    }
}
