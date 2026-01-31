using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform spawnPoint;
    public float spawnSpread = 0.2f;

    public float timeBetweenSpawns = 0.1f;

    private void OnEnable()
    {
        EventSystem.OnAddTotalBrewPoints += SpawnCoins;
    }

    private void OnDisable()
    {
        EventSystem.OnAddTotalBrewPoints -= SpawnCoins;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SpawnCoin();
            StartCoroutine(SpawnCoinsDelayed(10));
        }
    }

    public void SpawnCoin()
    {
        Vector3 randomPos = spawnPoint.position + new Vector3(Random.Range(-spawnSpread, spawnSpread), 0, Random.Range(-spawnSpread, spawnSpread));
        Instantiate(coinPrefab, randomPos, Quaternion.Euler(Random.Range(0, 360), 0, 0));
    }

    public void SpawnCoins(int numberOfCoins)
    {
        Debug.Log("Spawning " + numberOfCoins + " coins");
        StartCoroutine(SpawnCoinsDelayed(numberOfCoins));
    }

    IEnumerator SpawnCoinsDelayed(int numberOfCoins)
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            Debug.Log("spawning coin #" + i);
            SpawnCoin();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
}
