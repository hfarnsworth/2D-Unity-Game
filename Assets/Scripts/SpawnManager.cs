using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private float _spawnInterval = 5.0f;
    [SerializeField]
    private Player _player;

    private bool _stopSpawning;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (!_stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-10.5f, 10.5f), 7.6f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (!_stopSpawning)
        {
            bool isShieldActive = _player.shieldActive;
            int spawnShields = isShieldActive ? 2 : 3;
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7.6f, 0);
            int randomPowerup = Random.Range(0, spawnShields);
            Instantiate(powerups[randomPowerup], posToSpawn, Quaternion.identity);
            float powerupInterval = Random.Range(3, 8);
            yield return new WaitForSeconds(powerupInterval);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
