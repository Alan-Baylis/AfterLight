using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; set; }

    [SerializeField] List<GameObject> enemyList = new List<GameObject>();
    GameObject enemy;
    GameObject player;
    DayCycle dayCycle;
    float minRangeToSpawn = 40;
    float maxRangeToSpawn = 40;
    bool isSpawningEnemies;
    bool isDestroyingEnemies;
    // Use this for initialization
    public void StartManager()
    {
        enemy = Resources.Load("Prefabs/Enemy") as GameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        dayCycle = GetComponent<DayCycle>();

        status = ManagerStatus.Started;
    }

    // Update is called once per frame
    void Update()
    {
        if (dayCycle.GetIsNight() && !isSpawningEnemies)
        {
            print("spawning enemies");
            isDestroyingEnemies = false;
            isSpawningEnemies = true;
            SpawnEnemy();
            SpawnEnemy();
            SpawnEnemy();
            SpawnEnemy();
            InvokeRepeating("SpawnEnemy", 0, 10f);
        }
        if (dayCycle.GetIsDay() && !isDestroyingEnemies)
        {
            print("killing enemies");
            isSpawningEnemies = false;
            isDestroyingEnemies = true;
            DestroyEnemies(enemyList);

        }
    }
    public void DestroyEnemies(List<GameObject> list)
    {
        list.ForEach(i => Destroy(i));
        list.Clear();
    }
    public void SpawnEnemy()
    {
        if (dayCycle.GetIsNight())
        {
            GameObject enemyInstance = Instantiate(enemy);
            enemyList.Add(enemyInstance);
            enemyInstance.transform.position = LocationAwayFromPlayer(player, enemyInstance);
        }
    }
    Vector3 LocationAwayFromPlayer(GameObject player, GameObject enemy)
    {
        // is there a better way to randomly make a number negative
        int randomNegative = Random.Range(0, 10);
        randomNegative = randomNegative < 5 ? randomNegative = -1 : randomNegative = 1;

        Vector3 spawnPoint = player.transform.position + new Vector3(Random.Range(minRangeToSpawn, maxRangeToSpawn)
            * randomNegative, enemy.transform.position.y,
         Random.Range(minRangeToSpawn, maxRangeToSpawn) * randomNegative);

        return spawnPoint;
    }

}
