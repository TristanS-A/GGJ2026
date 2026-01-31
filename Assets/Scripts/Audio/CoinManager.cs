using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform spawnPoint;
    public float spawnSpread = 0.2f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnCoin();
        }
    }

    public void SpawnCoin()
    {
        Vector3 randomPos = spawnPoint.position + new Vector3(Random.Range(-spawnSpread, spawnSpread), 0, Random.Range(-spawnSpread, spawnSpread));
        Instantiate(coinPrefab, randomPos, Quaternion.Euler(Random.Range(0, 360), 0, 0));
    }
}
